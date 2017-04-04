CREATE TABLE [dbo].[Section] (
    [SectionId]   INT          IDENTITY (1, 1) NOT NULL,
    [RoomId]      INT          NULL,
    [SectionName] VARCHAR (50) NOT NULL,
    [IsActive]    BIT          NOT NULL,
    CONSTRAINT [PK_Section] PRIMARY KEY CLUSTERED ([SectionId] ASC),
    CONSTRAINT [FK_Section_Room] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Room] ([RoomId])
);

