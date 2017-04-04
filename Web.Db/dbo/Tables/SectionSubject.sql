CREATE TABLE [dbo].[SectionSubject] (
    [SectionSubjectId] INT IDENTITY (1, 1) NOT NULL,
    [SectionId]        INT NOT NULL,
    [SubjectId]        INT NOT NULL,
    CONSTRAINT [PK_SectionSubject] PRIMARY KEY CLUSTERED ([SectionSubjectId] ASC),
    CONSTRAINT [FK_SectionSubject_Section] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Section] ([SectionId]),
    CONSTRAINT [FK_SectionSubject_Subject] FOREIGN KEY ([SubjectId]) REFERENCES [dbo].[Subject] ([SubjectId])
);

