USE [kharta]
GO

/****** Object:  Table [dbo].[MapBook]    Script Date: 8/18/2016 6:52:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects where object_id = OBJECT_ID(N'dbo.MapBook') AND type in(N'U'))
BEGIN
CREATE TABLE [dbo].[MapBook](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationTypeId] [uniqueidentifier] NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[AvatarUrl] [nvarchar](512) NULL,
	[IsEnabled] [bit] NULL,
	[Url] [nvarchar](512) NULL,
	[OntologyId] [int] NULL CONSTRAINT [DF_MapBook_OntologyId]  DEFAULT ((1)),
	[GroupId] [int] NOT NULL,
	[SafeName] [nvarchar](256) NULL,
 CONSTRAINT [PK_MapBook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
 
