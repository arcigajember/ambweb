CREATE TABLE [dbo].[TimeType] (
    [TimeTypeId]   INT          IDENTITY (1, 1) NOT NULL,
    [TimeTypeName] VARCHAR (50) NOT NULL,
    [IsActive]     BIT          NOT NULL,
    CONSTRAINT [PK_TimeType] PRIMARY KEY CLUSTERED ([TimeTypeId] ASC)
);

