
CREATE PROCEDURE StudentGuardianContact(
    @StudentId INT
)
AS 
    SET NOCOUNT ON;

    SELECT 
	g.GuardianId,
	g.ContactNumber,
	sg.StudentGuardianId
    FROM Guardian g 
    INNER JOIN StudentGuardian sg ON sg.GuardianId = g.GuardianId
    WHERE sg.StudentId = @StudentId
