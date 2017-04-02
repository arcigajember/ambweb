USE AMBData
GO

CREATE PROCEDURE SubjectSelectAll
AS
    SELECT 
	   s.SubjectId,
	   s.SubjectName,
	   s.Description
    FROM Subject s
GO