
CREATE PROCEDURE [dbo].[StudentDelete](
    @StudentId int
)
AS 
    SET NOCOUNT ON

    UPDATE dbo.Student
    SET
        dbo.Student.IsActive = 0 -- bit
    WHERE dbo.Student.StudentId = @StudentId
