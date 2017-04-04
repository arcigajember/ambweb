CREATE PROCEDURE StudentSectionUpdate(
       @StudentId INT,
       @SectionId INT )
AS
     UPDATE dbo.Student
       SET dbo.Student.SectionId = @SectionId
     WHERE dbo.Student.StudentId = @StudentId;
