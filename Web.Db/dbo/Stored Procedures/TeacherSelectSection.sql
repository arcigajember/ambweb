CREATE PROCEDURE [dbo].[TeacherSelectSection](@TeacherId INT,
                                             @SubjectId INT)
AS
     SET NOCOUNT ON;
     SELECT s.SectionId,
            s.SectionName,
            r.RoomId,
            r.RoomNumber,
            r.RoomName
     FROM Section s
          INNER JOIN TeacherSchedule ts ON ts.SectionId = s.SectionId
          INNER JOIN Room r ON r.RoomId = s.RoomId
     WHERE ts.TeacherId = @TeacherId
           AND ts.SubjectId = @SubjectId;