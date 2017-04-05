CREATE PROCEDURE SectionRoomSelectAll
AS
     SET NOCOUNT ON;
     SELECT s.SectionId,
            s.SectionName,
            r.RoomId,
            r.RoomNumber,
            r.RoomName
     FROM Section s
          INNER JOIN Room r ON r.RoomId = s.RoomId;
