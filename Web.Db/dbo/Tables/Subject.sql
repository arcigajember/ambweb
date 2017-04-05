CREATE TABLE [dbo].[Subject] (
    [SubjectId]   INT          IDENTITY (1, 1) NOT NULL,
    [SubjectName] VARCHAR (50) NOT NULL,
    [Description] VARCHAR (50) NOT NULL,
    [IsActive]    BIT          NOT NULL,
    CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED ([SubjectId] ASC)
);

