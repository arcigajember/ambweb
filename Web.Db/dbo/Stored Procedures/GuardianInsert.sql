CREATE PROCEDURE [dbo].[GuardianInsert](@FirstName     VARCHAR(50),
                                       @LastName      VARCHAR(50),
                                       @MiddleName    VARCHAR(50),
                                       @Street        VARCHAR(50),
                                       @Barangay      VARCHAR(50),
                                       @Municipality  VARCHAR(50),
                                       @Province      VARCHAR(50),
                                       @ContactNumber VARCHAR(50),
                                       @IsActive      BIT,
                                       @GuardianId    INT OUTPUT)
AS
     SET NOCOUNT ON;
     INSERT INTO dbo.Guardian
     (
     --GuardianId - this column value is auto-generated
     FirstName,
     LastName,
     MiddleName,
     Street,
     Barangay,
     Municipality,
     Province,
     ContactNumber,
     IsActive
     )
     VALUES
     (
     -- GuardianId - int
     @FirstName, -- FirstName - varchar
     @LastName, -- LastName - varchar
     @MiddleName, -- MiddleName - varchar
     @Street, -- Street - varchar
     @Barangay, -- Barangay - varchar
     @Municipality, -- Municipality - varchar
     @Province, -- Province - varchar
     @ContactNumber, -- ContactNumber - varchar
     @IsActive
     );
     SET @GuardianId = SCOPE_IDENTITY();