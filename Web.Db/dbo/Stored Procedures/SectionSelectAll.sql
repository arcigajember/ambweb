
CREATE PROCEDURE [dbo].[SectionSelectAll]
AS
    SET NOCOUNT ON;

    SELECT 
	   s.SectionId,
	   s.SectionName
    FROM Section s
    WHERE s.IsActive = 1