CREATE TABLE [dbo].[Room] (
    [RoomId]     INT          IDENTITY (1, 1) NOT NULL,
    [RoomNumber] VARCHAR (50) NOT NULL,
    [RoomName]   VARCHAR (50) NOT NULL,
    [IsActive]   BIT          NOT NULL,
    CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([RoomId] ASC)
);

