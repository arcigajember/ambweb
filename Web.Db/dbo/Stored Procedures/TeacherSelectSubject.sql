
CREATE PROCEDURE [dbo].[TeacherSelectSubject](
    @TeacherId int
)
AS
    SET NOCOUNT ON;

    SELECT DISTINCT 
	   s.SubjectId,
	   s.SubjectName,
	   s.Description
    FROM Subject s
    INNER JOIN TeacherSchedule ts ON ts.SubjectId = s.SubjectId
    WHERE ts.TeacherId = @TeacherId


