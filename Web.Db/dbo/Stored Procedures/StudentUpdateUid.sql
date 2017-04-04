
CREATE PROCEDURE StudentUpdateUid(
    @StudentId INT,
    @Uid NVARCHAR(50)
)
AS 
 UPDATE dbo.Student
 SET
 dbo.Student.Uid = @Uid
 WHERE dbo.Student.StudentId = @StudentId 
