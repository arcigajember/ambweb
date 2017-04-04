CREATE PROCEDURE [dbo].[AttendanceReportByStudent](@StudentId INT,
                                           @DateFrom  DATETIME,
                                           @DateTo    DATETIME)
AS
     SET NOCOUNT ON;
     SELECT ad.AttendanceDetailsId,
            ad.AttendanceHeaderId,
            ad.TimeTypeId,
            ad.[Date],
		  ad.[Time],
            tt.TimeTypeId,
            tt.TimeTypeName
     FROM dbo.AttendanceDetails ad
          INNER JOIN dbo.AttendanceHeader ah ON ah.AttendanceHeaderId = ad.AttendanceHeaderId
          INNER JOIN dbo.TimeType tt ON tt.TimeTypeId = ad.TimeTypeId
     WHERE ah.StudentId = @studentId
           AND CONVERT(DATE, ad.[Date]) BETWEEN CONVERT(DATE, @DateFrom) AND CONVERT(DATE, @DateTo);
