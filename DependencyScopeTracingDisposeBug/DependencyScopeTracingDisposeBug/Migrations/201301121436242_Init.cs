namespace DependencyScopeTracingDisposeBug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HttpApiLogRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CorrelationId = c.Guid(nullable: false),
                        UserId = c.String(),
                        RequestUri = c.String(),
                        IpAddress = c.String(),
                        HttpMethod = c.String(),
                        UserAgent = c.String(),
                        Category = c.String(),
                        Level = c.String(),
                        Kind = c.String(),
                        Operator = c.String(),
                        Operation = c.String(),
                        ResponseStatusCode = c.Int(),
                        LogMessage = c.String(),
                        ExceptionType = c.String(),
                        BaseExceptionType = c.String(),
                        ExceptionMessage = c.String(),
                        ExceptionStackTrace = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HttpApiLogRecords");
        }
    }
}
