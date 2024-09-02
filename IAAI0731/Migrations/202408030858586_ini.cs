namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Memberships", "UpdateAt", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Memberships", "UpdateAt", c => c.DateTime(nullable: false));
        }
    }
}
