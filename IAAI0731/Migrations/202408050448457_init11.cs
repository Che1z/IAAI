namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Knowledges", "MemberId", c => c.Int());
            CreateIndex("dbo.Knowledges", "MemberId");
            AddForeignKey("dbo.Knowledges", "MemberId", "dbo.Memberships", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Knowledges", "MemberId", "dbo.Memberships");
            DropIndex("dbo.Knowledges", new[] { "MemberId" });
            DropColumn("dbo.Knowledges", "MemberId");
        }
    }
}
