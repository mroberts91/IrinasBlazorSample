using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Data.Models
{
    public class User
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Document> Documents { get; set; }
    }
}
