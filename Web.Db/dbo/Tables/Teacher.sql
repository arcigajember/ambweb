CREATE TABLE [dbo].[Teacher] (
    [TeacherId]  INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]  VARCHAR (10)  NOT NULL,
    [MiddleName] VARCHAR (10)  NULL,
    [LastName]   VARCHAR (10)  NOT NULL,
    [Address]    VARCHAR (MAX) NOT NULL,
    [IsActive]   BIT           NOT NULL,
    CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED ([TeacherId] ASC)
);



