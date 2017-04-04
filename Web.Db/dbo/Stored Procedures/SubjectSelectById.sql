
CREATE PROCEDURE SubjectSelectById(
    @SubjectId int
)
AS
    SELECT 
	   s.SubjectId,
	   s.SubjectName,
	   s.Description
    FROM Subject s
    WHERE s.SubjectId = @SubjectId
