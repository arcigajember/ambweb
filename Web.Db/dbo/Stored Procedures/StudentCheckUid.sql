CREATE PROCEDURE [dbo].[StudentCheckUid](@Uid NVARCHAR(50))
AS
     SELECT COUNT(*)
     FROM Student s
     WHERE s.Uid = @Uid
           AND s.IsActive = 1;