
CREATE PROCEDURE TeacherScheduleInsert(
    @TeacherId INT,
    @SubjectId INT,
    @SectionId INT
)
AS
    INSERT dbo.TeacherSchedule
    (
        --TeacherScheduleId - this column value is auto-generated
        SubjectId,
        SectionId,
        TeacherId
    )
    VALUES
    (
        -- TeacherScheduleId - int
        @SubjectId, -- SubjectId - int
        @SectionId, -- SectionId - int
        @TeacherId -- TeacherId - int
    )
