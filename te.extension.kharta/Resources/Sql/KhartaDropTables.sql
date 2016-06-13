USE [kharta]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Ontology', @level2type=N'CONSTRAINT',@level2name=N'FK_Ontology_Ontology'

GO

ALTER TABLE [dbo].[Ontology] DROP CONSTRAINT [FK_Ontology_Ontology]
GO

/****** Object:  Table [dbo].[Ontology]    Script Date: 6/11/2016 5:21:08 PM ******/
DROP TABLE [dbo].[Ontology]
GO
USE [kharta]
GO

/****** Object:  Table [dbo].[Hosting]    Script Date: 6/11/2016 5:30:24 PM ******/
DROP TABLE [dbo].[Hosting]
GO
 

ALTER TABLE [dbo].[Source] DROP CONSTRAINT [FK_Source_Ontology1]
GO

ALTER TABLE [dbo].[Source] DROP CONSTRAINT [DF_Source_OntologyId]
GO

/****** Object:  Table [dbo].[Source]    Script Date: 6/11/2016 5:31:20 PM ******/
DROP TABLE [dbo].[Source]
GO

USE [kharta]
GO

ALTER TABLE [dbo].[Transform] DROP CONSTRAINT [FK_Transform_Transform]
GO

ALTER TABLE [dbo].[Transform] DROP CONSTRAINT [FK_Transform_Source]
GO

ALTER TABLE [dbo].[Transform] DROP CONSTRAINT [FK_Transform_Hosting]
GO

ALTER TABLE [dbo].[Transform] DROP CONSTRAINT [DF_Transform_IsEnabled]
GO

/****** Object:  Table [dbo].[Transform]    Script Date: 6/11/2016 5:33:00 PM ******/
DROP TABLE [dbo].[Transform]
GO
