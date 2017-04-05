
CREATE PROCEDURE [dbo].[TeacherSelectAll]
AS
    SELECT 
	   t.TeacherId,
	   t.FirstName,
	   t.LastName,
	   t.MiddleName,
	   t.Address
    FROM Teacher t
    WHERE t.IsActive = 1
