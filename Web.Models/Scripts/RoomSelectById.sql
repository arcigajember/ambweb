USE AMBData
GO

CREATE PROCEDURE RoomSelectById(
    @RoomId int
)
AS
    SELECT 
	   r.RoomId,
	   r.RoomNumber,
	   r.RoomName
    FROM Room r
    WHERE r.RoomId = @RoomId
GO