namespace AttributeRoutingSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Stores", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Stores", "Name", c => c.Int(nullable: false));
        }
    }
}
