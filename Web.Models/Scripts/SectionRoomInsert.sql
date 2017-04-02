USE [AMBData]
GO
/****** Object:  StoredProcedure [dbo].[SectionRoomInsert]    Script Date: 10/11/2016 4:01:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SectionRoomInsert](
    @RoomId int,
    @SectionId int
)
AS
   UPDATE dbo.Section
   SET
       --SectionId - this column value is auto-generated
       dbo.Section.RoomId = @RoomId -- int
    WHERE dbo.Section.SectionId = @SectionId 
