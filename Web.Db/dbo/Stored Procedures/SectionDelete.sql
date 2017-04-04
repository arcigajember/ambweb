
CREATE PROCEDURE SectionDelete(
    @SectionId int
)
AS
    SET NOCOUNT ON;

    UPDATE dbo.Section
    SET
        --SectionId - this column value is auto-generated
       dbo.Section.IsActive = 0
    WHERE dbo.Section.SectionId = @SectionId
