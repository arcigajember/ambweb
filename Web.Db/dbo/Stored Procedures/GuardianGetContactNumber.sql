CREATE PROCEDURE [dbo].[GuardianGetContactNumber](@SectionId INT)
AS
     SET NOCOUNT ON;
     SELECT DISTINCT
            g.GuardianId,
            g.ContactNumber,
            sg.StudentGuardianId
     FROM Guardian g
          INNER JOIN StudentGuardian sg ON sg.GuardianId = g.GuardianId
          INNER JOIN Student s ON s.StudentId = sg.StudentId
     WHERE s.SectionId = @SectionId AND g.IsActive = 1;
