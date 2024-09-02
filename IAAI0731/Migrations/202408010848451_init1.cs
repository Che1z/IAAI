namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceExperiences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ServiceCompany = c.String(nullable: false, maxLength: 100),
                        JobTitle = c.String(nullable: false, maxLength: 100),
                        StartYear = c.Int(nullable: false),
                        StartMonth = c.Int(nullable: false),
                        EndYear = c.Int(),
                        EndMonth = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserLists", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceExperiences", "UserId", "dbo.UserLists");
            DropIndex("dbo.ServiceExperiences", new[] { "UserId" });
            DropTable("dbo.ServiceExperiences");
        }
    }
}
