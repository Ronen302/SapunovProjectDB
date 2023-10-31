USE [master]
GO
/****** Object:  Database [SapunovProjectDB]    Script Date: 31.10.2023 14:39:13 ******/
CREATE DATABASE [SapunovProjectDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SapunovProjectDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SapunovProjectDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SapunovProjectDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SapunovProjectDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SapunovProjectDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SapunovProjectDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SapunovProjectDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SapunovProjectDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SapunovProjectDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SapunovProjectDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SapunovProjectDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SapunovProjectDB] SET  MULTI_USER 
GO
ALTER DATABASE [SapunovProjectDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SapunovProjectDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SapunovProjectDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SapunovProjectDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SapunovProjectDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [SapunovProjectDB]
GO
/****** Object:  Table [dbo].[CategoryOfService]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryOfService](
	[IdCategory] [int] IDENTITY(1,1) NOT NULL,
	[NameCategory] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CategoryOfService] PRIMARY KEY CLUSTERED 
(
	[IdCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[IdClient] [int] IDENTITY(1,1) NOT NULL,
	[NameClient] [nvarchar](50) NOT NULL,
	[PhoneNumberClient] [nvarchar](11) NOT NULL,
	[EmailClient] [nvarchar](50) NULL,
	[DateOfRegistration] [date] NOT NULL,
	[IdUser] [int] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[IdClient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Education]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Education](
	[IdEducation] [int] IDENTITY(1,1) NOT NULL,
	[NameEducation] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Education] PRIMARY KEY CLUSTERED 
(
	[IdEducation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GenderStaff]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GenderStaff](
	[IdGenderStaff] [int] IDENTITY(1,1) NOT NULL,
	[NameGender] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_GenderStaff] PRIMARY KEY CLUSTERED 
(
	[IdGenderStaff] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Order]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[IdOrder] [int] IDENTITY(1,1) NOT NULL,
	[DateOfCreate] [date] NOT NULL,
	[IdClient] [int] NULL,
	[IdTypeOfWork] [int] NULL,
	[IdStatusOrder] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[IdOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PositionAtWork]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionAtWork](
	[IdPositionAtWork] [int] IDENTITY(1,1) NOT NULL,
	[NameOfPosition] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PositionAtWork] PRIMARY KEY CLUSTERED 
(
	[IdPositionAtWork] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[IdRole] [int] IDENTITY(1,1) NOT NULL,
	[NameRole] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[IdRole] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Service]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[IdService] [int] IDENTITY(1,1) NOT NULL,
	[NameService] [nvarchar](max) NOT NULL,
	[IdCategory] [int] NOT NULL,
	[PriceOfService] [decimal](10, 2) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[IdService] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Staff]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[IdStaff] [int] IDENTITY(1,1) NOT NULL,
	[FirstNameStaff] [nvarchar](50) NOT NULL,
	[LastNameStaff] [nvarchar](50) NOT NULL,
	[MiddleNameStaff] [nvarchar](50) NULL,
	[IdGenderStaff] [int] NOT NULL,
	[IdEducation] [int] NOT NULL,
	[DateOfBirthStaff] [date] NOT NULL,
	[PhoneNumberStaff] [nvarchar](11) NOT NULL,
	[EmailStaff] [nvarchar](50) NOT NULL,
	[IdPositionAtWork] [int] NOT NULL,
	[SalaryStaff] [decimal](10, 2) NOT NULL,
	[HireDateStaff] [date] NOT NULL,
	[IdUser] [int] NOT NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[IdStaff] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusOrder]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusOrder](
	[IdStatusOrder] [int] IDENTITY(1,1) NOT NULL,
	[NameStatusOrder] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_StatusOrder] PRIMARY KEY CLUSTERED 
(
	[IdStatusOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TypeOfWork]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfWork](
	[IdTypeOfWork] [int] IDENTITY(1,1) NOT NULL,
	[NameTypeOfWork] [nvarchar](max) NOT NULL,
	[IdService] [int] NOT NULL,
	[PriceOfWork] [decimal](10, 2) NOT NULL,
	[DescriptionOfWork] [nvarchar](max) NULL,
 CONSTRAINT [PK_TypeOfWork] PRIMARY KEY CLUSTERED 
(
	[IdTypeOfWork] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 31.10.2023 14:39:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[LoginUser] [nvarchar](50) NOT NULL,
	[PasswordUser] [nvarchar](50) NOT NULL,
	[IdRole] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[CategoryOfService] ON 

INSERT [dbo].[CategoryOfService] ([IdCategory], [NameCategory]) VALUES (1, N'Отделка')
INSERT [dbo].[CategoryOfService] ([IdCategory], [NameCategory]) VALUES (2, N'Строительство домов')
INSERT [dbo].[CategoryOfService] ([IdCategory], [NameCategory]) VALUES (3, N'Электротехнические услуги')
INSERT [dbo].[CategoryOfService] ([IdCategory], [NameCategory]) VALUES (4, N'Сантехнические услуги')
INSERT [dbo].[CategoryOfService] ([IdCategory], [NameCategory]) VALUES (5, N'Строительство мангалов, каминов, бань, беседок')
SET IDENTITY_INSERT [dbo].[CategoryOfService] OFF
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (1, N'Алексей', N'89108766767', N'aleks@gmail.com', CAST(N'2019-09-05' AS Date), 1)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (2, N'Алексей', N'81234565676', N'morozov@mail.ru', CAST(N'2013-11-24' AS Date), 2)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (3, N'Алексей', N'89345673456', N'akulov@gmail.com', CAST(N'2017-02-11' AS Date), 3)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (4, N'Алексей', N'86598452271', N'tarasov@gmail.com', CAST(N'2020-01-28' AS Date), 4)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (5, N'Лев', N'84568412585', N'drofeev@mail.ru', CAST(N'2014-04-18' AS Date), 5)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (6, N'Ефим', N'83645645645', N'borzilov@gmail.com', CAST(N'2022-04-07' AS Date), 6)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (7, N'Федор', N'89546456456', N'hodaev@mail.ru', CAST(N'2022-11-11' AS Date), 7)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (8, N'Анфиса', N'89456575352', N'werwea@gmail.com', CAST(N'2013-11-28' AS Date), 8)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (9, N'Таисия', N'89456456457', N'paulkina@mail.ru', CAST(N'2017-08-16' AS Date), 9)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (10, N'Герасим', N'89743445346', N'sirov@gmail.com', CAST(N'2012-10-07' AS Date), 10)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (11, N'Роман', N'89546576765', N'mahner@mail.ru', CAST(N'2012-03-19' AS Date), 11)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (12, N'Геннадий', N'89346456456', N'zhvikov@gmail.com', CAST(N'2021-03-29' AS Date), 12)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (13, N'Николай', N'89457456743', N'alistratov@gmail.com', CAST(N'2012-10-03' AS Date), 13)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (14, N'Савва', N'89546457457', N'korsakov@mail.ru', CAST(N'2017-10-01' AS Date), 14)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (15, N'Ульяна', N'89564563546', N'yagnisheva@gmail.com', CAST(N'2014-07-26' AS Date), 15)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (16, N'Алексей', N'89456456575', N'aplenov', CAST(N'2021-10-22' AS Date), 16)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (17, N'Захар', N'89567456654', N'gushin', CAST(N'2022-03-06' AS Date), 17)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (18, N'Михаил', N'89546456575', N'shipitsin@mail.ru', CAST(N'2022-03-06' AS Date), 18)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (19, N'Никифор', N'89345345646', N'buharov@gmail.com', CAST(N'2023-10-10' AS Date), 19)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (20, N'Давид', N'89345346546', N'andreev@gmail.com', CAST(N'2013-06-06' AS Date), 20)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (21, N'Захар', N'89456456456', N'emelin@mail.ru', CAST(N'2023-06-02' AS Date), 21)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (22, N'Яков', N'89575675675', N'teplov@mail.ru', CAST(N'2021-03-04' AS Date), 22)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (23, N'Афанасий', N'89454567567', N'yakyshenko@gmail.com', CAST(N'2017-12-02' AS Date), 23)
INSERT [dbo].[Client] ([IdClient], [NameClient], [PhoneNumberClient], [EmailClient], [DateOfRegistration], [IdUser]) VALUES (24, N'Даниил', N'89454573443', N'mikov@mail.ru', CAST(N'2018-10-28' AS Date), 24)
SET IDENTITY_INSERT [dbo].[Client] OFF
SET IDENTITY_INSERT [dbo].[Education] ON 

INSERT [dbo].[Education] ([IdEducation], [NameEducation]) VALUES (1, N'Среднее общее')
INSERT [dbo].[Education] ([IdEducation], [NameEducation]) VALUES (2, N'Среднее профессиональное')
INSERT [dbo].[Education] ([IdEducation], [NameEducation]) VALUES (3, N'Высшее')
SET IDENTITY_INSERT [dbo].[Education] OFF
SET IDENTITY_INSERT [dbo].[GenderStaff] ON 

INSERT [dbo].[GenderStaff] ([IdGenderStaff], [NameGender]) VALUES (1, N'муж.')
INSERT [dbo].[GenderStaff] ([IdGenderStaff], [NameGender]) VALUES (2, N'жен.')
SET IDENTITY_INSERT [dbo].[GenderStaff] OFF
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (1, CAST(N'2018-05-09' AS Date), 5, 14, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (2, CAST(N'2017-01-24' AS Date), 6, 1, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (3, CAST(N'2016-07-31' AS Date), 7, 3, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (4, CAST(N'2016-11-14' AS Date), 8, 29, 3)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (5, CAST(N'2014-05-01' AS Date), 9, 24, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (6, CAST(N'2018-11-23' AS Date), 10, 21, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (7, CAST(N'2012-05-03' AS Date), 11, 10, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (8, CAST(N'2018-09-16' AS Date), 12, 16, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (9, CAST(N'2019-06-20' AS Date), 13, 25, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (10, CAST(N'2014-07-04' AS Date), 14, 4, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (11, CAST(N'2019-08-27' AS Date), 15, 18, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (12, CAST(N'2022-09-05' AS Date), 16, 23, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (13, CAST(N'2014-01-24' AS Date), 17, 31, 3)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (14, CAST(N'2014-06-10' AS Date), 18, 27, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (15, CAST(N'2021-02-01' AS Date), 19, 33, 3)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (16, CAST(N'2013-11-29' AS Date), 20, 37, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (17, CAST(N'2018-06-12' AS Date), 21, 13, 3)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (18, CAST(N'2020-08-28' AS Date), 22, 15, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (19, CAST(N'2020-01-25' AS Date), 23, 30, 3)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (20, CAST(N'2016-02-29' AS Date), 24, 1, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (21, CAST(N'2021-08-18' AS Date), 14, 14, 2)
INSERT [dbo].[Order] ([IdOrder], [DateOfCreate], [IdClient], [IdTypeOfWork], [IdStatusOrder]) VALUES (22, CAST(N'2016-03-25' AS Date), 16, 21, 2)
SET IDENTITY_INSERT [dbo].[Order] OFF
SET IDENTITY_INSERT [dbo].[PositionAtWork] ON 

INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (1, N'Ген. директор')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (2, N'Заместитель ген. директора')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (3, N'Администратор системы')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (4, N'Главный инженер')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (5, N'Инженер-строитель')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (6, N'Инженер по технике безопасности')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (7, N'Архитектор')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (8, N'Главный специалист по строительству')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (9, N'Старший менеджер по строительству')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (10, N'Менеджер по работе с клиентами')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (11, N'Руководитель проекта')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (12, N'Ассистент менеджера проекта')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (13, N'Помощник по строительству')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (14, N'Менеджер по безопасности')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (15, N'Прораб')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (16, N'Оператор оборудования')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (17, N'Оценщик')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (18, N'Полевой инженер')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (19, N'Инспектор')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (20, N'Планировщик')
INSERT [dbo].[PositionAtWork] ([IdPositionAtWork], [NameOfPosition]) VALUES (21, N'Разнорабочий')
SET IDENTITY_INSERT [dbo].[PositionAtWork] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([IdRole], [NameRole]) VALUES (1, N'Администратор')
INSERT [dbo].[Role] ([IdRole], [NameRole]) VALUES (2, N'Менеджер')
INSERT [dbo].[Role] ([IdRole], [NameRole]) VALUES (3, N'Сотрудник')
INSERT [dbo].[Role] ([IdRole], [NameRole]) VALUES (4, N'Клиент')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Service] ON 

INSERT [dbo].[Service] ([IdService], [NameService], [IdCategory], [PriceOfService], [Description]) VALUES (1, N'Строительство домов по каркасной технологии', 2, CAST(22000.00 AS Decimal(10, 2)), N'Переезд в свой дом через 3-4 месяца - это реальность. Быстровозводимые, теплые и недорогие дома')
INSERT [dbo].[Service] ([IdService], [NameService], [IdCategory], [PriceOfService], [Description]) VALUES (2, N'Строительство деревянных домов', 2, CAST(20000.00 AS Decimal(10, 2)), N'Долговечные, экологически чистые дома с особой атмосферой')
INSERT [dbo].[Service] ([IdService], [NameService], [IdCategory], [PriceOfService], [Description]) VALUES (3, N'Строительство каменных домов', 2, CAST(22000.00 AS Decimal(10, 2)), N'Мой дом - моя крепость. Надежные и долговечные каменные дома')
INSERT [dbo].[Service] ([IdService], [NameService], [IdCategory], [PriceOfService], [Description]) VALUES (4, N'Устройство фундамента', 2, CAST(2000.00 AS Decimal(10, 2)), N'Ленточный, монолитный, столбчатый или винтовой')
INSERT [dbo].[Service] ([IdService], [NameService], [IdCategory], [PriceOfService], [Description]) VALUES (5, N'Устройство отмостки, дорожек и дренажной системы', 1, CAST(1200.00 AS Decimal(10, 2)), N'Проектирование и работы на вашем участке')
INSERT [dbo].[Service] ([IdService], [NameService], [IdCategory], [PriceOfService], [Description]) VALUES (6, N'Внешние отделочные работы', 1, CAST(1100.00 AS Decimal(10, 2)), N'Внешняя отделка домов облицовочным кирпичом, декоративной штукатуркой, деревом, сайдингом. Устройство софитов и отделка цоколя')
INSERT [dbo].[Service] ([IdService], [NameService], [IdCategory], [PriceOfService], [Description]) VALUES (7, N'Строительство бань, беседок, веранд, гаражей', 5, CAST(1500.00 AS Decimal(10, 2)), N'Строим бани, беседки, гаражи и пристраиваем веранды')
INSERT [dbo].[Service] ([IdService], [NameService], [IdCategory], [PriceOfService], [Description]) VALUES (8, N'Кладка печей, каминов и мангалов', 5, CAST(60000.00 AS Decimal(10, 2)), N'Кладка печей, каминов, мангалов и барбекю')
INSERT [dbo].[Service] ([IdService], [NameService], [IdCategory], [PriceOfService], [Description]) VALUES (9, N'Сантехнические работы', 4, CAST(2000.00 AS Decimal(10, 2)), N'Все виды сантехнических работ: разводка воды, монтаж радиаторов отопления и водяных теплых полов, установка септиков, подключение бойлеров и фильтров')
INSERT [dbo].[Service] ([IdService], [NameService], [IdCategory], [PriceOfService], [Description]) VALUES (10, N'Электротехнические работы', 3, CAST(1500.00 AS Decimal(10, 2)), N'Любые электротехнические работы. Монтаж электропроводки "под ключ", устройство теплого пола, монтаж электрощитка')
SET IDENTITY_INSERT [dbo].[Service] OFF
SET IDENTITY_INSERT [dbo].[Staff] ON 

INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (1, N'Алексей', N'Морозов', N'Давидович', 1, 3, CAST(N'1983-06-21' AS Date), N'89535278850', N'morozov@mail.ru', 1, CAST(230000.00 AS Decimal(10, 2)), CAST(N'2003-12-23' AS Date), 1)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (2, N'Алексей', N'Резяпкин', N'Романович', 1, 3, CAST(N'1973-07-18' AS Date), N'88524761177', N'rezyapkin@gmail.com', 10, CAST(120000.00 AS Decimal(10, 2)), CAST(N'2008-02-21' AS Date), 2)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (3, N'Алексей', N'Тарасов', N'Иннокентиевич', 1, 3, CAST(N'1971-07-25' AS Date), N'84963669583', N'tarasov@gmail.com', 4, CAST(150000.00 AS Decimal(10, 2)), CAST(N'2003-05-29' AS Date), 3)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (4, N'Лев', N'Дорофеев', N'Степанович', 1, 2, CAST(N'1990-12-23' AS Date), N'84568412585', N'drofeev@mail.ru', 12, CAST(60000.00 AS Decimal(10, 2)), CAST(N'2017-07-21' AS Date), 5)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (5, N'Ефим', N'Борзилов', N'Макарович', 1, 3, CAST(N'1983-06-04' AS Date), N'83645645645', N'borzilov@gmail.com', 16, CAST(78000.00 AS Decimal(10, 2)), CAST(N'2019-06-20' AS Date), 6)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (6, N'Федор', N'Ходяев', N'Петрович', 1, 3, CAST(N'1963-07-16' AS Date), N'89546456456', N'hodaev@mail.ru', 9, CAST(90000.00 AS Decimal(10, 2)), CAST(N'2014-08-22' AS Date), 7)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (7, N'Анфиса', N'Смольникова', N'Валентиновна', 2, 3, CAST(N'1992-04-28' AS Date), N'89456575352', N'werwea@gmail.com', 10, CAST(76000.00 AS Decimal(10, 2)), CAST(N'2020-06-10' AS Date), 8)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (8, N'Таисия', N'Паулкина', N'Ивановна', 2, 2, CAST(N'1960-04-01' AS Date), N'89456456457', N'paulkina@mail.ru', 7, CAST(80000.00 AS Decimal(10, 2)), CAST(N'2014-02-05' AS Date), 9)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (9, N'Герасим', N'Сыров', N'Филиппович', 1, 2, CAST(N'1975-04-02' AS Date), N'89743445346', N'sirov@gmail.com', 15, CAST(60000.00 AS Decimal(10, 2)), CAST(N'2022-05-09' AS Date), 10)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (10, N'Роман', N'Махнер', N'Антонович', 1, 1, CAST(N'1982-11-16' AS Date), N'89546576765', N'mahner@mail.ru', 21, CAST(50000.00 AS Decimal(10, 2)), CAST(N'2020-08-14' AS Date), 11)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (11, N'Геннадий', N'Жвиков', N'Ефимович', 1, 3, CAST(N'1987-11-26' AS Date), N'89346456456', N'zhvikov@gmail.com', 3, CAST(140000.00 AS Decimal(10, 2)), CAST(N'2022-03-09' AS Date), 12)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (12, N'Николай', N'Алистратов', N'Леонидович', 1, 3, CAST(N'1978-12-25' AS Date), N'89457456743', N'alistratov@gmail.com', 3, CAST(150000.00 AS Decimal(10, 2)), CAST(N'2018-02-13' AS Date), 13)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (13, N'Савва', N'Корсаков', N'Себастьянович', 1, 3, CAST(N'1985-08-09' AS Date), N'89546457457', N'korsakov@mail.ru', 10, CAST(80000.00 AS Decimal(10, 2)), CAST(N'2019-07-12' AS Date), 14)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (14, N'Ульяна', N'Ягнышева', N'Ивановна', 2, 3, CAST(N'1974-06-13' AS Date), N'89564563546', N'yagnisheva@gmail.com', 10, CAST(70000.00 AS Decimal(10, 2)), CAST(N'2022-05-10' AS Date), 15)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (15, N'Алексей', N'Алленов', N'Егорович', 1, 1, CAST(N'1978-09-18' AS Date), N'89456456575', N'aplenov', 21, CAST(20000.00 AS Decimal(10, 2)), CAST(N'2023-08-29' AS Date), 16)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (16, N'Захар', N'Гущин', N'Леонидович', 1, 1, CAST(N'1991-08-13' AS Date), N'89567456654', N'gushin@mail.ru', 21, CAST(20000.00 AS Decimal(10, 2)), CAST(N'2023-09-06' AS Date), 17)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (17, N'Михаил', N'Шипицин', N'Всеволодович', 1, 3, CAST(N'1964-03-21' AS Date), N'89546456575', N'shipitsin@mail.ru', 19, CAST(60000.00 AS Decimal(10, 2)), CAST(N'2019-07-12' AS Date), 18)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (18, N'Никифор', N'Бухаров', N'Павлович', 1, 2, CAST(N'1994-12-02' AS Date), N'89345345646', N'buharov@gmail.com', 16, CAST(70000.00 AS Decimal(10, 2)), CAST(N'2019-12-05' AS Date), 19)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (19, N'Давид', N'Андреев', N'Георгиевич', 1, 3, CAST(N'1979-12-20' AS Date), N'89345346546', N'andreev@gmail.com', 6, CAST(80000.00 AS Decimal(10, 2)), CAST(N'2020-06-18' AS Date), 20)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (20, N'Захар', N'Емелин', N'Филиппович', 1, 3, CAST(N'1971-03-21' AS Date), N'89456456456', N'emelin@mail.ru', 13, CAST(87000.00 AS Decimal(10, 2)), CAST(N'2020-06-11' AS Date), 21)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (21, N'Яков', N'Теплов', N'Павлович', 1, 2, CAST(N'1976-07-02' AS Date), N'89575675675', N'teplov@mail.ru', 18, CAST(60000.00 AS Decimal(10, 2)), CAST(N'2019-03-14' AS Date), 22)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (22, N'Афанасий', N'Якущенко', N'Венедиктович', 1, 3, CAST(N'1973-04-23' AS Date), N'89454567567', N'yakyshenko@gmail.com', 16, CAST(70000.00 AS Decimal(10, 2)), CAST(N'2021-03-12' AS Date), 23)
INSERT [dbo].[Staff] ([IdStaff], [FirstNameStaff], [LastNameStaff], [MiddleNameStaff], [IdGenderStaff], [IdEducation], [DateOfBirthStaff], [PhoneNumberStaff], [EmailStaff], [IdPositionAtWork], [SalaryStaff], [HireDateStaff], [IdUser]) VALUES (23, N'Даниил', N'Миков', N'Валерианович', 1, 3, CAST(N'1990-11-10' AS Date), N'89454573443', N'mikov@mail.ru', 20, CAST(65000.00 AS Decimal(10, 2)), CAST(N'2017-07-07' AS Date), 24)
SET IDENTITY_INSERT [dbo].[Staff] OFF
SET IDENTITY_INSERT [dbo].[StatusOrder] ON 

INSERT [dbo].[StatusOrder] ([IdStatusOrder], [NameStatusOrder]) VALUES (1, N'Принят')
INSERT [dbo].[StatusOrder] ([IdStatusOrder], [NameStatusOrder]) VALUES (2, N'Готов')
INSERT [dbo].[StatusOrder] ([IdStatusOrder], [NameStatusOrder]) VALUES (3, N'Отменен')
SET IDENTITY_INSERT [dbo].[StatusOrder] OFF
SET IDENTITY_INSERT [dbo].[TypeOfWork] ON 

INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (1, N'Строительство каркасных домов', 1, CAST(22000.00 AS Decimal(10, 2)), N'Фундамент, стены и перегородки, утепление, кровля')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (2, N'Строительство дома из оцилиндрованного бревна', 2, CAST(20000.00 AS Decimal(10, 2)), N'Подготовка коммуникаций, фундамент, поднятие стен и перегородок, утепленная кровля')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (3, N'Строительство дома из профилированного бруса', 2, CAST(22000.00 AS Decimal(10, 2)), N'Подготовка коммуникаций, фундамент, поднятие стен и перегородок, утепленная кровля')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (4, N'Строительство домов из клееного бруса', 2, CAST(25000.00 AS Decimal(10, 2)), N'Подготовка коммуникаций, фундамент, поднятие стен и перегородок, утепленная кровля')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (5, N'Строительство каркасных домов', 2, CAST(22000.00 AS Decimal(10, 2)), N'Фундамент, стены и перегородки, утепление, кровля')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (6, N'Строительство дома из газобетона', 3, CAST(22000.00 AS Decimal(10, 2)), N'Устройство фундамента, возведение стен, устройство перекрытия, устройство крыши')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (7, N'Строительство дома из пеноблоков', 3, CAST(22000.00 AS Decimal(10, 2)), N'Устройство фундамента, возведение стен, устройство перекрытия, устройство крыши')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (8, N'Строительство дома из кирпича', 3, CAST(32000.00 AS Decimal(10, 2)), N'Устройство фундамента, возведение стен, устройство перекрытия, устройство крыши')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (9, N'
Устройство ленточного фундамента', 4, CAST(3800.00 AS Decimal(10, 2)), N'Разметка фундамента, выемка грунта, устройство подушки из щебня и песка с утрамбовкой, монтаж опалубки, сбор и установка армирующего каркаса, заливка бетона')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (10, N'Устройство монолитного фундамента', 4, CAST(4000.00 AS Decimal(10, 2)), N'Разметка фундамента, выемка грунта, устройство подушки из щебня и песка с утрамбовкой, монтаж опалубки, сбор и установка армирующего каркаса, заливка бетона')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (11, N'Устройство столбчатого фундамента', 4, CAST(2500.00 AS Decimal(10, 2)), N'Разметка фундамента, выемка грунта, устройство подушки из щебня и песка с утрамбовкой, монтаж опалубки, сбор и установка армирующего каркаса, заливка бетона')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (12, N'Устройство винтового фундамента', 4, CAST(2000.00 AS Decimal(10, 2)), N'Разметка фундамента, бурение скважен, нанесение антикоррозийного состава, ввинчивание винтовых опор, бетонирование, обвязка свай')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (13, N'Устройство отмостки', 5, CAST(1200.00 AS Decimal(10, 2)), N'Снятие плодородного слоя грунта, устройство опалубки с учетом компенсационных швов, засыпка и утрамбовка щебня, засыпка и утрамбовка песка, армирование и укладка бетонной смеси')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (14, N'Устройство дорожек', 5, CAST(1200.00 AS Decimal(10, 2)), N'Снятие плодородного слоя грунта, устройство опалубки с учетом компенсационных швов, засыпка и утрамбовка щебня, засыпка и утрамбовка песка, армирование и укладка бетонной смеси')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (15, N'Устройство поверхностной дренажной системы', 5, CAST(1400.00 AS Decimal(10, 2)), N'Проектирование, земляные работы и монтаж дренажной системы')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (16, N'Строительство бани', 7, CAST(19800.00 AS Decimal(10, 2)), N'Фундамент, стены и перегородки, крыша')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (17, N'Строительство беседки', 7, CAST(1500.00 AS Decimal(10, 2)), N'Устройство беседки из пиломатериалов')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (18, N'Строительство гаража', 7, CAST(18000.00 AS Decimal(10, 2)), N'Фундамент, стены, крыша')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (19, N'Пристройка веранды', 7, CAST(14000.00 AS Decimal(10, 2)), N'Столбчатый фундамент, веранда из пиломатериалов, крепление к стене, устройство крыши')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (20, N'Кладка печей', 8, CAST(60000.00 AS Decimal(10, 2)), N'Чертеж, фундамент, кладка, устройство дымохода, установка дверок')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (21, N'Кладка каминов', 8, CAST(120000.00 AS Decimal(10, 2)), N'Чертеж, фундамент, кладка, устройство дымохода, декоративная отделка')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (22, N'Кладка мангалов', 8, CAST(110000.00 AS Decimal(10, 2)), N'Чертеж, фундамент, кладка, устройство дымохода, декоративная отделка')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (23, N'Монтаж радиаторов отопления (трубы полипропилен)', 9, CAST(3000.00 AS Decimal(10, 2)), N'Прокладка труб, штробление стен под краны, установка радиатора, подключение')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (24, N'Монтаж водяных теплых полов', 9, CAST(3300.00 AS Decimal(10, 2)), N'Слой гидроизоляции, укладка слоя утеплителя, армированная сетка, раскладка труб теплого пола, стяжка, подключение')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (25, N'Установка и "обвязка" котла отопления', 9, CAST(12500.00 AS Decimal(10, 2)), N'Монтаж газовых, электрических, дизельных, твердотопливных, настенных/напольных')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (26, N'Установка септика', 9, CAST(18000.00 AS Decimal(10, 2)), N'Земляные работы, установка станции в котлован, утепление, обсыпка песком, подключение труб, пусконаладочные работы')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (27, N'Подключение бойлеров и фильтров для воды', 9, CAST(2000.00 AS Decimal(10, 2)), N'Установка шарового вентиля, регулятора давления, отсекающего крана, предохранительного клапана и сливного крана')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (28, N'Разводка воды', 9, CAST(2500.00 AS Decimal(10, 2)), N'Прокладка труб холодной и горячей воды, штробление стен')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (29, N'Устройство теплого электрического пола', 10, CAST(1700.00 AS Decimal(10, 2)), N'Укладка утеплителя, армированной сетки, стяжка, монтаж кабеля или мата теплого пола, датчика, клеевой раствор, подключение')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (30, N'Монтаж электропроводки "под ключ"', 10, CAST(1500.00 AS Decimal(10, 2)), N'Штробление стен, укладка проводов, монтаж распределительных коробок, высверливание гнезда, подключение проводки и установка розеток')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (31, N'Монтаж электрощитка', 10, CAST(3200.00 AS Decimal(10, 2)), N'Сбор внутреннего наполнения электрического щитка: крепежные планки для автоматов, счетчик, вводный автомат, устройство защитного отключения, секторные автоматы, рейки по "0" и под "землю", подключение проводов')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (32, N'Отделка облицовочным кирпичом', 6, CAST(3300.00 AS Decimal(10, 2)), N'Подготовка стен, кладка облицовочного кирпича')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (33, N'Отделка декоративной штукатуркой с утеплителем', 6, CAST(3850.00 AS Decimal(10, 2)), N'Подготовка стен, крепление утеплителя, слой клея с сеткой, нанесение декоративной штукатурки')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (34, N'Отделка деревом', 6, CAST(1700.00 AS Decimal(10, 2)), N'Отделка деревом')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (35, N'Отделка сайдингом', 6, CAST(3700.00 AS Decimal(10, 2)), N'Подготовка стен, монтаж обрешетки и утеплителя, установка ветрозащитной мембраны, установка углов, соединительных элементов и стартового профиля, обшивка сайдингом')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (36, N'Установка софитов', 6, CAST(1100.00 AS Decimal(10, 2)), N'Монтаж обрешетки, подрезка софитов, вставка и фиксация софитов, оформление углов')
INSERT [dbo].[TypeOfWork] ([IdTypeOfWork], [NameTypeOfWork], [IdService], [PriceOfWork], [DescriptionOfWork]) VALUES (37, N'Отделка цоколя', 6, CAST(1700.00 AS Decimal(10, 2)), N'Подготовка поверхности цоколи, утепление при необходимости, декоративная отделка')
SET IDENTITY_INSERT [dbo].[TypeOfWork] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (1, N'admin', N'123', 1)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (2, N'manager', N'1234', 2)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (3, N'staff', N'12345', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (4, N'client', N'123456', 4)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (5, N'dorofeev90', N'Dorofeev90', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (6, N'borzilov', N'Borzilov34', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (7, N'hodaev', N'Hodaev23', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (8, N'smolnikova', N'Smolnikova56', 2)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (9, N'paulkina', N'Paulkina457', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (10, N'sirov', N'Sirov489', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (11, N'mahner', N'Mahner257', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (12, N'zhvikov', N'Zhvikov234', 1)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (13, N'alistratov', N'Alistratov235', 1)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (14, N'korsakov', N'Korsakov367', 2)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (15, N'yagnisheva', N'Yagnisheva568', 2)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (16, N'aplenov', N'Aplenov456', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (17, N'gushin', N'Gushin234', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (18, N'shipitsin', N'Shipitsin690', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (19, N'buharov', N'Buharov678', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (20, N'andreev', N'Andreev456', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (21, N'emelin', N'Emelin456', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (22, N'teplov', N'Teplov567', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (23, N'yakyshenko', N'Yakyshenko789', 3)
INSERT [dbo].[User] ([IdUser], [LoginUser], [PasswordUser], [IdRole]) VALUES (24, N'mikov', N'Mikov345', 3)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([IdUser])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Client] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Client] ([IdClient])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Client]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_StatusOrder] FOREIGN KEY([IdStatusOrder])
REFERENCES [dbo].[StatusOrder] ([IdStatusOrder])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_StatusOrder]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_TypeOfWork] FOREIGN KEY([IdTypeOfWork])
REFERENCES [dbo].[TypeOfWork] ([IdTypeOfWork])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_TypeOfWork]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_CategoryOfService] FOREIGN KEY([IdCategory])
REFERENCES [dbo].[CategoryOfService] ([IdCategory])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_CategoryOfService]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_Education] FOREIGN KEY([IdEducation])
REFERENCES [dbo].[Education] ([IdEducation])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_Education]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_GenderStaff] FOREIGN KEY([IdGenderStaff])
REFERENCES [dbo].[GenderStaff] ([IdGenderStaff])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_GenderStaff]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_PositionAtWork] FOREIGN KEY([IdPositionAtWork])
REFERENCES [dbo].[PositionAtWork] ([IdPositionAtWork])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_PositionAtWork]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Staff_User] FOREIGN KEY([IdUser])
REFERENCES [dbo].[User] ([IdUser])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Staff_User]
GO
ALTER TABLE [dbo].[TypeOfWork]  WITH CHECK ADD  CONSTRAINT [FK_TypeOfWork_Service] FOREIGN KEY([IdService])
REFERENCES [dbo].[Service] ([IdService])
GO
ALTER TABLE [dbo].[TypeOfWork] CHECK CONSTRAINT [FK_TypeOfWork_Service]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([IdRole])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [SapunovProjectDB] SET  READ_WRITE 
GO
