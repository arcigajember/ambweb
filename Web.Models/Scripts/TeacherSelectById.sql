USE AMBData
GO

CREATE PROCEDURE TeacherSelectById(
    @TeacherId int
)
AS
    SELECT 
	   t.TeacherId,
	   t.FirstName,
	   t.LastName,
	   t.MiddleName,
	   t.Address
    FROM Teacher t
    WHERE t.TeacherId = @TeacherId
GO