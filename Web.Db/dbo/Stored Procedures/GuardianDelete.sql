CREATE PROCEDURE [dbo].[GuardianDelete](
    @GuardianId int
)
AS
    SET NOCOUNT ON

    UPDATE dbo.Guardian
    SET
    dbo.Guardian.IsActive = 0
    WHERE dbo.Guardian.GuardianId = @GuardianId