CREATE TABLE [dbo].[AttendanceHeader] (
    [AttendanceHeaderId] INT IDENTITY (1, 1) NOT NULL,
    [StudentId]          INT NOT NULL,
    CONSTRAINT [PK_AttendanceHeader] PRIMARY KEY CLUSTERED ([AttendanceHeaderId] ASC),
    CONSTRAINT [FK_AttendanceHeader_Student] FOREIGN KEY ([StudentId]) REFERENCES [dbo].[Student] ([StudentId])
);

