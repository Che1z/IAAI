namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init16 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AboutUs", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.AboutUs", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AboutUs", "Content", c => c.String());
            AlterColumn("dbo.AboutUs", "Title", c => c.String(maxLength: 50));
        }
    }
}
