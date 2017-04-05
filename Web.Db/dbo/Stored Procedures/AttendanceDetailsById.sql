
CREATE PROCEDURE [dbo].[AttendanceDetailsById](
    @AttendanceDetailsId INT
)
AS
    SET NOCOUNT ON;

    SELECT 
	   ad.AttendanceDetailsId,
	   ad.AttendanceHeaderId,
	   ad.TimeTypeId,
	   ad.[Date],
	   ad.[Time]
    FROM dbo.AttendanceDetails ad
    WHERE ad.AttendanceDetailsId = @AttendanceDetailsId
