USE AMBData
GO

CREATE PROCEDURE RoomSelectAll
AS
    SELECT 
	   r.RoomId,
	   r.RoomNumber,
	   r.RoomName
    FROM Room r
GO