namespace DependencyScopeTracingDisposeBug.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SP_GetRequestBeginEndLogRecords : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                CREATE PROCEDURE SP_RequestBeginEndLogRecords
                AS
                BEGIN
	                WITH FirstLogs AS (
	                  SELECT 
                      [CorrelationId]
                      ,[UserId]
                      ,[RequestUri]
                      ,[IpAddress]
                      ,[HttpMethod]
                      ,[UserAgent]
                      ,[Category]
                      ,[Level]
                      ,[Kind]
                      ,[Operator]
                      ,[Operation]
                      ,[ResponseStatusCode]
                      ,[LogMessage]
                      ,[ExceptionType]
                      ,[BaseExceptionType]
                      ,[ExceptionMessage]
                      ,[ExceptionStackTrace]
                      ,[Timestamp],
		                ROW_NUMBER() OVER(PARTITION BY l.[CorrelationId] ORDER BY l.[Timestamp] ASC) AS rk
	                  FROM [HttpApiLogRecords] l
	                ),
	                LastLogs AS (
	                  SELECT 
                      [CorrelationId]
                      ,[UserId]
                      ,[RequestUri]
                      ,[IpAddress]
                      ,[HttpMethod]
                      ,[UserAgent]
                      ,[Category]
                      ,[Level]
                      ,[Kind]
                      ,[Operator]
                      ,[Operation]
                      ,[ResponseStatusCode]
                      ,[LogMessage]
                      ,[ExceptionType]
                      ,[BaseExceptionType]
                      ,[ExceptionMessage]
                      ,[ExceptionStackTrace]
                      ,[Timestamp],
		                ROW_NUMBER() OVER(PARTITION BY l.[CorrelationId] ORDER BY l.[Timestamp] DESC) AS rk
	                  FROM [HttpApiLogRecords] l
	                )
	                SELECT * FROM  (
	                  SELECT 
		                  [CorrelationId]
		                  ,[UserId]
		                  ,[RequestUri]
		                  ,[IpAddress]
		                  ,[HttpMethod]
		                  ,[UserAgent]
		                  ,[Category]
		                  ,[Level]
		                  ,[Kind]
		                  ,[Operator]
		                  ,[Operation]
		                  ,[ResponseStatusCode]
		                  ,[LogMessage]
		                  ,[ExceptionType]
		                  ,[BaseExceptionType]
		                  ,[ExceptionMessage]
		                  ,[ExceptionStackTrace]
		                  ,[Timestamp]
	                  FROM FirstLogs fl WHERE fl.rk = 1
	                  UNION ALL
	                  SELECT 
		                  [CorrelationId]
		                  ,[UserId]
		                  ,[RequestUri]
		                  ,[IpAddress]
		                  ,[HttpMethod]
		                  ,[UserAgent]
		                  ,[Category]
		                  ,[Level]
		                  ,[Kind]
		                  ,[Operator]
		                  ,[Operation]
		                  ,[ResponseStatusCode]
		                  ,[LogMessage]
		                  ,[ExceptionType]
		                  ,[BaseExceptionType]
		                  ,[ExceptionMessage]
		                  ,[ExceptionStackTrace]
		                  ,[Timestamp]
                      FROM LastLogs ll WHERE ll.rk = 1
	                ) AS Logs
	                ORDER BY [CorrelationId], [Timestamp];
                END
            ");
        }
        
        public override void Down()
        {
        }
    }
}