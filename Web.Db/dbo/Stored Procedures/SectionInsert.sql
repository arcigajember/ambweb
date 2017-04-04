
CREATE PROCEDURE [dbo].[SectionInsert](
    @SectionId int OUTPUT,
    @SectionName nvarchar(50)
)
AS
    SET NOCOUNT ON;

    INSERT INTO dbo.Section
    (
        --SectionId - this column value is auto-generated
        SectionName,
	   IsActive
    )
    VALUES
    (
        -- SectionId - int
        @SectionName, -- SectionName - varchar
	   1
    )

    SET @SectionId = SCOPE_IDENTITY();
