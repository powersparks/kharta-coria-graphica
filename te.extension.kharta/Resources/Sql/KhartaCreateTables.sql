/****** Object:  Table [dbo].[Ontology]    Script Date: 6/11/2016 5:21:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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
 CONSTRAINT [PK_Ontology] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Ontology]  WITH NOCHECK ADD  CONSTRAINT [FK_Ontology_Ontology] FOREIGN KEY([ParentOntologyId])
REFERENCES [dbo].[Ontology] ([Id])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Ontology] NOCHECK CONSTRAINT [FK_Ontology_Ontology]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'For each Ontology there are Zero or more Ontologies' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Ontology', @level2type=N'CONSTRAINT',@level2name=N'FK_Ontology_Ontology'
GO

/****** Object:  Table [dbo].[Hosting]    Script Date: 6/11/2016 5:30:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

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

GO



/****** Object:  Table [dbo].[Source]    Script Date: 6/11/2016 5:31:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Source](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationTypeId] [uniqueidentifier] NOT NULL,
	[ApplicationId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[AvatarUrl] [nvarchar](512) NULL,
	[IsEnabled] [bit] NULL,
	[Url] [nvarchar](512) NULL,
	[OntologyId] [int] NULL,
	[GroupId] [int] NULL,
	[SafeName] [nvarchar](256) NULL,
 CONSTRAINT [PK_Source] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Source] ADD  CONSTRAINT [DF_Source_OntologyId]  DEFAULT ((1)) FOR [OntologyId]
GO

ALTER TABLE [dbo].[Source]  WITH NOCHECK ADD  CONSTRAINT [FK_Source_Ontology1] FOREIGN KEY([OntologyId])
REFERENCES [dbo].[Ontology] ([Id])
ON DELETE SET DEFAULT
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Source] NOCHECK CONSTRAINT [FK_Source_Ontology1]
GO




/****** Object:  Table [dbo].[Transform]    Script Date: 6/11/2016 5:33:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transform](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationTypeId] [uniqueidentifier] NULL,
	[ApplicationId] [uniqueidentifier] NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](512) NULL,
	[AvatarUrl] [nvarchar](512) NULL,
	[IsEnabled] [bit] NULL,
	[Url] [nvarchar](512) NULL,
	[SourceId] [int] NULL,
	[HostingId] [int] NULL,
	[TransformId] [int] NULL,
	[SafeName] [nvarchar](256) NULL,
 CONSTRAINT [PK_Transform] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Transform] ADD  CONSTRAINT [DF_Transform_IsEnabled]  DEFAULT ((1)) FOR [IsEnabled]
GO

ALTER TABLE [dbo].[Transform]  WITH NOCHECK ADD  CONSTRAINT [FK_Transform_Hosting] FOREIGN KEY([HostingId])
REFERENCES [dbo].[Hosting] ([Id])
ON DELETE CASCADE
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Transform] NOCHECK CONSTRAINT [FK_Transform_Hosting]
GO

ALTER TABLE [dbo].[Transform]  WITH NOCHECK ADD  CONSTRAINT [FK_Transform_Source] FOREIGN KEY([SourceId])
REFERENCES [dbo].[Source] ([Id])
ON DELETE CASCADE
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Transform] NOCHECK CONSTRAINT [FK_Transform_Source]
GO

ALTER TABLE [dbo].[Transform]  WITH NOCHECK ADD  CONSTRAINT [FK_Transform_Transform] FOREIGN KEY([TransformId])
REFERENCES [dbo].[Transform] ([Id])
NOT FOR REPLICATION 
GO

ALTER TABLE [dbo].[Transform] NOCHECK CONSTRAINT [FK_Transform_Transform]
GO





