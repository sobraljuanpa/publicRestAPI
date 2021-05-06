USE [BetterCalmDB]
GO
/****** Object:  Table [dbo].[Administrators]    Script Date: 6/5/2021 18:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](450) NULL,
	[Name] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Token] [nvarchar](max) NULL,
 CONSTRAINT [PK_Administrators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/5/2021 18:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Consultations]    Script Date: 6/5/2021 18:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Consultations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientName] [nvarchar](max) NULL,
	[PatientBirthDate] [datetime2](7) NOT NULL,
	[PatientEmail] [nvarchar](max) NULL,
	[PatientPhone] [nvarchar](max) NULL,
	[ProblemId] [int] NOT NULL,
	[PsychologistId] [int] NULL,
	[IsRemote] [bit] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Date] [int] NOT NULL,
 CONSTRAINT [PK_Consultations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayableContentPlaylist]    Script Date: 6/5/2021 18:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayableContentPlaylist](
	[ContentsId] [int] NOT NULL,
	[PlaylistsId] [int] NOT NULL,
 CONSTRAINT [PK_PlayableContentPlaylist] PRIMARY KEY CLUSTERED 
(
	[ContentsId] ASC,
	[PlaylistsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayableContents]    Script Date: 6/5/2021 18:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayableContents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Duration] [float] NOT NULL,
	[Author] [nvarchar](max) NULL,
	[ContentURL] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[ImageURL] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_PlayableContents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Playlists]    Script Date: 6/5/2021 18:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Playlists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[ImageURL] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Playlists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProblemPsychologist]    Script Date: 6/5/2021 18:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProblemPsychologist](
	[ExpertiseId] [int] NOT NULL,
	[SpecialistsId] [int] NOT NULL,
 CONSTRAINT [PK_ProblemPsychologist] PRIMARY KEY CLUSTERED 
(
	[ExpertiseId] ASC,
	[SpecialistsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Problems]    Script Date: 6/5/2021 18:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Problems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](450) NULL,
 CONSTRAINT [PK_Problems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Psychologists]    Script Date: 6/5/2021 18:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Psychologists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PsychologistName] [nvarchar](max) NULL,
	[PsychologistSurname] [nvarchar](max) NULL,
	[IsRemote] [bit] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[ActiveYears] [int] NOT NULL,
	[ScheduleId] [int] NULL,
 CONSTRAINT [PK_Psychologists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 6/5/2021 18:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MondayConsultations] [int] NOT NULL,
	[TuesdayConsultations] [int] NOT NULL,
	[WednesdayConsultations] [int] NOT NULL,
	[ThursdayConsultations] [int] NOT NULL,
	[FridayConsultations] [int] NOT NULL,
 CONSTRAINT [PK_Schedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Consultations]  WITH CHECK ADD  CONSTRAINT [FK_Consultations_Problems_ProblemId] FOREIGN KEY([ProblemId])
REFERENCES [dbo].[Problems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Consultations] CHECK CONSTRAINT [FK_Consultations_Problems_ProblemId]
GO
ALTER TABLE [dbo].[Consultations]  WITH CHECK ADD  CONSTRAINT [FK_Consultations_Psychologists_PsychologistId] FOREIGN KEY([PsychologistId])
REFERENCES [dbo].[Psychologists] ([Id])
GO
ALTER TABLE [dbo].[Consultations] CHECK CONSTRAINT [FK_Consultations_Psychologists_PsychologistId]
GO
ALTER TABLE [dbo].[PlayableContentPlaylist]  WITH CHECK ADD  CONSTRAINT [FK_PlayableContentPlaylist_PlayableContents_ContentsId] FOREIGN KEY([ContentsId])
REFERENCES [dbo].[PlayableContents] ([Id])
GO
ALTER TABLE [dbo].[PlayableContentPlaylist] CHECK CONSTRAINT [FK_PlayableContentPlaylist_PlayableContents_ContentsId]
GO
ALTER TABLE [dbo].[PlayableContentPlaylist]  WITH CHECK ADD  CONSTRAINT [FK_PlayableContentPlaylist_Playlists_PlaylistsId] FOREIGN KEY([PlaylistsId])
REFERENCES [dbo].[Playlists] ([Id])
GO
ALTER TABLE [dbo].[PlayableContentPlaylist] CHECK CONSTRAINT [FK_PlayableContentPlaylist_Playlists_PlaylistsId]
GO
ALTER TABLE [dbo].[PlayableContents]  WITH CHECK ADD  CONSTRAINT [FK_PlayableContents_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlayableContents] CHECK CONSTRAINT [FK_PlayableContents_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Playlists]  WITH CHECK ADD  CONSTRAINT [FK_Playlists_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Playlists] CHECK CONSTRAINT [FK_Playlists_Categories_CategoryId]
GO
ALTER TABLE [dbo].[ProblemPsychologist]  WITH CHECK ADD  CONSTRAINT [FK_ProblemPsychologist_Problems_ExpertiseId] FOREIGN KEY([ExpertiseId])
REFERENCES [dbo].[Problems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProblemPsychologist] CHECK CONSTRAINT [FK_ProblemPsychologist_Problems_ExpertiseId]
GO
ALTER TABLE [dbo].[ProblemPsychologist]  WITH CHECK ADD  CONSTRAINT [FK_ProblemPsychologist_Psychologists_SpecialistsId] FOREIGN KEY([SpecialistsId])
REFERENCES [dbo].[Psychologists] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProblemPsychologist] CHECK CONSTRAINT [FK_ProblemPsychologist_Psychologists_SpecialistsId]
GO
ALTER TABLE [dbo].[Psychologists]  WITH CHECK ADD  CONSTRAINT [FK_Psychologists_Schedules_ScheduleId] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedules] ([Id])
GO
ALTER TABLE [dbo].[Psychologists] CHECK CONSTRAINT [FK_Psychologists_Schedules_ScheduleId]
GO
