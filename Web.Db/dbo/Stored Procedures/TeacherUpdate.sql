
CREATE PROCEDURE TeacherUpdate(
    @TeacherId int,
    @FirstName varchar(50),
    @LastName varchar(50),
    @MiddleName varchar(50),
    @Address varchar(50)
)
AS
    UPDATE dbo.Teacher
    SET
	   dbo.Teacher.FirstName = @FirstName,
	   dbo.Teacher.LastName = @LastName,
	   dbo.Teacher.MiddleName = @MiddleName,
	   dbo.Teacher.Address = @Address
    WHERE dbo.Teacher.TeacherId = @TeacherId
