namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init19 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForumReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostNumber = c.Int(nullable: false),
                        ReplyContent = c.String(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ForumReplies");
            DropTable("dbo.Fora");
        }
    }
}
