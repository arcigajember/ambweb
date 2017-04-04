
CREATE PROCEDURE SectionSelectById(
    @SectionId int
)
AS
    SET NOCOUNT ON;

    SELECT 
	   s.SectionId,
	   s.SectionName
    FROM Section s
    WHERE s.SectionId = @SectionId
