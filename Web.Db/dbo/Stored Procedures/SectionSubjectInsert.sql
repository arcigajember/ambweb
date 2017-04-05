CREATE PROCEDURE [dbo].[SectionSubjectInsert](@SectionId INT,
                                             @SubjectId INT)
AS
     DELETE FROM dbo.SectionSubject
     WHERE dbo.SectionSubject.SectionId = @SectionId
           AND dbo.SectionSubject.SubjectId = @SubjectId;
     INSERT INTO dbo.SectionSubject
     (
     --SectionSubjectId - this column value is auto-generated
     SectionId,
     SubjectId
     )
     VALUES
     (
     -- SectionSubjectId - int
     @SectionId, -- SectionId - int
     @SubjectId -- SubjectId - int
     );