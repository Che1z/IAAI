namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "MemberId", c => c.Int());
            CreateIndex("dbo.News", "MemberId");
            AddForeignKey("dbo.News", "MemberId", "dbo.Memberships", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.News", "MemberId", "dbo.Memberships");
            DropIndex("dbo.News", new[] { "MemberId" });
            DropColumn("dbo.News", "MemberId");
        }
    }
}
