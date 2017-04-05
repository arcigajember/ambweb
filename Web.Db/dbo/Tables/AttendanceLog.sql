CREATE TABLE [dbo].[AttendanceLog] (
    [AttendanceLogId]    INT            IDENTITY (1, 1) NOT NULL,
    [AttendanceHeaderId] INT            NOT NULL,
    [StudentGuardianId]  INT            NULL,
    [ApiResponse]        NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_AttendanceLog] PRIMARY KEY CLUSTERED ([AttendanceLogId] ASC),
    CONSTRAINT [FK_AttendanceLog_AttendanceHeader] FOREIGN KEY ([AttendanceHeaderId]) REFERENCES [dbo].[AttendanceHeader] ([AttendanceHeaderId])
);

