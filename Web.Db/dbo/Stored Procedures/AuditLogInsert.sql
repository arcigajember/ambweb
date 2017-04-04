CREATE PROCEDURE AuditLogInsert(@AuditId      UNIQUEIDENTIFIER,
                               @UserName     NVARCHAR(50),
                               @AreaAccessed NVARCHAR(MAX),
                               @TimeAccessed DATETIME,
                               @IPAddress    NVARCHAR(MAX),
                               @Parameters   NVARCHAR(MAX))
AS
     BEGIN
         SET NOCOUNT ON;
         INSERT INTO dbo.AuditLog
         (AuditId,
          UserName,
          AreaAccessed,
          TimeAccessed,
          IPAddress,
          Parameters
         )
         VALUES
         (@AuditId, -- AuditId - uniqueidentifier
          @UserName, -- UserName - nvarchar
          @AreaAccessed, -- AreaAccesed - nvarchar
          @TimeAccessed, -- Timestamp - timestamp
          @IPAddress,
          @Parameters
         );
     END;