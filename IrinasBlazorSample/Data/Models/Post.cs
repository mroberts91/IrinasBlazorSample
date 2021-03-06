﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Data.Models
{
    public class Post
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        
        public string Content { get; set; }

        public DateTime Timestamp { get; set; }
        
        
        public User User { get; set; }

    }
}
