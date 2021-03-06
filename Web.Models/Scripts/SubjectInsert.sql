USE [AMBData];
GO

/****** Object:  StoredProcedure [dbo].[SubjectInsert]    Script Date: 9/24/2016 8:47:41 AM ******/

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
ALTER PROCEDURE [dbo].[SubjectInsert](@SubjectId   INT OUTPUT,
                                      @SubjectName VARCHAR(50),
                                      @Description VARCHAR(50),
							   @IsActive BIT)
AS
     INSERT INTO dbo.Subject
     (
     --SubjectId - this column value is auto-generated
     SubjectName,
     Description,
	IsActive
     )
     VALUES
     (
     -- SubjectId - int
     @SubjectName, -- SubjectName - varchar
     @Description, -- Description - varchar
	@IsActive
     );
     SET @SubjectId = SCOPE_IDENTITY();