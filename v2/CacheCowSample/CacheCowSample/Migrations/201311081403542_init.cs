namespace CacheCowSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccommodationProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DestinationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Destinations", t => t.DestinationId, cascadeDelete: true)
                .Index(t => t.DestinationId);
            
            CreateTable(
                "dbo.Destinations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.TagAccommodationProperties",
                c => new
                    {
                        Tag_Name = c.String(nullable: false, maxLength: 128),
                        AccommodationProperty_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Name, t.AccommodationProperty_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Name, cascadeDelete: true)
                .ForeignKey("dbo.AccommodationProperties", t => t.AccommodationProperty_Id, cascadeDelete: true)
                .Index(t => t.Tag_Name)
                .Index(t => t.AccommodationProperty_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagAccommodationProperties", "AccommodationProperty_Id", "dbo.AccommodationProperties");
            DropForeignKey("dbo.TagAccommodationProperties", "Tag_Name", "dbo.Tags");
            DropForeignKey("dbo.AccommodationProperties", "DestinationId", "dbo.Destinations");
            DropForeignKey("dbo.Destinations", "CountryId", "dbo.Countries");
            DropIndex("dbo.TagAccommodationProperties", new[] { "AccommodationProperty_Id" });
            DropIndex("dbo.TagAccommodationProperties", new[] { "Tag_Name" });
            DropIndex("dbo.AccommodationProperties", new[] { "DestinationId" });
            DropIndex("dbo.Destinations", new[] { "CountryId" });
            DropTable("dbo.TagAccommodationProperties");
            DropTable("dbo.Tags");
            DropTable("dbo.Countries");
            DropTable("dbo.Destinations");
            DropTable("dbo.AccommodationProperties");
        }
    }
}
