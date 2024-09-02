namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Experts", "AdditionalInfo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Experts", "AdditionalInfo");
        }
    }
}
