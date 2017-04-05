CREATE PROCEDURE TeacherSelectById(
    @TeacherId int
)
AS
    SET NOCOUNT ON;

    SELECT 
	   t.TeacherId,
	   t.FirstName,
	   t.LastName,
	   t.MiddleName,
	   t.Address
    FROM Teacher t
    WHERE t.IsActive = 1 AND t.TeacherId = @TeacherId
