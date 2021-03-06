USE [AMBData]
GO
/****** Object:  StoredProcedure [dbo].[SectionRoomSelectById]    Script Date: 10/11/2016 4:01:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SectionRoomSelectById](
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
