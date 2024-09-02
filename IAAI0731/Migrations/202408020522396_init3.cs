namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServiceExperiences", "ServiceCompany", c => c.String(maxLength: 100));
            AlterColumn("dbo.ServiceExperiences", "JobTitle", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ServiceExperiences", "JobTitle", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.ServiceExperiences", "ServiceCompany", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
