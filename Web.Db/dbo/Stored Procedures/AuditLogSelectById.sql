CREATE PROCEDURE AuditLogSelectById(@AuditId UNIQUEIDENTIFIER)
AS
     BEGIN
         SET NOCOUNT OFF;
         SELECT al.AuditId,
                al.UserName,
                al.AreaAccessed,
                al.Timeaccessed,
                al.IPAddress,
                al.Parameters
         FROM dbo.AuditLog al;
     END;