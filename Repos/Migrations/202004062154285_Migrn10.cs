namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrn10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblInstructor", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblInstructor", "Gender");
        }
    }
}
