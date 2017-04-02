USE AMBData
GO

CREATE PROCEDURE StudentSelectById(
	@StudentId int
)
AS
	SET NOCOUNT ON;

	SELECT
		s.StudentId,
		s.FirstName,
		s.MiddleName,
		s.LastName,
		s.Street,
		s.Barangay,
		s.Municipality,
		s.Province,
		s.ContactNumber,
		r.RoomId,
		r.RoomNumber,
		r.RoomName,
		sc.SectionId,
		sc.SectionName
	FROM Student s 
	INNER JOIN StudentSection ss ON ss.StudentId = s.StudentId
	INNER JOIN SectionRoom sr ON sr.SectionId = ss.SectionId
	INNER JOIN Room r ON r.RoomId = sr.RoomId
	INNER JOIN Section sc ON sc.SectionId = ss.StudentId
	WHERE s.StudentId = @StudentId
GO

