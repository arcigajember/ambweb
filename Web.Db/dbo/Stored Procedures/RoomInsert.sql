CREATE PROCEDURE [dbo].[RoomInsert](@RoomId     INT OUTPUT,
                                   @RoomNumber VARCHAR(50),
                                   @RoomName   VARCHAR(50),
                                   @IsActive   BIT)
AS
     INSERT INTO dbo.Room
     (
     --RoomId - this column value is auto-generated
     RoomNumber,
     RoomName,
     IsActive
     )
     VALUES
     (
     -- RoomId - int
     @RoomNumber, -- RoomNumber - varchar
     @RoomName, -- RoomName - varchar
     @IsActive
     );
     SET @RoomId = SCOPE_IDENTITY();