using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Data.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public Guid StorageFilename { get; set; }
        public string Filename { get; set; }
        public DateTime? LastModified { get; set; }
        public long? Size { get; set; }
        public string Type { get; set; }
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
