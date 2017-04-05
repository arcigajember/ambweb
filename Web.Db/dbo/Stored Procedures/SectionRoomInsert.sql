
CREATE PROCEDURE SectionRoomInsert(
    @RoomId int,
    @SectionId int
)
AS
   UPDATE dbo.Section
   SET
       --SectionId - this column value is auto-generated
       dbo.Section.RoomId = @RoomId -- int
    WHERE dbo.Section.SectionId = @SectionId 
