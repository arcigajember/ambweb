CREATE PROCEDURE AttendanceHeaderInsert(@StudentId          INT,
                                        @AttendanceHeaderId INT OUTPUT)
AS
     INSERT INTO dbo.AttendanceHeader
     (
     --AttendanceHeaderId - this column value is auto-generated
     StudentId
     )
     VALUES
     (
     -- AttendanceHeaderId - int
     @StudentId -- StudentId - int
     );
     SET @AttendanceHeaderId = SCOPE_IDENTITY();
