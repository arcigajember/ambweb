CREATE PROCEDURE [dbo].[StudentTimeCheck](@StudentId INT)
AS
     DECLARE @TimeCount INT;
     SET @TimeCount =
     (
         SELECT COUNT(*)
         FROM dbo.AttendanceDetails ad
              INNER JOIN AttendanceHeader ah ON ah.AttendanceHeaderId = ad.AttendanceHeaderId
         WHERE ah.StudentId = @StudentId
               AND (CONVERT(DATE, ad.[Date]) = CONVERT(DATE, GETDATE()))
     );
     IF @TimeCount = 0
         BEGIN
             SELECT 1;
         END;
     ELSE
     IF @TimeCount = 1
         BEGIN
             SELECT 2;
         END;
