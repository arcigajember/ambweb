CREATE TABLE [dbo].[TeacherSchedule] (
    [TeacherScheduleId] INT IDENTITY (1, 1) NOT NULL,
    [SubjectId]         INT NOT NULL,
    [SectionId]         INT NOT NULL,
    [TeacherId]         INT NOT NULL,
    CONSTRAINT [PK_TeacherSchedule] PRIMARY KEY CLUSTERED ([TeacherScheduleId] ASC),
    CONSTRAINT [FK_TeacherSchedule_Section] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Section] ([SectionId]),
    CONSTRAINT [FK_TeacherSchedule_Subject] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject] ([SubjectId]),
    CONSTRAINT [FK_TeacherSchedule_Teacher] FOREIGN KEY ([TeacherId]) REFERENCES [dbo].[Teacher] ([TeacherId])
);

