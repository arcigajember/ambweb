USE AMBData
GO

CREATE PROCEDURE StudentDelete(
    @StudentId int
)
AS 
    SET NOCOUNT ON

    DELETE 
    FROM Student 
    WHERE StudentId = @StudentId 
GO