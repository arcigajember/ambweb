CREATE TABLE [dbo].[StudentGuardian] (
    [StudentGuardianId] INT           IDENTITY (1, 1) NOT NULL,
    [StudentId]         INT           NOT NULL,
    [GuardianId]        INT           NOT NULL,
    [Relation]          VARCHAR (MAX) NOT NULL,
    [IsActive]          BIT           NOT NULL,
    CONSTRAINT [PK_StudentGuardian] PRIMARY KEY CLUSTERED ([StudentGuardianId] ASC),
    CONSTRAINT [FK_StudentGuardian_Guardian] FOREIGN KEY ([GuardianId]) REFERENCES [dbo].[Guardian] ([GuardianId]),
    CONSTRAINT [FK_StudentGuardian_Student] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([StudentId])
);

