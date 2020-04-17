using IrinasBlazorSample.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrinasBlazorSample.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }
        
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(o =>
            {
                o.HasKey(e => e.Id);

                o.HasMany(e => e.Posts)
                    .WithOne(e => e.User);

                o.HasMany(e => e.Documents)
                    .WithOne(e => e.User);
            });

            builder.Entity<Post>(o =>
            {
                o.HasKey(e => e.Id);

                o.HasOne(e => e.User)
                    .WithMany(e => e.Posts)
                    .HasForeignKey(e => e.UserId);
            });

            builder.Entity<Document>(o =>
            {
                o.HasKey(e => e.DocumentId);

                o.HasOne(e => e.User)
                    .WithMany(e => e.Documents)
                    .HasForeignKey(e => e.UserId);
            });
                   
        }
    }
}
