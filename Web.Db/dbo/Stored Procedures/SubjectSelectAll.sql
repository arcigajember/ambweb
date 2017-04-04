
CREATE PROCEDURE [dbo].[SubjectSelectAll]
AS
    SELECT 
	   s.SubjectId,
	   s.SubjectName,
	   s.Description
    FROM Subject s
    WHERE s.IsActive = 1