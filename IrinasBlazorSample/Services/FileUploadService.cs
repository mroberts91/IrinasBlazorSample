using IrinasBlazorSample.Data;
using IrinasBlazorSample.Data.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Services
{
    public interface IFileUploadService
    {
        Document GetDocumentByStorageId(Guid documentStorageId);
        Document GetDocumentByStorageId(int documentId);
        Task UploadUserDocumentAsync(string userId, Stream fileStream, string fileName, DateTime? lastModified = null, long? fileSize = null, string fileType = null);
    }

    public class FileUploadService : IFileUploadService
    {
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IWebHostEnvironment _environment;
        public FileUploadService(ILoggerFactory loggerFactory, IServiceScopeFactory scopeFactory, IWebHostEnvironment environment)
        {
            _logger = loggerFactory.CreateLogger(GetType().Name);
            _serviceScopeFactory = scopeFactory;
            _environment = environment;
        }

        public Document GetDocumentByStorageId(Guid documentStorageId)
        {
            var scope = _serviceScopeFactory.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            return db.Documents.FirstOrDefault(d => d.StorageFilename == documentStorageId);
        }

        public Document GetDocumentByStorageId(int documentId)
        {
            var scope = _serviceScopeFactory.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            return db.Documents.FirstOrDefault(d => d.DocumentId == documentId);
        }

        public async Task UploadUserDocumentAsync(string userId, Stream fileStream, string fileName, DateTime? lastModified = null, long? fileSize = null, string fileType = null)
        {
            var storageFileName = await SaveFileToStorage(fileStream);
            await SaveDocumentDataToDatabase(userId, fileName, storageFileName, lastModified, fileSize, fileType);
        }

        private async Task SaveDocumentDataToDatabase(string userId, string fileName, Guid stroageDocumentName, DateTime? lastModified, long? fileSize, string fileType)
        {
            var scope = _serviceScopeFactory.CreateScope();
            using var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            var document = new Document
            {
                Filename = fileName,
                StorageFilename = stroageDocumentName,
                LastModified = lastModified,
                Size = fileSize,
                Type = fileType,
                UserId = userId,
            };
            var user = db.Users.Include(u => u.Documents).FirstOrDefault(u => u.Id == userId);
            user?.Documents.Add(document);
            await db.SaveChangesAsync();
        }

        private async Task<Guid> SaveFileToStorage(Stream inputStream)
        {
            try
            {
                var directory = new DirectoryInfo($@"{_environment.ContentRootPath}\wwwroot\files");
                if (!directory.Exists)
                    directory.Create();

                var storageFilename = Guid.NewGuid();
                var path = Path.Combine(directory.FullName, storageFilename.ToString());

                using FileStream outputFileStream = new FileStream(path, FileMode.Create);
                await inputStream.CopyToAsync(outputFileStream);
                outputFileStream.Close();
                
                return storageFilename;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected Errro: {message}", ex.Message);
                throw;
            }
        }
    }
}
