USE AMBData
GO

CREATE PROCEDURE TeacherDelete(
    @TeacherId int
)
AS
    UPDATE dbo.Teacher
    SET
	   dbo.Teacher.IsActive = 0
    WHERE dbo.Teacher.TeacherId = @TeacherId
GO