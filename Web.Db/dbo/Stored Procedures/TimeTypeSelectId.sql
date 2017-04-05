CREATE PROCEDURE TimeTypeSelectId(@TimeTypeId INT)
AS
     SET NOCOUNT ON;
     SELECT tt.TimeTypeId,
            tt.TimeTypeName
     FROM dbo.TimeType tt
     WHERE tt.TimeTypeId = @TimeTypeId;