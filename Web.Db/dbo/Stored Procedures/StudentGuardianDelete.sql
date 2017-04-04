CREATE PROCEDURE StudentGuardianDelete(@StudentId  INT,
                                       @GuardianId INT)
AS
     UPDATE StudentGuardian
       SET
           dbo.StudentGuardian.IsActive = 0
     WHERE dbo.StudentGuardian.StudentId = @StudentId
           AND dbo.StudentGuardian.GuardianId = @GuardianId;
