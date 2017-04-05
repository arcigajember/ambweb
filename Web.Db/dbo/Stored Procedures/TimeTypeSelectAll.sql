
CREATE PROCEDURE [dbo].[TimeTypeSelectAll]
AS
   SET NOCOUNT ON;
    
    SELECT 
	   t.TimeTypeId,
	   t.TimeTypeName
    FROM TimeType t
    WHERE t.IsActive = 1