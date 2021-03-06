/*    ==Paramètres de script==

    Version du serveur source : SQL Server 2014 (12.0.2269)
    Édition du moteur de base de données source : Microsoft SQL Server Express Edition
    Type du moteur de base de données source : SQL Server autonome

    Version du serveur cible : SQL Server 2017
    Édition du moteur de base de données cible : Microsoft SQL Server Standard Edition
    Type du moteur de base de données cible : SQL Server autonome
*/
USE [MskManager]
GO
/****** Object:  Table [dbo].[DeezerUser]    Script Date: 09/10/2017 23:17:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeezerUser](
	[Id] [int] NOT NULL,
	[AccessToken] [text] NOT NULL,
	[Creation] [datetime] NOT NULL,
 CONSTRAINT [PK_DeezerUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[DeezerUser] ([Id], [AccessToken], [Creation]) VALUES (4934039, N'frOOFTbEmf564c722d48541i6T7kMi6564c722d4857bXbmMyV', CAST(N'2017-10-09T21:13:01.647' AS DateTime))
/****** Object:  StoredProcedure [dbo].[AddDeezerUser]    Script Date: 09/10/2017 23:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[AddDeezerUser] 
	-- Add the parameters for the stored procedure here
	@id int,
	@accessToken text
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into DeezerUser(Id, AccessToken, Creation)
	values(@id, @accessToken, GETUTCDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[DeezerUserExists]    Script Date: 09/10/2017 23:17:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeezerUserExists] 
	-- Add the parameters for the stored procedure here
	@id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select Case When Exists (
      select usr.Id
      from DeezerUser usr
      where usr.Id = @Id
   )
   Then Cast(1 as bit)
   Else Cast(0 as bit) 
   End
END
GO
