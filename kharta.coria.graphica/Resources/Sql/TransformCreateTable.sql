USE [kharta]
GO

/****** Object:  Table [dbo].[Transform]    Script Date: 8/18/2016 6:35:26 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects where object_id = OBJECT_ID(N'[dbo].[Transform]') AND type in(N'U'))
BEGIN
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



ALTER TABLE [dbo].[Transform] ADD  CONSTRAINT [DF_Transform_IsEnabled]  DEFAULT ((1)) FOR [IsEnabled]
END
GO 

