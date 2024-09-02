namespace IAAI0731.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init20 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Fora", newName: "Forum");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Forum", newName: "Fora");
        }
    }
}
