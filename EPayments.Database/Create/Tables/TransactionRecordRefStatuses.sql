﻿PRINT 'TransactionRecordRefStatuses'
GO

CREATE TABLE [dbo].[TransactionRecordRefStatuses](
    [TransactionRecordRefStatusId]  INT             NOT NULL IDENTITY,
    [Alias]                         NVARCHAR(50)    NOT NULL,
    [Name]                          NVARCHAR(100)   NOT NULL,
    [Description]                   NVARCHAR(MAX)   NULL,
 CONSTRAINT [PK_TransactionRecordRefStatuses] PRIMARY KEY ([TransactionRecordRefStatusId])
);

GO
