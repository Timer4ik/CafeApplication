USE [master]
GO
/****** Object:  Database [Cafe]    Script Date: 10.03.2024 23:07:26 ******/
CREATE DATABASE [Cafe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Cafe', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Cafe.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Cafe_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Cafe_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Cafe] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cafe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cafe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cafe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cafe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cafe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cafe] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cafe] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Cafe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cafe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cafe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cafe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cafe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cafe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cafe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cafe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cafe] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Cafe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cafe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cafe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cafe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cafe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cafe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cafe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cafe] SET RECOVERY FULL 
GO
ALTER DATABASE [Cafe] SET  MULTI_USER 
GO
ALTER DATABASE [Cafe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cafe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Cafe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Cafe] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Cafe] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Cafe] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Cafe', N'ON'
GO
ALTER DATABASE [Cafe] SET QUERY_STORE = ON
GO
ALTER DATABASE [Cafe] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Cafe]
GO
/****** Object:  Table [dbo].[Change]    Script Date: 10.03.2024 23:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Change](
	[ChangeId] [int] IDENTITY(1,1) NOT NULL,
	[ChangeDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Change] PRIMARY KEY CLUSTERED 
(
	[ChangeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChangeEmployee]    Script Date: 10.03.2024 23:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChangeEmployee](
	[ChangeEmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ChangeId] [int] NOT NULL,
 CONSTRAINT [PK_ChangeEmployee] PRIMARY KEY CLUSTERED 
(
	[ChangeEmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 10.03.2024 23:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[FoodId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [float] NOT NULL,
	[FoodCategoryId] [int] NOT NULL,
	[Photo] [nvarchar](250) NULL,
 CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED 
(
	[FoodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodCategory]    Script Date: 10.03.2024 23:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodCategory](
	[FoodCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Photo] [nvarchar](250) NULL,
 CONSTRAINT [PK_FoodCategory] PRIMARY KEY CLUSTERED 
(
	[FoodCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 10.03.2024 23:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[TableId] [int] NOT NULL,
	[GuestsCount] [int] NOT NULL,
	[ChangeId] [int] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderFood]    Script Date: 10.03.2024 23:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderFood](
	[OrderFoodId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[FoodId] [int] NOT NULL,
 CONSTRAINT [PK_OrderFood] PRIMARY KEY CLUSTERED 
(
	[OrderFoodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10.03.2024 23:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Slug] [nvarchar](100) NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Table]    Script Date: 10.03.2024 23:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Table](
	[TableId] [int] IDENTITY(1,1) NOT NULL,
	[Num] [nvarchar](50) NOT NULL,
	[SeatAmount] [int] NOT NULL,
 CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED 
(
	[TableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10.03.2024 23:07:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[UserRoleId] [int] NOT NULL,
	[Email] [nvarchar](150) NULL,
	[Password] [nvarchar](150) NULL,
	[Photo] [nvarchar](250) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Change] ON 

INSERT [dbo].[Change] ([ChangeId], [ChangeDate]) VALUES (3, CAST(N'2024-03-10T00:00:00.000' AS DateTime))
INSERT [dbo].[Change] ([ChangeId], [ChangeDate]) VALUES (5, CAST(N'2024-03-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Change] ([ChangeId], [ChangeDate]) VALUES (6, CAST(N'2024-03-30T00:00:00.000' AS DateTime))
INSERT [dbo].[Change] ([ChangeId], [ChangeDate]) VALUES (7, CAST(N'2024-03-24T00:00:00.000' AS DateTime))
INSERT [dbo].[Change] ([ChangeId], [ChangeDate]) VALUES (8, CAST(N'2024-07-05T00:00:00.000' AS DateTime))
INSERT [dbo].[Change] ([ChangeId], [ChangeDate]) VALUES (9, CAST(N'2024-08-16T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Change] OFF
GO
SET IDENTITY_INSERT [dbo].[ChangeEmployee] ON 

INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (1, 4, 5)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (2, 4, 3)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (3, 3, 3)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (4, 2, 3)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (5, 6, 3)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (6, 1, 3)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (7, 5, 3)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (8, 5, 6)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (9, 5, 7)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (10, 5, 8)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (11, 5, 9)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (12, 4, 9)
INSERT [dbo].[ChangeEmployee] ([ChangeEmployeeId], [UserId], [ChangeId]) VALUES (13, 6, 9)
SET IDENTITY_INSERT [dbo].[ChangeEmployee] OFF
GO
SET IDENTITY_INSERT [dbo].[Food] ON 

INSERT [dbo].[Food] ([FoodId], [Name], [Price], [FoodCategoryId], [Photo]) VALUES (0, N'Капучино с клиновым сиропом', 323, 2, N'3208a01e-8be1-4ba9-9288-0b34faa89c3b.jpg')
INSERT [dbo].[Food] ([FoodId], [Name], [Price], [FoodCategoryId], [Photo]) VALUES (1, N'32', 423, 2, N'd00721f7-3460-486d-8efb-f009d094e9d9.jpg')
SET IDENTITY_INSERT [dbo].[Food] OFF
GO
SET IDENTITY_INSERT [dbo].[FoodCategory] ON 

INSERT [dbo].[FoodCategory] ([FoodCategoryId], [Name], [Photo]) VALUES (1, N'Треугольник с курицей', N'f8e9e53e-d01b-4f0c-94fb-e8c171cd29d9.jpg')
INSERT [dbo].[FoodCategory] ([FoodCategoryId], [Name], [Photo]) VALUES (2, N'Капучино', N'804cdcda-a24e-4a48-9ada-1dff9776bfc3.jpg')
SET IDENTITY_INSERT [dbo].[FoodCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderId], [TableId], [GuestsCount], [ChangeId]) VALUES (4, 1, 1234444, 3)
INSERT [dbo].[Order] ([OrderId], [TableId], [GuestsCount], [ChangeId]) VALUES (5, 1, 545, 3)
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([UserRoleId], [Name], [Slug]) VALUES (1, N'Администратор', N'admin')
INSERT [dbo].[Role] ([UserRoleId], [Name], [Slug]) VALUES (2, N'Повар', N'cook')
INSERT [dbo].[Role] ([UserRoleId], [Name], [Slug]) VALUES (3, N'Официант', N'waiter')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Table] ON 

INSERT [dbo].[Table] ([TableId], [Num], [SeatAmount]) VALUES (1, N'123', 3)
SET IDENTITY_INSERT [dbo].[Table] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [FullName], [UserRoleId], [Email], [Password], [Photo]) VALUES (1, N'Артём', 1, N'admin', N'admin', NULL)
INSERT [dbo].[User] ([UserId], [FullName], [UserRoleId], [Email], [Password], [Photo]) VALUES (2, N'Андрей', 1, NULL, NULL, NULL)
INSERT [dbo].[User] ([UserId], [FullName], [UserRoleId], [Email], [Password], [Photo]) VALUES (3, N'Максим', 2, NULL, NULL, NULL)
INSERT [dbo].[User] ([UserId], [FullName], [UserRoleId], [Email], [Password], [Photo]) VALUES (4, N'Валерия1 Валерия1 Валерия1', 2, N'Валерия1@', N'Валерия1', NULL)
INSERT [dbo].[User] ([UserId], [FullName], [UserRoleId], [Email], [Password], [Photo]) VALUES (5, N'Игорь', 3, NULL, NULL, NULL)
INSERT [dbo].[User] ([UserId], [FullName], [UserRoleId], [Email], [Password], [Photo]) VALUES (6, N'Овечкин Михаил Лермонтович', 3, N'michael@michael.michael', N'michael@michael.michael', N'f7549d8e-321b-49f4-86b6-50cf4793efaa.jpg')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[ChangeEmployee]  WITH CHECK ADD  CONSTRAINT [FK_ChangeEmployee_Change] FOREIGN KEY([ChangeId])
REFERENCES [dbo].[Change] ([ChangeId])
GO
ALTER TABLE [dbo].[ChangeEmployee] CHECK CONSTRAINT [FK_ChangeEmployee_Change]
GO
ALTER TABLE [dbo].[ChangeEmployee]  WITH CHECK ADD  CONSTRAINT [FK_ChangeEmployee_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[ChangeEmployee] CHECK CONSTRAINT [FK_ChangeEmployee_User]
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD  CONSTRAINT [FK_Food_FoodCategory] FOREIGN KEY([FoodCategoryId])
REFERENCES [dbo].[FoodCategory] ([FoodCategoryId])
GO
ALTER TABLE [dbo].[Food] CHECK CONSTRAINT [FK_Food_FoodCategory]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Change] FOREIGN KEY([ChangeId])
REFERENCES [dbo].[Change] ([ChangeId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Change]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Table] FOREIGN KEY([TableId])
REFERENCES [dbo].[Table] ([TableId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Table]
GO
ALTER TABLE [dbo].[OrderFood]  WITH CHECK ADD  CONSTRAINT [FK_OrderFood_Food] FOREIGN KEY([FoodId])
REFERENCES [dbo].[Food] ([FoodId])
GO
ALTER TABLE [dbo].[OrderFood] CHECK CONSTRAINT [FK_OrderFood_Food]
GO
ALTER TABLE [dbo].[OrderFood]  WITH CHECK ADD  CONSTRAINT [FK_OrderFood_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderFood] CHECK CONSTRAINT [FK_OrderFood_Order]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserRole] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[Role] ([UserRoleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserRole]
GO
USE [master]
GO
ALTER DATABASE [Cafe] SET  READ_WRITE 
GO
