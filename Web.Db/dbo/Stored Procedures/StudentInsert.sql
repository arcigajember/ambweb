CREATE PROCEDURE [dbo].[StudentInsert](@StudentNumber VARCHAR(50),
                               @FirstName     VARCHAR(50),
                               @LastName      VARCHAR(50),
                               @MiddleName    VARCHAR(50),
                               @Street        VARCHAR(50),
                               @Barangay      VARCHAR(50),
                               @Municipality  VARCHAR(50),
                               @Province      VARCHAR(50),
                               @ContactNumber VARCHAR(50),
                               @Status        VARCHAR(50),
                               @Gender        VARCHAR(50),
                               @StudentId     INT OUTPUT,
						 @IsActive BIT)
						 
AS
     SET NOCOUNT ON;
     INSERT INTO dbo.Student
     (
     --StudentId - this column value is auto-generated
     StudentNumber,
     FirstName,
     LastName,
     MiddleName,
     Street,
     Barangay,
     Municipality,
     Province,
     ContactNumber,
     Status,
     Gender,
	IsActive
     )
     VALUES
     (
     -- StudentId - int
     @StudentNumber, -- StudentNumber - varchar
     @FirstName, -- FirstName - varchar
     @LastName, -- LastName - varchar
     @MiddleName, -- MiddleName - varchar
     @Street, -- Street - varchar
     @Barangay, -- Barangay - varchar
     @Municipality, -- Municipality - varchar
     @Province, -- Province - varchar
     @ContactNumber, -- ContactNumber - varchar
     @Status, -- Status - varchar
     @Gender, -- Gender - varchar
	@IsActive
     );
     SET @StudentId = SCOPE_IDENTITY();
