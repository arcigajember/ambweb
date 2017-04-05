
CREATE PROCEDURE [dbo].[AttendanceLogInsert](
    @AttendanceHeaderId INT,
    @StudentGuardianId INT,
    @ApiResponse NVARCHAR(MAX)
)
AS
    INSERT dbo.AttendanceLog
    (
        --AttendanceLogId - this column value is auto-generated
        AttendanceHeaderId,
        StudentGuardianId,
        ApiResponse
    )
    VALUES
    (
        -- AttendanceLogId - int
        @AttendanceHeaderId, -- AttendanceDetailsId - int
        @StudentGuardianId, -- StudentGuardianId - int
        @ApiResponse -- ApiResponse - nvarchar
    )
