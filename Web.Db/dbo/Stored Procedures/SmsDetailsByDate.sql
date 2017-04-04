CREATE PROCEDURE SmsDetailsByDate(@DateFrom DATETIME,
                                  @DateTo   DATETIME)
AS
     SET NOCOUNT ON;
     SELECT sd.SmsDetailsId,
            sd.SmsType,
            sd.DateSent,
            sd.[Status],
            sd.[Message],
            anu.Id,
            anu.UserName,
            s.StudentId,
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
            s.Gender,
            s1.SectionId,
            s1.SectionName,
            r.RoomId,
            r.RoomNumber,
            r.RoomName,
            g.GuardianId,
            g.FirstName,
            g.LastName,
            g.MiddleName,
            g.Street,
            g.Barangay,
            g.Municipality,
            g.Province,
            g.ContactNumber
     FROM dbo.SmsDetails sd
          INNER JOIN dbo.AspNetUsers anu ON anu.Id = sd.AspNetUsersId
          INNER JOIN dbo.StudentGuardian sg ON sg.StudentGuardianId = sd.StudentGuardianId
          INNER JOIN dbo.Student s ON s.StudentId = sg.StudentId
          INNER JOIN dbo.Guardian g ON g.GuardianId = sg.GuardianId
          INNER JOIN dbo.Section s1 ON s1.SectionId = s.SectionId
          INNER JOIN dbo.Room r ON r.RoomId = s1.RoomId
     WHERE CONVERT(DATE, sd.DateSent) BETWEEN CONVERT(DATE, @DateFrom) AND CONVERT(DATE, @DateTo)
	ORDER BY sd.SmsDetailsId DESC;
