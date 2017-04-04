CREATE PROCEDURE TimeTypeCheck(@StudentId INT)
AS
     DECLARE @TimeType INT;
     SET @TimeType =
     (
         SELECT COUNT(*)
         FROM AttendanceSection ats
              INNER JOIN StudentGuardian sg ON sg.StudentGuardianId = ats.StudentGuardianId
         WHERE sg.StudentId = @StudentId
               AND CONVERT(DATE, ats.Date) = CONVERT(DATE, GETDATE())
     );
     IF @TimeType = 0
         BEGIN
             SET NOCOUNT ON;
             SELECT tt.TimeTypeId
             FROM dbo.TimeType tt
             WHERE tt.TimeTypeId = 1;
         END;
     ELSE
     IF @TimeType = 1
         BEGIN
             SET NOCOUNT ON;
             SELECT tt.TimeTypeId
             FROM dbo.TimeType tt
             WHERE tt.TimeTypeId = 2;
         END;