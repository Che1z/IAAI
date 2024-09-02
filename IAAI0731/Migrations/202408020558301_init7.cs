namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLists", "Salt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLists", "Salt");
        }
    }
}
