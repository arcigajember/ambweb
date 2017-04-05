CREATE PROCEDURE [dbo].[AttendanceToday]
AS
     SELECT s.StudentId,
            s.FirstName,
            s.MiddleName,
            s.LastName,
            s.Street,
            s.Barangay,
            s.Municipality,
            s.Province,
            s2.SectionId,
            s2.SectionName
     FROM dbo.Student s
          INNER JOIN Section s2 ON s2.SectionId = s.SectionId
          INNER JOIN AttendanceHeader ah ON ah.StudentId = s.StudentId
          INNER JOIN dbo.AttendanceDetails ad ON ad.AttendanceHeaderId = ah.AttendanceHeaderId
     WHERE CONVERT(DATE, ad.[Date]) = CONVERT(DATE, GETDATE())
     ORDER BY ad.[Date] DESC;
