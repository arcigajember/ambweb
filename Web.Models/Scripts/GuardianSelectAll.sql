USE AMBData
GO

CREATE PROCEDURE GuardianSelectAll
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
	   g.ContactNumber,
	   g.Relation
    FROM Guardian g
GO