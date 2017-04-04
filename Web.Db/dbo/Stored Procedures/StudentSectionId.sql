
CREATE PROCEDURE StudentSectionId(
    @StudentId INT
)
AS 
SET NOCOUNT ON;
SELECT 
    s.SectionId,
    s.SectionName
FROM Section s 
INNER JOIN Student s2 ON s2.SectionId = s.SectionId
