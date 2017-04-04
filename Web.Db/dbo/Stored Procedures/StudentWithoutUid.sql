 
 CREATE PROCEDURE StudentWithoutUid
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
	   s.Uid
FROM Student s 
WHERE s.Uid IS NULL