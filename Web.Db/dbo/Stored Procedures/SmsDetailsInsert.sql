CREATE PROCEDURE [dbo].[SmsDetailsInsert](@SmsType           NVARCHAR(50),
                                         @StudentGuardianId INT,
                                         @AspNetUserId      NVARCHAR(128),
                                         @DateSent          DATETIME,
                                         @Status            NVARCHAR(MAX),
                                         @Message           NVARCHAR(MAX),
                                         @SmsDetailsId      INT OUTPUT)
AS
     SET NOCOUNT ON;
     INSERT INTO dbo.SmsDetails
     (
     --SmsDetailsId - this column value is auto-generated
     SmsType,
     StudentGuardianId,
     AspNetUsersId,
     DateSent,
     Status,
     Message
     )
     VALUES
     (
     -- SmsDetailsId - int
     @SmsType, -- SmsType - nvarchar
     @StudentGuardianId, -- StudentGuardianId - int
     @AspNetUserId, -- AspNetUsersId - nvarchar
     @DateSent, -- DateSent - datetime
     @Status, -- Status - nvarchar
     @Message-- Message - nvarchar
     );
     SET @SmsDetailsId = SCOPE_IDENTITY();