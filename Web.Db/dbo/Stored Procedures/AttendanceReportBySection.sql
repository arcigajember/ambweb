-- Batch submitted through debugger: SQLQuery3.sql|2|0|C:\Users\jonathan.guinto\AppData\Local\Temp\~vs76F1.sql
CREATE PROCEDURE [dbo].[AttendanceReportBySection](@SectionId INT)
AS
     SET NOCOUNT ON;
     SELECT s.StudentId,
            s.FirstName,
            s.MiddleName,
            s.LastName,
            s.Street,
            s.Barangay,
            s.Municipality,
            s.Province,
            s.ContactNumber,
            s.StudentNumber,
            s.Status,
            s2.SectionId,
            s2.SectionName,
            r.RoomId,
            r.RoomNumber,
            r.RoomName
     FROM dbo.Student s
          INNER JOIN dbo.Section s2 ON s2.SectionId = s.SectionId
          INNER JOIN dbo.Room r ON r.RoomId = s2.RoomId
     WHERE s.SectionId = @SectionId;