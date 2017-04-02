USE AMBData;
GO
CREATE PROCEDURE StudentGetIdentity(@StudentId INT OUTPUT)
AS
     SET NOCOUNT ON;
     SET @StudentId = IDENT_CURRENT('Student');
GO

