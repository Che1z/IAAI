namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Description", c => c.String(nullable: false, maxLength: 200));
            DropColumn("dbo.NewsPhotoes", "IsCover");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsPhotoes", "IsCover", c => c.Boolean(nullable: false));
            DropColumn("dbo.News", "Description");
        }
    }
}
