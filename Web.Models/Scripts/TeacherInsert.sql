USE [AMBData]
GO
/****** Object:  StoredProcedure [dbo].[TeacherInsert]    Script Date: 9/24/2016 8:48:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[TeacherInsert](
    @TeacherId int OUTPUT,
    @FirstName varchar(50),
    @LastName varchar(50),
    @MiddleName varchar(50),
    @Address varchar(50),
    @IsActive BIT
)
AS
    INSERT dbo.Teacher
    (
        --TeacherId - this column value is auto-generated
        FirstName,
        MiddleName,
        LastName,
        Address,
	   IsActive
    )
    VALUES
    (
        -- TeacherId - int
        @FirstName, -- FirstName - varchar
        @MiddleName, -- MiddleName - varchar
        @LastName, -- LastName - varchar
        @Address, -- Address - varchar
	   @IsActive
    )

    SET @TeacherId = SCOPE_IDENTITY()

