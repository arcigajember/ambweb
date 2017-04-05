CREATE PROCEDURE [dbo].[GuardianUpdate](@FirstName     VARCHAR(50),
                                @LastName      VARCHAR(50),
                                @MiddleName    VARCHAR(50),
                                @Street        VARCHAR(50),
                                @Barangay      VARCHAR(50),
                                @Municipality  VARCHAR(50),
                                @Province      VARCHAR(50),
                                @ContactNumber VARCHAR(50),
                                @GuardianId    INT OUTPUT)
AS
   
    UPDATE dbo.Guardian
    SET
    dbo.Guardian.FirstName = @FirstName,
    dbo.Guardian.LastName = @LastName,
    dbo.Guardian.MiddleName = @MiddleName,
    dbo.Guardian.Street = @Street,
    dbo.Guardian.Barangay = @Barangay,
    dbo.Guardian.Municipality = @Municipality,
    dbo.Guardian.Province = @Province,
    dbo.Guardian.ContactNumber = @ContactNumber
    WHERE dbo.Guardian.GuardianId = @GuardianId
   
