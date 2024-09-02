namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserLists", "IsInternationalMember", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserLists", "IsInternationalMember", c => c.Boolean());
        }
    }
}
