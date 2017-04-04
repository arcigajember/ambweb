CREATE TABLE [dbo].[SmsDetails] (
    [SmsDetailsId]      INT            IDENTITY (1, 1) NOT NULL,
    [SmsType]           NVARCHAR (50)  NOT NULL,
    [StudentGuardianId] INT            NOT NULL,
    [AspNetUsersId]     NVARCHAR (128) NOT NULL,
    [DateSent]          DATETIME       NOT NULL,
    [Status]            NVARCHAR (MAX) NOT NULL,
    [Message]           NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_SmsDetails] PRIMARY KEY CLUSTERED ([SmsDetailsId] ASC),
    CONSTRAINT [FK_SmsDetails_AspNetUsers] FOREIGN KEY ([AspNetUsersId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_SmsDetails_StudentGuardian] FOREIGN KEY ([StudentGuardianId]) REFERENCES [dbo].[StudentGuardian] ([StudentGuardianId])
);

