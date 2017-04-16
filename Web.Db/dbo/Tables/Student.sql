CREATE TABLE [dbo].[Student] (
    [StudentId]     INT            IDENTITY (1, 1) NOT NULL,
    [SectionId]     INT            NULL,
    [StudentNumber] VARCHAR (50)   NOT NULL,
    [FirstName]     VARCHAR (10)   NOT NULL,
    [LastName]      VARCHAR (10)   NOT NULL,
    [MiddleName]    VARCHAR (10)   NULL,
    [Street]        VARCHAR (50)   NOT NULL,
    [Barangay]      VARCHAR (50)   NOT NULL,
    [Municipality]  VARCHAR (50)   NOT NULL,
    [Province]      VARCHAR (50)   NOT NULL,
    [ContactNumber] VARCHAR (50)   NOT NULL,
    [Status]        VARCHAR (10)   NOT NULL,
    [Gender]        VARCHAR (50)   NOT NULL,
    [IsActive]      BIT            NOT NULL,
    [Uid]           NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED ([StudentId] ASC),
    CONSTRAINT [FK_Student_Section] FOREIGN KEY ([SectionId]) REFERENCES [dbo].[Section] ([SectionId])
);



