
CREATE PROCEDURE [dbo].[GuardianSelectAll]
AS
    SET NOCOUNT ON

    SELECT 
	   g.GuardianId,
	   g.FirstName,
	   g.LastName,
	   g.MiddleName,
	   g.Street,
	   g.Barangay,
	   g.Municipality,
	   g.Province,
	   g.ContactNumber
    FROM Guardian g
    WHERE g.IsActive = 1
