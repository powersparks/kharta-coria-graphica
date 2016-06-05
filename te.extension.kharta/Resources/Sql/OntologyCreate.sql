USE [kharta]
GO
 
/****** Object:  Table [dbo].[Ontology]    Script Date: 6/7/2016 6:37:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ontology](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContainerTypeId] [uniqueidentifier] NULL,
	[ContainerId] [uniqueidentifier] NULL,
	[Name] [nvarchar](256) NULL,
	[Description] [nvarchar](512) NULL,
	[AvatarUrl] [nvarchar](512) NULL,
	[IsEnabled] [bit] NULL,
	[Url] [nvarchar](512) NULL,
	[ParentOntologyId] [int] NULL,
	[SafeName] [nchar](256) NULL,
 CONSTRAINT [PK_Ontology] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


