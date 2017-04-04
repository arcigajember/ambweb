CREATE PROCEDURE [dbo].[StudentGuardianById](@StudentId INT)
AS
     SELECT g.GuardianId,
            g.FirstName,
            g.LastName,
            g.MiddleName,
            g.ContactNumber,
            sg.StudentGuardianId,
            sg.Relation
     FROM dbo.Guardian g
          INNER JOIN dbo.StudentGuardian sg ON sg.GuardianId = g.GuardianId
     WHERE sg.StudentId = @StudentId
           AND g.IsActive = 1
           AND sg.IsActive = 1;