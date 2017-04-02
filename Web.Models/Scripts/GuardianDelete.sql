USE AMBData;
GO
CREATE PROCEDURE GuardianDelete(
    @GuardianId int
)
AS
    SET NOCOUNT ON

    UPDATE dbo.Guardian
    SET
    dbo.Guardian.IsActive = 0
GO