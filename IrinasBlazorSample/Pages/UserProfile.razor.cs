using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrinasBlazorSample.Data;
using IrinasBlazorSample.Data.Models;
using IrinasBlazorSample.Services;
using Microsoft.EntityFrameworkCore;
using BlazorInputFile;
using Microsoft.AspNetCore.Components;

namespace IrinasBlazorSample.Pages
{
    public partial class UserProfile
    {
        private bool ShowUserDetail => SelectedUser != null;
        private bool showDocuments = false;
        private bool showPosts = false;
        IFileListEntry[] selectedFiles;

        [Inject]
        public DataContext DataContext { get; set; }
        [Inject]
        public IFileUploadService FileService { get; set; }

        public List<User> AvailableUsers { get; set; } = new List<User>();
        public User SelectedUser { get; set; }
        public InputFile InputFile { get; set; } = new InputFile();


        protected override void OnInitialized()
        {
            LoadAvailableUsers();
        }

        private void LoadAvailableUsers()
        {
            AvailableUsers = DataContext.Users
                                     .Include(u => u.Posts)
                                     .Include(u => u.Documents)
                                     .ToList();
        }

        private void OnSelectedUserChange(User user)
        {
            SelectedUser = user;
            selectedFiles = null;
        }

        private void ReloadSelectedUser()
        {
            SelectedUser = DataContext.Users.Where(u => u.Id == SelectedUser.Id)
                                        .Include(u => u.Documents)
                                        .Include(u => u.Posts)
                                        .FirstOrDefault();
        }

        private async Task ReInitializeFileInput()
        {
            InputFile = new InputFile();
            await InvokeAsync(() => StateHasChanged());
        }

        private void ShowDocumentClick()
        {
            showDocuments = true;
            showPosts = false;
        }

        private async Task OnClearDocumentQueueClick()
        {
            selectedFiles = null;
            await ReInitializeFileInput();
        }

        private void ShowPostsClick()
        {
            showDocuments = false;
            showPosts = true;
        }

        private void HandleFileUpload(IFileListEntry[] files)
        {
            if (SelectedUser is null) return;

            selectedFiles = files;
        }

        async Task LoadFile(IFileListEntry file)
        {
            // So the UI updates to show progress
            file.OnDataRead += (sender, eventArgs) => InvokeAsync(StateHasChanged);

            await FileService.UploadUserDocumentAsync(SelectedUser.Id, file.Data, file.Name, file.LastModified, file.Size, file.Type);
            ReloadSelectedUser();
        }
    }
}
