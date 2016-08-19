USE [kharta]
GO

/****** Object:  Table [dbo].[Ontology]    Script Date: 8/18/2016 6:15:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ontology]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ontology](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContainerTypeId] [uniqueidentifier] NOT NULL,
	[ContainerId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[AvatarUrl] [nvarchar](512) NULL,
	[IsEnabled] [bit] NULL CONSTRAINT [DF_Ontology_IsEnabled]  DEFAULT ((0)),
	[Url] [nvarchar](512) NULL,
	[ParentOntologyId] [int] NULL CONSTRAINT [DF_Ontology_ParentOntologyId]  DEFAULT ((1)),
	[SafeName] [nvarchar](256) NULL,
	[GroupId] [int] NULL,
 CONSTRAINT [PK_Ontology] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO 
 