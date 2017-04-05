
CREATE PROCEDURE [dbo].[TimeTypeInsert](
    @TimeTypeId int OUTPUT,
    @TimeTypeName varchar(50),
    @IsActive BIT
)
AS
    INSERT dbo.TimeType
    (
        --TimeTypeId - this column value is auto-generated
        TimeTypeName,
	   IsActive
    )
    VALUES
    (
        -- TimeTypeId - int
        @TimeTypeName, -- TimTypeName - varchar
	   @IsActive
    )

    SET @TimeTypeId = SCOPE_IDENTITY();
