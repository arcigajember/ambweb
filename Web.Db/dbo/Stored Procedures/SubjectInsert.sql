CREATE PROCEDURE [dbo].[SubjectInsert](@SubjectId   INT OUTPUT,
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