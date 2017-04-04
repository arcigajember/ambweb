CREATE PROCEDURE [dbo].[SectionSubjectSelectById](@SectionId INT)
AS
     SET NOCOUNT ON;
     SELECT s.SectionId,
            s.SectionName,
            s.RoomId
     FROM dbo.Section s
     WHERE s.SectionId = @SectionId;
     SELECT s.SubjectId,
            s.SubjectName,
            s.Description
     FROM Subject s
          INNER JOIN SectionSubject ss ON ss.SubjectId = s.SubjectId
     WHERE ss.SectionId = @SectionId;