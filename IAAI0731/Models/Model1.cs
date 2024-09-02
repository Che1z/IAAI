using IAAI0731.Models.BackModel;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace IAAI0731.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
        public virtual DbSet<UserList> UserListEntities { get; set; }
        public virtual DbSet<ServiceExperience> ServiceExperienceEntities { get; set; }

        public virtual DbSet<Membership> MembershipEntities { get; set; }

        public virtual DbSet<Permission> PermissionEntities { get; set; }
        public virtual DbSet<Knowledge> KnowledgeEntities { get; set; }
        public virtual DbSet<News> NewsEntities { get; set; }
        public virtual DbSet<NewsPhoto> NewsPhotoEntities { get; set; }
        public virtual DbSet<AboutUs> AboutUsEntities { get; set; }
        public virtual DbSet<Expert> ExpertEntities { get; set; }
        public virtual DbSet<Forum> ForumEntities { get; set; }
        public virtual DbSet<ForumReply> ForumReplyEntities { get; set; }
    }
}
