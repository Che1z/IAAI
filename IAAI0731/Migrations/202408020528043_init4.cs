namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserLists", "IsInternationalMember", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserLists", "IsInternationalMember", c => c.Boolean(nullable: false));
        }
    }
}
