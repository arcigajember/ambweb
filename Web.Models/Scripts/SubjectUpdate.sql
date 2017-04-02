USE AMBData
GO

CREATE PROCEDURE SubjectUpdate(
    @SubjectId int,
    @SubjectName varchar(50),
    @Description varchar(50)
)
AS
    UPDATE dbo.Subject
    SET
        --SubjectId - this column value is auto-generated
        dbo.Subject.SubjectName = @Subjectname, -- varchar
        dbo.Subject.Description = @Description -- varchar
    WHERE dbo.Subject.SubjectId = @SubjectId
GO