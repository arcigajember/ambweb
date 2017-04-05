CREATE TABLE [dbo].[AuditLog] (
    [AuditId]      UNIQUEIDENTIFIER NOT NULL,
    [UserName]     NVARCHAR (50)    NOT NULL,
    [AreaAccessed] NVARCHAR (MAX)   NOT NULL,
    [Timeaccessed] DATETIME         NOT NULL,
    [IPAddress]    NVARCHAR (MAX)   NOT NULL,
    [Parameters]   NVARCHAR (MAX)   NOT NULL
);

