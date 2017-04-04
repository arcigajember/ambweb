
CREATE PROCEDURE TimeTypeUpdate(
    @TimeTypeId int,
    @TimeTypeName varchar(50)
)
AS
    UPDATE dbo.TimeType
    SET
        --TimeTypeId - this column value is auto-generated
        dbo.TimeType.TimeTypeName = @TimeTypeName -- varchar 
    WHERE dbo.TimeType.TimeTypeId = @TimeTypeId
