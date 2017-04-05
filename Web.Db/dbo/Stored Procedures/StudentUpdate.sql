
CREATE PROCEDURE [dbo].[StudentUpdate](
    @StudentId int,
    @FirstName varchar(50),
    @LastName varchar(50),
    @MiddleName varchar(50),
    @Street varchar(50),
    @Barangay varchar(50),
    @Municipality varchar(50),
    @Province varchar(50),
    @ContactNumber varchar(50),
    @Status varchar(50),
    @Gender varchar(50)
)
AS
    UPDATE dbo.Student
    SET
        --StudentId - this column value is auto-generated
        dbo.Student.FirstName = @FirstName, -- varchar
        dbo.Student.LastName = @LastName, -- varchar
        dbo.Student.MiddleName = @MiddleName, -- varchar
        dbo.Student.Street = @Street, -- varchar
        dbo.Student.Barangay = @Barangay, -- varchar
        dbo.Student.Municipality = @Municipality, -- varchar
        dbo.Student.Province = @Province, -- varchar
        dbo.Student.ContactNumber = @ContactNumber, -- varchar
        dbo.Student.Status = @Status, -- varchar
        dbo.Student.Gender = @Gender -- varchar
    WHERE StudentId = @StudentId
