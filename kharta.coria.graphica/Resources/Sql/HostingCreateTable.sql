﻿USE [kharta]
GO

/****** Object:  Table [dbo].[Hosting]    Script Date: 8/18/2016 7:17:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Hosting]') AND type in(N'U'))
BEGIN
CREATE TABLE [dbo].[Hosting](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationTypeId] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[AvatarUrl] [nvarchar](512) NULL,
	[IsEnabled] [bit] NULL,
	[Url] [nvarchar](512) NULL,
	[TransformId] [int] NULL,
	[GroupId] [int] NULL,
	[SafeName] [nvarchar](256) NULL,
 CONSTRAINT [PK_Hosting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
 

