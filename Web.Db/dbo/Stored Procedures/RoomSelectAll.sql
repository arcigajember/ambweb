
CREATE PROCEDURE [dbo].[RoomSelectAll]
AS
    SET NOCOUNT ON;
    SELECT 
	   r.RoomId,
	   r.RoomNumber,
	   r.RoomName
    FROM Room r
    WHERE r.IsActive = 1