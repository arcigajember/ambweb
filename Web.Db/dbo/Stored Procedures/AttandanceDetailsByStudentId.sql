CREATE PROCEDURE [dbo].[AttandanceDetailsByStudentId](@StudentId INT)
AS
     SET NOCOUNT ON;
     SELECT ad.AttendanceDetailsId,
            ad.AttendanceHeaderId,
            ad.TimeTypeId,
            ad.[Date],
            ad.[Time]
     FROM dbo.AttendanceDetails ad
          INNER JOIN dbo.AttendanceHeader ah ON ah.AttendanceHeaderId = ad.AttendanceHeaderId
     WHERE ah.StudentId = @StudentId
           AND CONVERT(DATE, ad.[Date]) = CONVERT(DATE, GETDATE());