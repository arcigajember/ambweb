
CREATE PROCEDURE [dbo].[StudentSelectByUid](
    @Uid NVARCHAR(50)
)
AS
SET NOCOUNT ON;
SELECT 
      s.StudentId,
	   s.StudentNumber,
	   s.FirstName,
	   s.MiddleName,
	   s.LastName,
	   s.Street,
	   s.Barangay,
	   s.Municipality,
	   s.Province,
	   s.ContactNumber,
	   s.Status,
	   s.Gender,
	   s.Uid,
	   s2.SectionId,
	   s2.SectionName
FROM Student s 
INNER JOIN Section s2 ON s2.SectionId = s.SectionId
WHERE s.Uid = @Uid