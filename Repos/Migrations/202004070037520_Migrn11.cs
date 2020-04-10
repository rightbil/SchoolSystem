namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrn11 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tblInstructor", "Major");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tblInstructor", "Major", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
