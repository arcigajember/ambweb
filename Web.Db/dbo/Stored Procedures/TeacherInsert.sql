
CREATE PROCEDURE [dbo].[TeacherInsert](
    @TeacherId int OUTPUT,
    @FirstName varchar(50),
    @LastName varchar(50),
    @MiddleName varchar(50),
    @Address varchar(50),
    @IsActive BIT
)
AS
    INSERT dbo.Teacher
    (
        --TeacherId - this column value is auto-generated
        FirstName,
        MiddleName,
        LastName,
        Address,
	   IsActive
    )
    VALUES
    (
        -- TeacherId - int
        @FirstName, -- FirstName - varchar
        @MiddleName, -- MiddleName - varchar
        @LastName, -- LastName - varchar
        @Address, -- Address - varchar
	   @IsActive
    )

    SET @TeacherId = SCOPE_IDENTITY()

