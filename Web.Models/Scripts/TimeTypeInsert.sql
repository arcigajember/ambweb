USE [AMBData]
GO
/****** Object:  StoredProcedure [dbo].[TimeTypeInsert]    Script Date: 9/24/2016 9:43:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[TimeTypeInsert](
    @TimeTypeId int OUTPUT,
    @TimeTypeName varchar(50),
    @IsActive BIT
)
AS
    INSERT dbo.TimeType
    (
        --TimeTypeId - this column value is auto-generated
        TimeTypeName,
	   IsActive
    )
    VALUES
    (
        -- TimeTypeId - int
        @TimeTypeName, -- TimTypeName - varchar
	   @IsActive
    )

    SET @TimeTypeId = SCOPE_IDENTITY();
