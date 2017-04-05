
CREATE PROCEDURE SectionRoomSelectById(
    @SectionId int
)
AS
    SET NOCOUNT ON;

    SELECT 
	   s.SectionId,
	   s.SectionName,
	   r.RoomId,
	   r.RoomName,
	   r.RoomNumber
    FROM Section s 
    INNER JOIN Room r ON r.RoomId = s.RoomId
    WHERE s.SectionId = @SectionId
