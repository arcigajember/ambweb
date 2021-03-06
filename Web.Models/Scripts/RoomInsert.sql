USE [AMBData]
GO
/****** Object:  StoredProcedure [dbo].[RoomInsert]    Script Date: 9/24/2016 9:26:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RoomInsert](
    @RoomId int OUTPUT,
    @RoomNumber varchar(50),
    @RoomName varchar(50),
    @IsActive BIT
)
AS
    INSERT INTO dbo.Room
    (
        --RoomId - this column value is auto-generated
        RoomNumber,
        RoomName,
	   IsActive
    )
    VALUES
    (
        -- RoomId - int
        @RoomNumber, -- RoomNumber - varchar
        @RoomName, -- RoomName - varchar
	   @IsActive
    )

    SET @RoomId = SCOPE_IDENTITY()
