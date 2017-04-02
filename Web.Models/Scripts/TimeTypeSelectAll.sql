USE AMBData
GO

CREATE PROCEDURE TimeTypeSelectAll
AS
   SET NOCOUNT ON;
    
    SELECT 
	   t.TimeTypeId,
	   t.TimeTypeName
    FROM TimeType t
GO