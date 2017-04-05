
CREATE PROCEDURE TimeTypeDelete(
    @TimeTypeId int
)
AS
    UPDATE dbo.TimeType
    SET
        --TimeTypeId - this column value is auto-generated
        dbo.TimeType.IsActive = 0
    WHERE dbo.TimeType.TimeTypeId = @TimeTypeId
