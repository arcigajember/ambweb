CREATE PROCEDURE [dbo].[AttendanceDetailsInsert](@AttendanceDetailsId INT OUTPUT,
                                                @AttendanceHeaderId  INT,
                                                @TimeTypeId          INT,
                                                @Date                DATETIME,
                                                @Time                DATETIME)
AS
     INSERT INTO dbo.AttendanceDetails
     (
     --AttendanceDetailsId - this column value is auto-generated
     AttendanceHeaderId,
     TimeTypeId,
     [Date],
     [Time]
     )
     VALUES
     (
     -- AttendanceDetailsId - int
     @AttendanceHeaderId, -- AttendanceHeaderId - int
     @TimeTypeId, -- TimeTypeId - int
     @Date, -- DateTime - datetime
     @Time
     );
     SET @AttendanceDetailsId = SCOPE_IDENTITY();