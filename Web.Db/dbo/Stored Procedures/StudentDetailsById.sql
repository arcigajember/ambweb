CREATE PROCEDURE [dbo].[StudentDetailsById](@StudentId INT)
AS
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
            s.Gender,
            s1.SectionId,
            s1.SectionName,
            r.RoomId,
            r.RoomNumber,
            r.RoomName
     FROM Student s
          LEFT JOIN Section s1 ON s.SectionId = s1.SectionId
          LEFT JOIN Room r ON r.RoomId = s1.RoomId
     WHERE s.StudentId = @StudentId;
