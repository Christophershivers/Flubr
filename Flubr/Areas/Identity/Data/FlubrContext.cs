using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flubr.Areas.Identity.Data;
using Flubr.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Flubr.Areas.Identity.Data
{
    public class FlubrContext : IdentityDbContext<ApplicationUser>
    {
        public FlubrContext(DbContextOptions<FlubrContext> options)
            : base(options)
        {
        }

        public DbSet<MessagesModel> Message { get; set; }
        public DbSet<UserMessagesModel> UserMessage { get; set; }
        public DbSet<FollowersModel> Followers { get; set; }
        public DbSet<UserPicturesModel> UserPictures { get; set; }
        public DbSet<UserPostModel> UserPosts { get; set; }
        public DbSet<MessageThreadModel> MessageThread { get; set; }
        public DbSet<CommentModel> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            
        }
    }
}
