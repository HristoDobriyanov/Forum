using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

using Forum.Data.Models;


namespace Forum.Data
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext()
        {

        }

        public ForumDbContext(DbContextOptions options)
            :  base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Reply> Replies { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<PostTag> PostsTags { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                base.OnConfiguring(builder);
                builder.UseSqlServer(Configuration.ConnectionString);
            }
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasMany(c => c.Posts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            builder.Entity<Post>()
                .HasMany(r => r.Replies)
                .WithOne(r => r.Post)
                .HasForeignKey(r => r.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(p => p.Posts)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AutorId);

            builder.Entity<User>()
                .HasMany(r => r.Replies)
                .WithOne(a => a.Author)
                .HasForeignKey(a => a.AuthorId);

            builder.Entity<Tag>()
                .ToTable("Tags");

            builder.Entity<PostTag>()
                .ToTable("PostsTags")
                .HasKey(pt => new { pt.PostId, pt.TagId});
        }
    }
}