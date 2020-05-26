namespace Repos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrn1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblStudent", "RegisteredOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblStudent", "RegisteredOn", c => c.DateTime(nullable: false));
        }
    }
}
