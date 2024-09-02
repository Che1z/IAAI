namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init21 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ForumReplies", "PostNumber");
            AddForeignKey("dbo.ForumReplies", "PostNumber", "dbo.Forum", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForumReplies", "PostNumber", "dbo.Forum");
            DropIndex("dbo.ForumReplies", new[] { "PostNumber" });
        }
    }
}
