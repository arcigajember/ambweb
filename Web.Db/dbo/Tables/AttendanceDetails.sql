CREATE TABLE [dbo].[AttendanceDetails] (
    [AttendanceDetailsId] INT      IDENTITY (1, 1) NOT NULL,
    [AttendanceHeaderId]  INT      NOT NULL,
    [TimeTypeId]          INT      NOT NULL,
    [Time]                DATETIME NOT NULL,
    [Date]                DATETIME NOT NULL,
    CONSTRAINT [PK_AttendanceDetails] PRIMARY KEY CLUSTERED ([AttendanceDetailsId] ASC),
    CONSTRAINT [FK_AttendanceDetails_AttendanceHeader] FOREIGN KEY ([AttendanceHeaderId]) REFERENCES [dbo].[AttendanceHeader] ([AttendanceHeaderId]),
    CONSTRAINT [FK_AttendanceDetails_TimeType] FOREIGN KEY ([TimeTypeId]) REFERENCES [dbo].[TimeType] ([TimeTypeId])
);

