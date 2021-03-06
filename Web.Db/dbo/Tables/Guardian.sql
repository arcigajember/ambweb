﻿CREATE TABLE [dbo].[Guardian] (
    [GuardianId]    INT          IDENTITY (1, 1) NOT NULL,
    [FirstName]     VARCHAR (10) NOT NULL,
    [LastName]      VARCHAR (10) NOT NULL,
    [MiddleName]    VARCHAR (10) NULL,
    [Street]        VARCHAR (50) NOT NULL,
    [Barangay]      VARCHAR (50) NOT NULL,
    [Municipality]  VARCHAR (50) NOT NULL,
    [Province]      VARCHAR (50) NOT NULL,
    [ContactNumber] VARCHAR (50) NOT NULL,
    [IsActive]      BIT          NOT NULL,
    CONSTRAINT [PK_Guardian] PRIMARY KEY CLUSTERED ([GuardianId] ASC)
);



