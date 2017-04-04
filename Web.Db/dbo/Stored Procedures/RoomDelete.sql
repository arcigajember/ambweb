
CREATE PROCEDURE RoomDelete(
    @RoomId int
)
AS
    UPDATE dbo.Room
    SET
        dbo.Room.IsActive = 0
    WHERE dbo.Room.RoomId = @RoomId
