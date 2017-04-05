CREATE PROCEDURE AttendanceLogDetails(@DateFrom DATETIME,
                                     @DateTo   DATETIME)
AS
     SET NOCOUNT ON;
     SELECT al.AttendancelogId,
            al.ApiResponse,
            al.AttendanceHeaderId,
            al.StudentGuardianId,
            al.ApiResponse,
            s.StudentId,
            s.FirstName,
            s.MiddleName,
            s.LastName,
            s.Street,
            s.Barangay,
            s.Municipality,
            s.Province,
            s.ContactNumber,
            s2.SectionId,
            s2.SectionName,
            r.RoomId,
            r.RoomNumber,
            r.RoomName,
            tt.TimeTypeId,
            tt.TimeTypeName,
            ad.AttendanceDetailsId,
            ad.[Date],
            ad.[Time],
            g.GuardianId,
            g.FirstName,
            g.LastName,
            g.MiddleName,
            g.Street,
            g.Barangay,
            g.Municipality,
            g.Province,
            g.ContactNumber
     FROM dbo.AttendanceLog al
          INNER JOIN dbo.AttendanceHeader ah ON ah.AttendanceHeaderId = al.AttendanceLogId
          INNER JOIN dbo.AttendanceDetails ad ON ad.AttendanceHeaderId = ah.AttendanceHeaderId
          INNER JOIN dbo.TimeType tt ON tt.TimeTypeId = ad.TimeTypeId
          INNER JOIN dbo.Student s ON s.StudentId = ah.StudentId
          INNER JOIN dbo.StudentGuardian sg ON sg.StudentGuardianId = al.StudentGuardianId
          INNER JOIN dbo.Guardian g ON g.GuardianId = sg.GuardianId
          INNER JOIN dbo.Section s2 ON s2.SectionId = s.SectionId
          INNER JOIN dbo.Room r ON r.RoomId = s2.RoomId
     WHERE CONVERT(DATE, ad.[Date]) BETWEEN CONVERT(DATE, @DateFrom) AND CONVERT(DATE, @DateTo)
     ORDER BY al.AttendanceLogId DESC;
