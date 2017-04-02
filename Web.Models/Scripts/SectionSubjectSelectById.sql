USE AMBData
GO

ALTER PROCEDURE SectionSubjectSelectById(
    @SectionId int
)
AS
    SET NOCOUNT ON;

    SELECT 
	   s.SectionId,
	   s.SectionName,
	   s2.SubjectId,
	   s2.SubjectName,
	   s2.Description
    FROM Section s 
    INNER JOIN SectionSubject ss ON ss.SectionId = s.SectionId
    INNER JOIN Subject s2 ON s2.SubjectId = ss.SubjectId
    WHERE s.SectionId = @SectionId
GO