USE [master]
GO
/****** Object:  Database [BetterCalmDB]    Script Date: 17/6/2021 6:08:40 ******/
CREATE DATABASE [BetterCalmDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BetterCalmDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\BetterCalmDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BetterCalmDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\BetterCalmDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BetterCalmDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BetterCalmDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BetterCalmDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BetterCalmDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BetterCalmDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BetterCalmDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BetterCalmDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BetterCalmDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BetterCalmDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BetterCalmDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BetterCalmDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BetterCalmDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BetterCalmDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BetterCalmDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BetterCalmDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BetterCalmDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BetterCalmDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BetterCalmDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BetterCalmDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BetterCalmDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BetterCalmDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BetterCalmDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BetterCalmDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BetterCalmDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BetterCalmDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BetterCalmDB] SET  MULTI_USER 
GO
ALTER DATABASE [BetterCalmDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BetterCalmDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BetterCalmDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BetterCalmDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BetterCalmDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BetterCalmDB] SET QUERY_STORE = OFF
GO
USE [BetterCalmDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17/6/2021 6:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Administrators]    Script Date: 17/6/2021 6:08:41 ******/
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
/****** Object:  Table [dbo].[Categories]    Script Date: 17/6/2021 6:08:41 ******/
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
/****** Object:  Table [dbo].[Consultations]    Script Date: 17/6/2021 6:08:41 ******/
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
	[Duration] [int] NOT NULL,
	[Bonus] [int] NOT NULL,
	[Cost] [int] NOT NULL,
 CONSTRAINT [PK_Consultations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayableContentPlaylist]    Script Date: 17/6/2021 6:08:41 ******/
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
/****** Object:  Table [dbo].[PlayableContents]    Script Date: 17/6/2021 6:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayableContents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageURL] [nvarchar](max) NULL,
	[Duration] [float] NOT NULL,
	[Author] [nvarchar](max) NULL,
	[ContentURL] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_PlayableContents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Playlists]    Script Date: 17/6/2021 6:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Playlists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImageURL] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Playlists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlaylistVideoContent]    Script Date: 17/6/2021 6:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlaylistVideoContent](
	[PlaylistsId] [int] NOT NULL,
	[VideosId] [int] NOT NULL,
 CONSTRAINT [PK_PlaylistVideoContent] PRIMARY KEY CLUSTERED 
(
	[PlaylistsId] ASC,
	[VideosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProblemPsychologist]    Script Date: 17/6/2021 6:08:41 ******/
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
/****** Object:  Table [dbo].[Problems]    Script Date: 17/6/2021 6:08:41 ******/
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
/****** Object:  Table [dbo].[Psychologists]    Script Date: 17/6/2021 6:08:41 ******/
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
	[ScheduleId] [int] NOT NULL,
	[Fee] [int] NOT NULL,
 CONSTRAINT [PK_Psychologists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 17/6/2021 6:08:41 ******/
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
/****** Object:  Table [dbo].[VideoContents]    Script Date: 17/6/2021 6:08:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoContents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Duration] [float] NOT NULL,
	[Author] [nvarchar](max) NULL,
	[VideoURL] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_VideoContents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210615234211_CreateDB', N'5.0.4')
GO
SET IDENTITY_INSERT [dbo].[Administrators] ON 

INSERT [dbo].[Administrators] ([Id], [Email], [Name], [Password], [Token]) VALUES (1, N'admin@admin.admin', NULL, N'admin', NULL)
INSERT [dbo].[Administrators] ([Id], [Email], [Name], [Password], [Token]) VALUES (2, N'chiara@chiara.chiara', N'chiara', N'chiara', NULL)
INSERT [dbo].[Administrators] ([Id], [Email], [Name], [Password], [Token]) VALUES (3, N'juan@juan.juan', N'juan', N'juan', NULL)
INSERT [dbo].[Administrators] ([Id], [Email], [Name], [Password], [Token]) VALUES (4, N'sofia@sofia.sofia', N'sofia', N'sofia', NULL)
INSERT [dbo].[Administrators] ([Id], [Email], [Name], [Password], [Token]) VALUES (5, N'nina@nina.nina', N'nina', N'nina', NULL)
SET IDENTITY_INSERT [dbo].[Administrators] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (1, N'Dormir')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (2, N'Meditar')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'Musica')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (4, N'Cuerpo')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Consultations] ON 

INSERT [dbo].[Consultations] ([Id], [PatientName], [PatientBirthDate], [PatientEmail], [PatientPhone], [ProblemId], [PsychologistId], [IsRemote], [Address], [Date], [Duration], [Bonus], [Cost]) VALUES (1, N'Lucia', CAST(N'1998-06-19T00:00:00.0000000' AS DateTime2), N'lucia@lucia.lucia', N'099887766', 1, 1, 0, N'18 de Julio 1111', 0, 1, 50, 500)
INSERT [dbo].[Consultations] ([Id], [PatientName], [PatientBirthDate], [PatientEmail], [PatientPhone], [ProblemId], [PsychologistId], [IsRemote], [Address], [Date], [Duration], [Bonus], [Cost]) VALUES (2, N'Nicolas', CAST(N'1998-05-11T00:00:00.0000000' AS DateTime2), N'nicolas@nicolas.nicolas', N'099776655', 1, 4, 0, N'Av. Italia 6677', 4, 1, 15, 1700)
INSERT [dbo].[Consultations] ([Id], [PatientName], [PatientBirthDate], [PatientEmail], [PatientPhone], [ProblemId], [PsychologistId], [IsRemote], [Address], [Date], [Duration], [Bonus], [Cost]) VALUES (3, N'Stefano', CAST(N'1997-06-21T00:00:00.0000000' AS DateTime2), N'stefano@stefano.stefano ', N'099665544', 2, 1, 0, N'18 de Julio 1111', 3, 2, 25, 1500)
INSERT [dbo].[Consultations] ([Id], [PatientName], [PatientBirthDate], [PatientEmail], [PatientPhone], [ProblemId], [PsychologistId], [IsRemote], [Address], [Date], [Duration], [Bonus], [Cost]) VALUES (4, N'Pilar', CAST(N'2003-02-05T00:00:00.0000000' AS DateTime2), N'pilar@pilar.pilar', N'099112233', 4, 4, 0, N'Av. Italia 6677', 2, 2, 15, 3400)
INSERT [dbo].[Consultations] ([Id], [PatientName], [PatientBirthDate], [PatientEmail], [PatientPhone], [ProblemId], [PsychologistId], [IsRemote], [Address], [Date], [Duration], [Bonus], [Cost]) VALUES (5, N'Carolina', CAST(N'2021-06-05T00:00:00.0000000' AS DateTime2), N'carolina@carolina.carolina', N'099443355', 5, 2, 0, N'Av. Bolivia 1122', 4, 1, 50, 1000)
SET IDENTITY_INSERT [dbo].[Consultations] OFF
GO
INSERT [dbo].[PlayableContentPlaylist] ([ContentsId], [PlaylistsId]) VALUES (4, 2)
INSERT [dbo].[PlayableContentPlaylist] ([ContentsId], [PlaylistsId]) VALUES (5, 2)
INSERT [dbo].[PlayableContentPlaylist] ([ContentsId], [PlaylistsId]) VALUES (1, 3)
INSERT [dbo].[PlayableContentPlaylist] ([ContentsId], [PlaylistsId]) VALUES (2, 3)
INSERT [dbo].[PlayableContentPlaylist] ([ContentsId], [PlaylistsId]) VALUES (3, 3)
GO
SET IDENTITY_INSERT [dbo].[PlayableContents] ON 

INSERT [dbo].[PlayableContents] ([Id], [ImageURL], [Duration], [Author], [ContentURL], [Name], [CategoryId]) VALUES (1, N'', 4.5, N'Audioslave', N'http://like-a-stone.mp3', N'Like a Stone', 3)
INSERT [dbo].[PlayableContents] ([Id], [ImageURL], [Duration], [Author], [ContentURL], [Name], [CategoryId]) VALUES (2, N'', 5.3, N'Audioslave', N'http://i-am-the-highway.mp3', N'I Am the Highway', 3)
INSERT [dbo].[PlayableContents] ([Id], [ImageURL], [Duration], [Author], [ContentURL], [Name], [CategoryId]) VALUES (3, N'', 4, N'Audioslave', N'http://be-yourself.mp3', N'Be Yourself', 3)
INSERT [dbo].[PlayableContents] ([Id], [ImageURL], [Duration], [Author], [ContentURL], [Name], [CategoryId]) VALUES (4, N'', 4.9, N'The Strokes', N'http://you-only-live-once.mp3', N'You Only Live Once', 3)
INSERT [dbo].[PlayableContents] ([Id], [ImageURL], [Duration], [Author], [ContentURL], [Name], [CategoryId]) VALUES (5, N'', 3.9, N'The Strokes', N'http://someday.mp3', N'Someday', 3)
SET IDENTITY_INSERT [dbo].[PlayableContents] OFF
GO
SET IDENTITY_INSERT [dbo].[Playlists] ON 

INSERT [dbo].[Playlists] ([Id], [ImageURL], [Description], [Name], [CategoryId]) VALUES (1, N'http://red-hot-chili-peppers.mp3', N'Red Hot Chili Peppers', N'Red Hot Chili Peppers', 3)
INSERT [dbo].[Playlists] ([Id], [ImageURL], [Description], [Name], [CategoryId]) VALUES (2, N'http://the-strokes.mp3', N'The Strokes', N'The Strokes', 3)
INSERT [dbo].[Playlists] ([Id], [ImageURL], [Description], [Name], [CategoryId]) VALUES (3, N'http://audioslave.mp3', N'Audioslave', N'Audioslave', 3)
SET IDENTITY_INSERT [dbo].[Playlists] OFF
GO
INSERT [dbo].[PlaylistVideoContent] ([PlaylistsId], [VideosId]) VALUES (1, 1)
INSERT [dbo].[PlaylistVideoContent] ([PlaylistsId], [VideosId]) VALUES (1, 2)
INSERT [dbo].[PlaylistVideoContent] ([PlaylistsId], [VideosId]) VALUES (1, 3)
INSERT [dbo].[PlaylistVideoContent] ([PlaylistsId], [VideosId]) VALUES (1, 4)
INSERT [dbo].[PlaylistVideoContent] ([PlaylistsId], [VideosId]) VALUES (1, 5)
GO
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (1, 1)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (2, 1)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (3, 1)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (4, 2)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (5, 2)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (5, 3)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (7, 3)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (8, 3)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (1, 4)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (4, 4)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (6, 4)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (5, 5)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (6, 5)
INSERT [dbo].[ProblemPsychologist] ([ExpertiseId], [SpecialistsId]) VALUES (7, 5)
GO
SET IDENTITY_INSERT [dbo].[Problems] ON 

INSERT [dbo].[Problems] ([Id], [Name]) VALUES (1, N'Depresión')
INSERT [dbo].[Problems] ([Id], [Name]) VALUES (2, N'Estrés')
INSERT [dbo].[Problems] ([Id], [Name]) VALUES (3, N'Ansiedad')
INSERT [dbo].[Problems] ([Id], [Name]) VALUES (4, N'Autoestima')
INSERT [dbo].[Problems] ([Id], [Name]) VALUES (5, N'Enojo')
INSERT [dbo].[Problems] ([Id], [Name]) VALUES (6, N'Relaciones')
INSERT [dbo].[Problems] ([Id], [Name]) VALUES (7, N'Duelo')
INSERT [dbo].[Problems] ([Id], [Name]) VALUES (8, N'Y más')
SET IDENTITY_INSERT [dbo].[Problems] OFF
GO
SET IDENTITY_INSERT [dbo].[Psychologists] ON 

INSERT [dbo].[Psychologists] ([Id], [PsychologistName], [PsychologistSurname], [IsRemote], [Address], [ActiveYears], [ScheduleId], [Fee]) VALUES (1, N'Juan', N'Lopez', 0, N'18 de Julio 1111', 5, 1, 1000)
INSERT [dbo].[Psychologists] ([Id], [PsychologistName], [PsychologistSurname], [IsRemote], [Address], [ActiveYears], [ScheduleId], [Fee]) VALUES (2, N'Martin', N'Gomez', 0, N'Av. Bolivia 1122', 7, 2, 2000)
INSERT [dbo].[Psychologists] ([Id], [PsychologistName], [PsychologistSurname], [IsRemote], [Address], [ActiveYears], [ScheduleId], [Fee]) VALUES (3, N'Maria', N'Perez', 1, N'', 3, 3, 750)
INSERT [dbo].[Psychologists] ([Id], [PsychologistName], [PsychologistSurname], [IsRemote], [Address], [ActiveYears], [ScheduleId], [Fee]) VALUES (4, N'Lucia ', N'Perez', 0, N'Av. Italia 6677', 10, 4, 2000)
INSERT [dbo].[Psychologists] ([Id], [PsychologistName], [PsychologistSurname], [IsRemote], [Address], [ActiveYears], [ScheduleId], [Fee]) VALUES (5, N'Paula', N'Dominguez', 0, N'Av. Bolivia 3344', 1, 5, 500)
SET IDENTITY_INSERT [dbo].[Psychologists] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([Id], [MondayConsultations], [TuesdayConsultations], [WednesdayConsultations], [ThursdayConsultations], [FridayConsultations]) VALUES (1, 1, 0, 0, 1, 0)
INSERT [dbo].[Schedules] ([Id], [MondayConsultations], [TuesdayConsultations], [WednesdayConsultations], [ThursdayConsultations], [FridayConsultations]) VALUES (2, 0, 0, 0, 0, 1)
INSERT [dbo].[Schedules] ([Id], [MondayConsultations], [TuesdayConsultations], [WednesdayConsultations], [ThursdayConsultations], [FridayConsultations]) VALUES (3, 0, 0, 0, 0, 0)
INSERT [dbo].[Schedules] ([Id], [MondayConsultations], [TuesdayConsultations], [WednesdayConsultations], [ThursdayConsultations], [FridayConsultations]) VALUES (4, 0, 0, 1, 0, 1)
INSERT [dbo].[Schedules] ([Id], [MondayConsultations], [TuesdayConsultations], [WednesdayConsultations], [ThursdayConsultations], [FridayConsultations]) VALUES (5, 0, 0, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
SET IDENTITY_INSERT [dbo].[VideoContents] ON 

INSERT [dbo].[VideoContents] ([Id], [Duration], [Author], [VideoURL], [Name], [CategoryId]) VALUES (1, 4.33, N'Red Hot Chili Peppers', N'https://www.youtube.com/embed/YlUKcNNmywk', N'Californication ', 3)
INSERT [dbo].[VideoContents] ([Id], [Duration], [Author], [VideoURL], [Name], [CategoryId]) VALUES (2, 5.03, N'Red Hot Chili Peppers', N'https://www.youtube.com/embed/Q0oIoR9mLwc', N'Dark Necessities', 3)
INSERT [dbo].[VideoContents] ([Id], [Duration], [Author], [VideoURL], [Name], [CategoryId]) VALUES (3, 4.4, N'Red Hot Chili Peppers', N'https://www.youtube.com/embed/GLvohMXgcBo', N'Under The Bridge', 3)
INSERT [dbo].[VideoContents] ([Id], [Duration], [Author], [VideoURL], [Name], [CategoryId]) VALUES (4, 3.4, N'Red Hot Chili Peppers', N'https://www.youtube.com/embed/mzJj5-lubeM', N'Scar Tissue', 3)
INSERT [dbo].[VideoContents] ([Id], [Duration], [Author], [VideoURL], [Name], [CategoryId]) VALUES (5, 4.49, N'Red Hot Chili Peppers', N'https://www.youtube.com/embed/RtBbinpK5XI', N'The Adventures of Rain Dance Maggie', 3)
SET IDENTITY_INSERT [dbo].[VideoContents] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Administrators_Email]    Script Date: 17/6/2021 6:08:41 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Administrators_Email] ON [dbo].[Administrators]
(
	[Email] ASC
)
WHERE ([Email] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Categories_Name]    Script Date: 17/6/2021 6:08:41 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Categories_Name] ON [dbo].[Categories]
(
	[Name] ASC
)
WHERE ([Name] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Consultations_ProblemId]    Script Date: 17/6/2021 6:08:41 ******/
CREATE NONCLUSTERED INDEX [IX_Consultations_ProblemId] ON [dbo].[Consultations]
(
	[ProblemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Consultations_PsychologistId]    Script Date: 17/6/2021 6:08:41 ******/
CREATE NONCLUSTERED INDEX [IX_Consultations_PsychologistId] ON [dbo].[Consultations]
(
	[PsychologistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlayableContentPlaylist_PlaylistsId]    Script Date: 17/6/2021 6:08:41 ******/
CREATE NONCLUSTERED INDEX [IX_PlayableContentPlaylist_PlaylistsId] ON [dbo].[PlayableContentPlaylist]
(
	[PlaylistsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlayableContents_CategoryId]    Script Date: 17/6/2021 6:08:41 ******/
CREATE NONCLUSTERED INDEX [IX_PlayableContents_CategoryId] ON [dbo].[PlayableContents]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Playlists_CategoryId]    Script Date: 17/6/2021 6:08:41 ******/
CREATE NONCLUSTERED INDEX [IX_Playlists_CategoryId] ON [dbo].[Playlists]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlaylistVideoContent_VideosId]    Script Date: 17/6/2021 6:08:41 ******/
CREATE NONCLUSTERED INDEX [IX_PlaylistVideoContent_VideosId] ON [dbo].[PlaylistVideoContent]
(
	[VideosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProblemPsychologist_SpecialistsId]    Script Date: 17/6/2021 6:08:41 ******/
CREATE NONCLUSTERED INDEX [IX_ProblemPsychologist_SpecialistsId] ON [dbo].[ProblemPsychologist]
(
	[SpecialistsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Problems_Name]    Script Date: 17/6/2021 6:08:41 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Problems_Name] ON [dbo].[Problems]
(
	[Name] ASC
)
WHERE ([Name] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Psychologists_ScheduleId]    Script Date: 17/6/2021 6:08:41 ******/
CREATE NONCLUSTERED INDEX [IX_Psychologists_ScheduleId] ON [dbo].[Psychologists]
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_VideoContents_CategoryId]    Script Date: 17/6/2021 6:08:41 ******/
CREATE NONCLUSTERED INDEX [IX_VideoContents_CategoryId] ON [dbo].[VideoContents]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
ALTER TABLE [dbo].[PlaylistVideoContent]  WITH CHECK ADD  CONSTRAINT [FK_PlaylistVideoContent_Playlists_PlaylistsId] FOREIGN KEY([PlaylistsId])
REFERENCES [dbo].[Playlists] ([Id])
GO
ALTER TABLE [dbo].[PlaylistVideoContent] CHECK CONSTRAINT [FK_PlaylistVideoContent_Playlists_PlaylistsId]
GO
ALTER TABLE [dbo].[PlaylistVideoContent]  WITH CHECK ADD  CONSTRAINT [FK_PlaylistVideoContent_VideoContents_VideosId] FOREIGN KEY([VideosId])
REFERENCES [dbo].[VideoContents] ([Id])
GO
ALTER TABLE [dbo].[PlaylistVideoContent] CHECK CONSTRAINT [FK_PlaylistVideoContent_VideoContents_VideosId]
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
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Psychologists] CHECK CONSTRAINT [FK_Psychologists_Schedules_ScheduleId]
GO
ALTER TABLE [dbo].[VideoContents]  WITH CHECK ADD  CONSTRAINT [FK_VideoContents_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VideoContents] CHECK CONSTRAINT [FK_VideoContents_Categories_CategoryId]
GO
USE [master]
GO
ALTER DATABASE [BetterCalmDB] SET  READ_WRITE 
GO
