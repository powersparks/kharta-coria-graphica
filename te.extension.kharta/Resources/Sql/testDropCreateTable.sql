USE [kharta]
GO
IF   EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[test]') AND type in (N'U'))
BEGIN
/****** Object:  Table [dbo].[test]    Script Date: 8/18/2016 5:08:45 AM ******/
DROP TABLE [dbo].[test]
END
GO

/****** Object:  Table [dbo].[test]    Script Date: 8/18/2016 5:08:45 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[test1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[test1](
	[id] [int] NOT NULL,
	[attr] [nchar](10) NULL
) ON [PRIMARY]
END
GO


