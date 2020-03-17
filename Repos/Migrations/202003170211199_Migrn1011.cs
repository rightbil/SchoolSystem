namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrn1011 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "RegisteredOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.Students", "RegistrationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "RegistrationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Students", "RegisteredOn");
        }
    }
}
