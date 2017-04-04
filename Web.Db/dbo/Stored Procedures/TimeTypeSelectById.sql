
CREATE PROCEDURE TimeTypeSelectById(
    @TimeTypeId int
)
AS
   SET NOCOUNT ON;
    
    SELECT 
	   t.TimeTypeId,
	   t.TimeTypeName
    FROM TimeType t
    WHERE t.TimeTypeId = @TimeTypeId
