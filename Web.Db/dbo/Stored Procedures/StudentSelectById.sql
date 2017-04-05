
CREATE PROCEDURE [dbo].[StudentSelectById](
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
	   s.StudentNumber,
	   s.Status,
	   s.Gender
	FROM Student s 
	WHERE s.StudentId = @StudentId
