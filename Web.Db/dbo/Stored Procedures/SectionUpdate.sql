
CREATE PROCEDURE SectionUpdate(
    @SectionId int,
    @SectionName nvarchar(50)
)
AS
    SET NOCOUNT ON;

    UPDATE dbo.Section
    SET
        --SectionId - this column value is auto-generated
        dbo.Section.SectionName = @SectionName -- varchar
    WHERE dbo.Section.SectionId = @SectionId
