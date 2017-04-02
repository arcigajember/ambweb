USE AMBData;
GO
CREATE PROCEDURE SubjectDelete(
    @SubjectId int
)
AS
    UPDATE dbo.Subject
    SET
        --SubjectId - this column value is auto-generated
        dbo.Subject.IsActive = 0
    WHERE dbo.Subject.SubjectId = @SubjectId
GO