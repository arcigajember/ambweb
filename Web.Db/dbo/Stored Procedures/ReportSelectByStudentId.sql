CREATE PROCEDURE [dbo].[ReportSelectByStudentId](@StudentId INT,
                                                @DateFrom  DATETIME,
                                                @DateTo    DATETIME)
AS
     SET NOCOUNT ON;
     EXEC dbo.StudentDetailsById
          @StudentId = @StudentId;
     EXEC AttendanceReportByStudent
          @StudentId = @StudentId,
          @DateFrom = @DateFrom,
          @DateTo = @DateTo;