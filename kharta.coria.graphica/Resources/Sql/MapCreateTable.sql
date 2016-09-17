USE [kharta]
GO

/****** Object:  Table [dbo].[Map]    Script Date: 8/18/2016 7:17:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects where object_id = OBJECT_ID(N'dbo.Map') AND Type in(N'U'))
BEGIN
CREATE TABLE [dbo].[Map](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MapId] [uniqueidentifier] NOT NULL,
	[MapTypeId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[ThumbnailUrl] [nvarchar](512) NULL,
	[SafeName] [nvarchar](256) NULL,
	[MapOptions] [nvarchar](max) NULL,
	[OntologyId] [int] NULL,
	[GroupId] [int] NULL,
	[CreateByUserId] [int] NULL,
	[ModifiedByUserId] [int] NULL,
	[CreateUtcDate] [datetime] NULL,
    [ModifiedUtcDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO