USE AMBData
GO

CREATE PROCEDURE TeacherSelectAll
AS
    SELECT 
	   t.TeacherId,
	   t.FirstName,
	   t.LastName,
	   t.MiddleName,
	   t.Address
    FROM Teacher t
GO