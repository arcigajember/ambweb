USE AMBData
GO

CREATE PROCEDURE RoomUpdate(
    @RoomId int,
    @RoomNumber varchar(50),
    @RoomName varchar(50)
)
AS
    UPDATE dbo.Room
    SET
        --RoomId - this column value is auto-generated
        dbo.Room.RoomNumber = @RoomNumber, -- varchar
        dbo.Room.RoomName = @RoomName -- varchar
    WHERE dbo.Room.RoomId = @RoomId
GO