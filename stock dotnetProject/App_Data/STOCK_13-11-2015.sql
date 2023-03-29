USE [master]
GO
/****** Object:  Database [STOCK]    Script Date: 11/13/2015 10:44:17 ******/
CREATE DATABASE [STOCK] ON  PRIMARY 
( NAME = N'STOCK', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\STOCK.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'STOCK_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\STOCK_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [STOCK] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [STOCK].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [STOCK] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [STOCK] SET ANSI_NULLS OFF
GO
ALTER DATABASE [STOCK] SET ANSI_PADDING OFF
GO
ALTER DATABASE [STOCK] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [STOCK] SET ARITHABORT OFF
GO
ALTER DATABASE [STOCK] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [STOCK] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [STOCK] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [STOCK] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [STOCK] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [STOCK] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [STOCK] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [STOCK] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [STOCK] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [STOCK] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [STOCK] SET  DISABLE_BROKER
GO
ALTER DATABASE [STOCK] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [STOCK] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [STOCK] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [STOCK] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [STOCK] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [STOCK] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [STOCK] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [STOCK] SET  READ_WRITE
GO
ALTER DATABASE [STOCK] SET RECOVERY FULL
GO
ALTER DATABASE [STOCK] SET  MULTI_USER
GO
ALTER DATABASE [STOCK] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [STOCK] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'STOCK', N'ON'
GO
USE [STOCK]
GO
/****** Object:  Table [dbo].[Stock_District]    Script Date: 11/13/2015 10:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_District](
	[Dist_Code] [varchar](2) NOT NULL,
	[Dist_Name] [varchar](20) NULL,
 CONSTRAINT [PK_Stock_District] PRIMARY KEY CLUSTERED 
(
	[Dist_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'01', N'ANGUL')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'02', N'BALASORE')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'03', N'BARGARH')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'04', N'BHADRAK')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'05', N'BOLANGIR')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'06', N'BOUDH')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'07', N'CUTTACK')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'08', N'DEOGARH')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'09', N'DHENKANAL')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'10', N'GAJAPATI')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'11', N'GANJAM')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'12', N'JAGATSINGHPUR')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'13', N'JAJPUR')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'14', N'JHARSUGUDA')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'15', N'KALAHANDI')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'16', N'KENDRAPARA')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'17', N'KEONJHAR')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'18', N'KHURDA')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'19', N'KORAPUT')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'20', N'MALKANGIRI')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'21', N'MAYURBHANJ')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'22', N'NABARANGAPUR')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'23', N'NAYAGARH')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'24', N'NUAPADA')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'25', N'PHULBANI')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'26', N'PURI')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'27', N'RAYGADA')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'28', N'SAMBALPUR')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'29', N'SONEPUR')
INSERT [dbo].[Stock_District] ([Dist_Code], [Dist_Name]) VALUES (N'30', N'SUNDERGARH')
/****** Object:  Table [dbo].[Stock_CropVariety_Master]    Script Date: 11/13/2015 10:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_CropVariety_Master](
	[CropCatg_ID] [varchar](2) NULL,
	[Crop_ID] [varchar](4) NULL,
	[Crop_Verid] [varchar](6) NULL,
	[Crop_VerName] [varchar](50) NULL,
	[IsActive] [char](1) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010101', N'Jogesh', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010102', N'Heera', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010103', N'Kalinga-III', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010104', N'Naveen', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010105', N'Annanda', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010106', N'Sahabhagi', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010107', N'Pathara', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010108', N'Parijat', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010109', N'Satabdi', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010110', N'Vandana', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010111', N'Sidhant', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010112', N'Manaswini', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010113', N'Khandagiri', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010114', N'Udayagiri', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010115', N'D. Heera', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010116', N'Lalat', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010117', N'Konark', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010118', N'Surendra', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010119', N'Tapaswini', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010120', N'Gajapati', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010121', N'Upahar', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010122', N'Kharavela', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010123', N'MTU-1001', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010124', N'Chandan', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010125', N'Virendra', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010126', N'MTU-1010', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010127', N'RGL-2538', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010128', N'Jajati', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010129', N'Padmini', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010130', N'Kanchana', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010131', N'BPT-5204', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010132', N'Hanseswari', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010133', N'Varshadhan', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010134', N'Mahanadi', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010135', N'Swarna sub-1', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010137', N'RGL-2537', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010138', N'Geetanjali', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010139', N'RGL-2332', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010140', N'Moti', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010141', N'Lunishree', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010142', N'CR-1009', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010143', N'CR-1018', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010144', N'CR-1014', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010145', N'CR-1030', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010146', N'Pooja', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010147', N'Ramachandi', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010148', N'Panidhan', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010149', N'Jagabandhu', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010150', N'MTU-7029', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010151', N'NLR-145', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010152', N'Sarala', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010153', N'Durga', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010154', N'Tulsi', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010155', N'Ranidhan', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010156', N'Ranjeet', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010157', N'Pratikshya', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010158', N'Ketakijoha', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010159', N'Ketakijoha', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010160', N'Satyakrishna', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010161', N'Nuakalajira', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010162', N'Dhanarasi', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010163', N'Nua Dhusura', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010164', N'ADT-43', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010165', N'MTU-1032', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010166', N'PR 113', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010167', N'Samba Masuri', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010168', N'NDR 359', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010169', N'SARJU 52', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010171', N'IR 64', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010173', N'RAJNDRA MASURI-1', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010175', N'PUSA SUGANDHA-4', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010176', N'HKR-47', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010178', N'PUSA BASMATI 1', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010179', N'PUSA BASMATI 6', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010180', N'IMPROVED BASMATI', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010182', N'PANT 18', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010183', N'PRA 116', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010184', N'PUSA SUGANDH 5', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010185', N'PUSA SUGANDH-15', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010186', N'PSD 3', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010188', N'PANT 11', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010189', N'PANT 12', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010192', N'VL 85', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010193', N'VIVEK PADDY 154', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0101', N'010194', N'VIVEK PADDY 62', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0102', N'010201', N'Bhairabi', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0102', N'010202', N'Chilika', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0102', N'010203', N'Subhra', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0102', N'010204', N'GPU-48', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0103', N'010301', N'Sonalika', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0103', N'010302', N'UP-262', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0104', N'010401', N'Navjot', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0104', N'010402', N'G-5', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0104', N'010403', N'DHM-103', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0104', N'010404', N'Deccan-103', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0104', N'010405', N'Maharaja', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0104', N'010406', N'PHEM 2', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0104', N'010407', N'PHEM 5', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'01', N'0104', N'010408', N'VIVEK 31', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0201', N'020101', N'AK-12-24', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0201', N'020102', N'Kadri-5', N'Y')
GO
print 'Processed 100 total records'
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0201', N'020103', N'Kadri-6', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0201', N'020104', N'TG-37', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0201', N'020105', N'Smruti', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0201', N'020106', N'TMV-2', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0201', N'020107', N'TAG-24', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0201', N'020108', N'TG-38', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0201', N'020109', N'JL-25', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0201', N'020110', N'ICGV-91114', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0201', N'020111', N'GPBD-4', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0202', N'020201', N'M-27', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0202', N'020202', N'Anuradha', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0202', N'020203', N'Parvati', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0202', N'020204', N'P. Agrani', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0202', N'020205', N'PT-303', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0203', N'020301', N'Uma', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0203', N'020302', N'S-14', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0203', N'020303', N'Kalika', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0203', N'020304', N'GT-2', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0203', N'020305', N'Nirmala', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0204', N'020401', N'GA-10', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0205', N'020501', N'KBSH-44', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0205', N'020502', N'KBSH-1', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0205', N'020503', N'PAC-36', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0205', N'020504', N'Modern', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0206', N'020602', N'PS1092', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0206', N'020604', N'PS1242', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0206', N'020605', N'VL BHATT 65', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'02', N'0206', N'020607', N'PANT SOYA 1225', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030101', N'PDM-54', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030102', N'PDM-11', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030103', N'LGG-460', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030104', N'TARM-1', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030105', N'TARM-2', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030106', N'Pusa Baisakhi', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030107', N'PDM-139', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030108', N'OUM-11-5', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030109', N'OBGG-52', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030110', N'SML-668', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030111', N'K-851', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030112', N'IPM-2-3', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030113', N'Pusa Vishal', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030114', N'Dhauli', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030115', N'HUM-12', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030116', N'HUM-16', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030117', N'PANT 4', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0301', N'030118', N'PANT 5', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030201', N'T-9', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030202', N'Sekhar-1', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030203', N'Sekhar-2', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030204', N'Sarala', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030205', N'Ujala', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030206', N'Prasad', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030207', N'PU-30', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030208', N'PU-31', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030209', N'PU-35', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030210', N'PU-35', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030211', N'PU-19', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0302', N'030212', N'TU-94-2', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0303', N'030301', N'UPAS-120', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0303', N'030302', N'ICPL-87', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0303', N'030303', N'Laxmi', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0303', N'030304', N'Asha', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0303', N'030305', N'ICTL', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0303', N'030306', N'Maruti (ICPL-8863)', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0304', N'030401', N'ICCV-2', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0304', N'030402', N'ICCV-37', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0304', N'030403', N'Annegiri', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0304', N'030404', N'JG-11', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0305', N'030501', N'IPF-99-25', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0305', N'030502', N'Aparna', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'03', N'0305', N'030503', N'Rachana', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'04', N'0401', N'040101', N'JRO-524', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'04', N'0401', N'040102', N'JRO-66', N'Y')
INSERT [dbo].[Stock_CropVariety_Master] ([CropCatg_ID], [Crop_ID], [Crop_Verid], [Crop_VerName], [IsActive]) VALUES (N'05', N'0501', N'050101', N'Local', N'Y')
/****** Object:  Table [dbo].[Stock_CropCategory_Master]    Script Date: 11/13/2015 10:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_CropCategory_Master](
	[CropCatg_ID] [varchar](2) NOT NULL,
	[CropCatg_Name] [varchar](50) NULL,
	[IsActive] [char](1) NULL,
 CONSTRAINT [PK_Stock_CropCategory_Master] PRIMARY KEY CLUSTERED 
(
	[CropCatg_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_CropCategory_Master] ([CropCatg_ID], [CropCatg_Name], [IsActive]) VALUES (N'01', N'Cereal Crop', N'Y')
INSERT [dbo].[Stock_CropCategory_Master] ([CropCatg_ID], [CropCatg_Name], [IsActive]) VALUES (N'02', N'Oil Seed', N'Y')
INSERT [dbo].[Stock_CropCategory_Master] ([CropCatg_ID], [CropCatg_Name], [IsActive]) VALUES (N'03', N'Pulses', N'Y')
INSERT [dbo].[Stock_CropCategory_Master] ([CropCatg_ID], [CropCatg_Name], [IsActive]) VALUES (N'04', N'Fibre', N'Y')
INSERT [dbo].[Stock_CropCategory_Master] ([CropCatg_ID], [CropCatg_Name], [IsActive]) VALUES (N'05', N'Others', N'Y')
/****** Object:  Table [dbo].[Stock_Crop_Master]    Script Date: 11/13/2015 10:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_Crop_Master](
	[Crop_ID] [varchar](4) NOT NULL,
	[CropCatg_ID] [varchar](2) NULL,
	[Crop_Name] [varchar](50) NULL,
	[IsActive] [char](1) NULL,
 CONSTRAINT [PK_Stock_Crop_Master] PRIMARY KEY CLUSTERED 
(
	[Crop_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0101', N'01', N'Paddy', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0102', N'01', N'Ragi', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0103', N'01', N'Wheat', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0104', N'01', N'Maize', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0201', N'02', N'Groundnut', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0202', N'02', N'Mustard', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0203', N'02', N'Til/Sesamum', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0204', N'02', N'Niger', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0205', N'02', N'Sunflower', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0206', N'02', N'Soyabean', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0301', N'03', N'Moong', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0302', N'03', N'Biri', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0303', N'03', N'Arhar', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0304', N'03', N'Bengal Gram', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0305', N'03', N'Pea/Field Pea', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0401', N'04', N'Jute', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0402', N'04', N'Sunhemp', N'Y')
INSERT [dbo].[Stock_Crop_Master] ([Crop_ID], [CropCatg_ID], [Crop_Name], [IsActive]) VALUES (N'0501', N'05', N'Dhanicha', N'Y')
/****** Object:  Table [dbo].[Stock_Agencies_Master]    Script Date: 11/13/2015 10:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_Agencies_Master](
	[AgenciesID] [varchar](2) NOT NULL,
	[AgenciesName] [varchar](50) NULL,
	[IsActive] [char](1) NULL,
 CONSTRAINT [PK_Stock_Agencies_Master] PRIMARY KEY CLUSTERED 
(
	[AgenciesID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_Agencies_Master] ([AgenciesID], [AgenciesName], [IsActive]) VALUES (N'01', N'MOU', N'Y')
INSERT [dbo].[Stock_Agencies_Master] ([AgenciesID], [AgenciesName], [IsActive]) VALUES (N'02', N'Outside Agencies', N'Y')
INSERT [dbo].[Stock_Agencies_Master] ([AgenciesID], [AgenciesName], [IsActive]) VALUES (N'03', N'Godown', N'Y')
INSERT [dbo].[Stock_Agencies_Master] ([AgenciesID], [AgenciesName], [IsActive]) VALUES (N'04', N'Self', N'Y')
INSERT [dbo].[Stock_Agencies_Master] ([AgenciesID], [AgenciesName], [IsActive]) VALUES (N'05', N'Departmental Farm', N'Y')
INSERT [dbo].[Stock_Agencies_Master] ([AgenciesID], [AgenciesName], [IsActive]) VALUES (N'06', N'OUAT Farm', N'Y')
INSERT [dbo].[Stock_Agencies_Master] ([AgenciesID], [AgenciesName], [IsActive]) VALUES (N'07', N'Others', N'Y')
INSERT [dbo].[Stock_Agencies_Master] ([AgenciesID], [AgenciesName], [IsActive]) VALUES (N'08', N'Own', N'Y')
/****** Object:  Table [dbo].[LoginSession]    Script Date: 11/13/2015 10:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoginSession](
	[SessionID] [varchar](50) NULL,
	[UserId] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LoginAccess]    Script Date: 11/13/2015 10:44:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoginAccess](
	[AccessID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [varchar](50) NOT NULL,
	[ActivityDate] [datetime] NOT NULL,
	[UserIP] [nvarchar](50) NULL,
	[Login_Status] [varchar](10) NULL,
	[OS] [varchar](135) NULL,
	[BWSER] [nvarchar](135) NULL,
 CONSTRAINT [PK_LoginAccess] PRIMARY KEY CLUSTERED 
(
	[AccessID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[LoginAccess] ON
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (1, N'dmangul@orissaagro.com', CAST(0x0000A50F00D5191B AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (2, N'dmangul@orissaagro.com', CAST(0x0000A50F00D7CD80 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (3, N'dmangul@orissaagro.com', CAST(0x0000A51000C4CCF5 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (4, N'dmangul@orissaagro.com', CAST(0x0000A51000D66020 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (5, N'dmangul@orissaagro.com', CAST(0x0000A51000DAB81C AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (6, N'dmangul@orissaagro.com', CAST(0x0000A51000DB5EC9 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (7, N'dmangul@orissaagro.com', CAST(0x0000A51000DE88BF AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (8, N'dmangul@orissaagro.com', CAST(0x0000A51300C01B90 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (9, N'dmangul@orissaagro.com', CAST(0x0000A51300C8A162 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (10, N'dmangul@orissaagro.com', CAST(0x0000A51300D79D04 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (11, N'dmangul@orissaagro.com', CAST(0x0000A51300D9E179 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (12, N'dmangul@orissaagro.com', CAST(0x0000A51300DB17D4 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (13, N'dmangul@orissaagro.com', CAST(0x0000A51300DD6DCF AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (14, N'dmangul@orissaagro.com', CAST(0x0000A51500C42719 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (15, N'dmangul@orissaagro.com', CAST(0x0000A51500C72980 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (16, N'dmangul@orissaagro.com', CAST(0x0000A51500CF3692 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (17, N'dmangul@orissaagro.com', CAST(0x0000A51500D26841 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (18, N'dmangul@orissaagro.com', CAST(0x0000A51500D2A733 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (19, N'dmangul@orissaagro.com', CAST(0x0000A515010436FA AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (20, N'dmangul@orissaagro.com', CAST(0x0000A515011B4B55 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (21, N'dmangul@orissaagro.com', CAST(0x0000A515011CDD82 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (22, N'ossc.malkangiri@gmail.com', CAST(0x0000A515012040DE AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (23, N'dmangul@orissaagro.com', CAST(0x0000A5150121788D AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (24, N'dmangul@orissaagro.com', CAST(0x0000A515012247B4 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (25, N'ossc.malkangiri@gmail.com', CAST(0x0000A51800C70615 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (26, N'dmangul@orissaagro.com', CAST(0x0000A51800C773A9 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (27, N'dmangul@orissaagro.com', CAST(0x0000A51800F62808 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (28, N'ossc.malkangiri@gmail.com', CAST(0x0000A51800F66AAB AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (29, N'dmangul@orissaagro.com', CAST(0x0000A51800F6F6AC AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (30, N'dmangul@orissaagro.com', CAST(0x0000A51A00BDE0B4 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (31, N'dmangul@orissaagro.com', CAST(0x0000A51A00C4D9BF AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (32, N'dmangul@orissaagro.com', CAST(0x0000A51A00F6C6AB AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (33, N'dmangul@orissaagro.com', CAST(0x0000A51A00FEB906 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (34, N'dmangul@orissaagro.com', CAST(0x0000A51A00FEFC21 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (35, N'ossc.anugul@gmail.com', CAST(0x0000A51A010D6048 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (36, N'dmangul@orissaagro.com', CAST(0x0000A51A010DB361 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (37, N'dmangul@orissaagro.com', CAST(0x0000A51A011E6F4F AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (38, N'dmangul@orissaagro.com', CAST(0x0000A51B00D92211 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (39, N'ossc.malkangiri@gmail.com', CAST(0x0000A51B00D94DEB AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (40, N'dmangul@orissaagro.com', CAST(0x0000A51B00D9DE54 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (41, N'dmangul@orissaagro.com', CAST(0x0000A51B0107FE24 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (42, N'dmangul@orissaagro.com', CAST(0x0000A51B010F0590 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (43, N'dmangul@orissaagro.com', CAST(0x0000A51B012AB432 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (44, N'dmangul@orissaagro.com', CAST(0x0000A51C00CDC6DC AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (45, N'ossc.anugul@gmail.com', CAST(0x0000A51C00CF85C7 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (46, N'dmangul@orissaagro.com', CAST(0x0000A51C00CFA26E AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (47, N'dmangul@orissaagro.com', CAST(0x0000A51C00D5B5D0 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (48, N'dmangul@orissaagro.com', CAST(0x0000A51C00D7AD77 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (49, N'dmangul@orissaagro.com', CAST(0x0000A51C00D8700D AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (50, N'ossc.malkangiri@gmail.com', CAST(0x0000A51D00BC26F5 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (51, N'dmangul@orissaagro.com', CAST(0x0000A51D00BC6673 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (52, N'dmangul@orissaagro.com', CAST(0x0000A51D00FCB218 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (53, N'ossc.malkangiri@gmail.com', CAST(0x0000A51D00FD9ECF AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (54, N'dmangul@orissaagro.com', CAST(0x0000A51D00FE4033 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (55, N'dmangul@orissaagro.com', CAST(0x0000A51D0120D20F AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (56, N'dmangul@orissaagro.com', CAST(0x0000A51F00BFDCDB AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (57, N'dmangul@orissaagro.com', CAST(0x0000A51F00D5ACAA AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (58, N'ossc.malkangiri@gmail.com', CAST(0x0000A51F00D8F43E AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (59, N'ossc.malkangiri@gmail.com', CAST(0x0000A51F00E1BFC3 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (60, N'ossc.malkangiri@gmail.com', CAST(0x0000A51F00F828D6 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (61, N'dmangul@orissaagro.com', CAST(0x0000A51F00F92B56 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (62, N'dmangul@orissaagro.com', CAST(0x0000A51F00FCF43F AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (63, N'dmangul@orissaagro.com', CAST(0x0000A51F0105F0CE AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (64, N'dmangul@orissaagro.com', CAST(0x0000A51F0107EEB3 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (65, N'dmangul@orissaagro.com', CAST(0x0000A51F01091186 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (66, N'ossc.anugul@gmail.com', CAST(0x0000A51F010BA709 AS DateTime), N'::1', N'Logout', N'WinNT', N'Firefox41')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (67, N'ossc.malkangiri@gmail.com', CAST(0x0000A51F011051A3 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome42')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (68, N'dmangul@orissaagro.com', CAST(0x0000A51F012660D0 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (69, N'dmangul@orissaagro.com', CAST(0x0000A52100BBEBAD AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (70, N'dmangul@orissaagro.com', CAST(0x0000A52200CEB6A0 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (71, N'dmangul@orissaagro.com', CAST(0x0000A52200D6E5F8 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (72, N'dmangul@orissaagro.com', CAST(0x0000A52200F566D9 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (73, N'dmangul@orissaagro.com', CAST(0x0000A52201008D7D AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (74, N'dmangul@orissaagro.com', CAST(0x0000A52300CF55AA AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (75, N'dmangul@orissaagro.com', CAST(0x0000A5230115387D AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (76, N'dmangul@orissaagro.com', CAST(0x0000A52400FD7DB4 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (77, N'dmangul@orissaagro.com', CAST(0x0000A52400FE9FA2 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (78, N'dmangul@orissaagro.com', CAST(0x0000A5240108DBCF AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (79, N'dmangul@orissaagro.com', CAST(0x0000A5240114750F AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (80, N'dmangul@orissaagro.com', CAST(0x0000A52600C21251 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (81, N'dmangul@orissaagro.com', CAST(0x0000A52600C908F9 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (82, N'dmangul@orissaagro.com', CAST(0x0000A5260111A0E5 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (83, N'dmangul@orissaagro.com', CAST(0x0000A52800DC0E9B AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (84, N'dmangul@orissaagro.com', CAST(0x0000A52800E468BC AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (85, N'ossc.malkangiri@gmail.com', CAST(0x0000A52800E480DD AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (86, N'dmangul@orissaagro.com', CAST(0x0000A52900DF19A6 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (87, N'dmangul@orissaagro.com', CAST(0x0000A52A00CE6DA4 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (88, N'dmangul@orissaagro.com', CAST(0x0000A52A00CF6EBF AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (89, N'dmangul@orissaagro.com', CAST(0x0000A52A01014117 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (90, N'dmangul@orissaagro.com', CAST(0x0000A52B00B31ABC AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (91, N'dmangul@orissaagro.com', CAST(0x0000A52B00CB69B4 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (92, N'dmangul@orissaagro.com', CAST(0x0000A52B00D55410 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (93, N'dmangul@orissaagro.com', CAST(0x0000A52B01051CE3 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (94, N'ossc.malkangiri@gmail.com', CAST(0x0000A52B0105C9CC AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (95, N'ossc.anugul@gmail.com', CAST(0x0000A52B010936C5 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (96, N'ossc.anugul@gmail.com', CAST(0x0000A52C00C4C221 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (97, N'dmbalasore@orissaagro.com', CAST(0x0000A53000D957ED AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (98, N'dmangul@orissaagro.com', CAST(0x0000A53200C6BC6B AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (99, N'dmangul@orissaagro.com', CAST(0x0000A53200D28A3A AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (100, N'dmangul@orissaagro.com', CAST(0x0000A5320105863B AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
GO
print 'Processed 100 total records'
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (101, N'dmangul@orissaagro.com', CAST(0x0000A532010C7C82 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (102, N'dmangul@orissaagro.com', CAST(0x0000A53300D7DA9C AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (103, N'dmangul@orissaagro.com', CAST(0x0000A53300D82124 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (104, N'dmangul@orissaagro.com', CAST(0x0000A53300D8B5C5 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (105, N'dmangul@orissaagro.com', CAST(0x0000A53300DB43FC AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (106, N'dmangul@orissaagro.com', CAST(0x0000A533011D3AA1 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome45')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (107, N'dmangul@orissaagro.com', CAST(0x0000A53400C27526 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome46')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (108, N'dmangul@orissaagro.com', CAST(0x0000A53F00C0218C AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome46')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (109, N'dmangul@orissaagro.com', CAST(0x0000A54200C71CF6 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome46')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (110, N'dmangul@orissaagro.com', CAST(0x0000A54200D17B6A AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome46')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (111, N'dmangul@orissaagro.com', CAST(0x0000A54200D25021 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome46')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (112, N'dmangul@orissaagro.com', CAST(0x0000A54200D3980F AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome46')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (113, N'dmangul@orissaagro.com', CAST(0x0000A54200D49D67 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome46')
INSERT [dbo].[LoginAccess] ([AccessID], [UserID], [ActivityDate], [UserIP], [Login_Status], [OS], [BWSER]) VALUES (114, N'dmangul@orissaagro.com', CAST(0x0000A54200D99026 AS DateTime), N'::1', N'Logout', N'WinNT', N'Chrome46')
SET IDENTITY_INSERT [dbo].[LoginAccess] OFF
/****** Object:  StoredProcedure [dbo].[Stock_sp_GetMaxID]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[Stock_sp_GetMaxID] 
(
	@tablename varchar(200),
	@colname varchar(200)
)

as

declare @sql as NVARCHAR(512) 
declare @count as int

set @sql='select @count=(ISNULL(MAX('+ @colname + '),0) + 1)
from ' + @tablename
EXEC sp_executesql
	@query = @sql, 
    @params = N'@count INT OUTPUT', 
    @count = @count OUTPUT 
 
return @count 


/****** Object:  StoredProcedure [dbo].[ps_sp_delete_FeeAdjustment]    Script Date: 08/31/2010 13:25:49 ******/
SET ANSI_NULLS ON
GO
/****** Object:  Table [dbo].[Stock_Source_Master]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_Source_Master](
	[AgenciesID] [varchar](2) NULL,
	[SourceID] [varchar](4) NOT NULL,
	[SourceName] [varchar](50) NULL,
	[IsActive] [char](1) NULL,
 CONSTRAINT [PK_Stock_Source_Master] PRIMARY KEY CLUSTERED 
(
	[SourceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0101', N'Jagannath Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0102', N'Lal Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0103', N'Vikash Agro Tech', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0104', N'Kalinga Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0105', N'Green Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0106', N'Rajdhani Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0107', N'Bhulaxmi Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0108', N'Jaykishan Agro Firm', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0109', N'Dhana Laxmi Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0110', N'Orissa Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0111', N'Birtia Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0112', N'Sai Ram Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0113', N'Adyaa Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0114', N'Babamani Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0115', N'Champa Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0116', N'Gopinath Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0117', N'Jagannath Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0118', N'Khireswar Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0119', N'Kishan Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0120', N'Namita Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0121', N'Purnima Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0122', N'Sai Ram Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0123', N'Maa Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0124', N'Hindustan Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0125', N'Amulya Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0126', N'Tirupati Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0127', N'KABS Pvt. Ltd.', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0128', N'Jageswari Seeds', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0129', N'Jay Kishan Agro Firm', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0130', N'Sansar Agro Polo', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0131', N'Sidhabatai Agency', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'01', N'0132', N'Gramatarang Self Help', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0201', N'NSC Ltd.', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0202', N'SFCI Ltd.', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0203', N'APSSDC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0204', N'MSSC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0205', N'KSSC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0206', N'US & TDC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0207', N'APOILFED', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0208', N'GSSC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0209', N'RSSC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0210', N'WBSSC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0211', N'Bihar Veej Nigane', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0212', N'UPSSC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0213', N'ASSC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0214', N'HSSC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0215', N'HACA', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0216', N'NAFED', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0217', N'OCCF', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0218', N'NERMAC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0219', N'OAIC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'02', N'0220', N'OSSC', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'05', N'0501', N'Departmental Farm', N'Y')
INSERT [dbo].[Stock_Source_Master] ([AgenciesID], [SourceID], [SourceName], [IsActive]) VALUES (N'6', N'0502', N'OUAT Farm', N'Y')
/****** Object:  Table [dbo].[Stock_ReceiveDetails]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_ReceiveDetails](
	[Recv_ID] [int] NOT NULL,
	[Dist_Code] [varchar](2) NULL,
	[Godown_ID] [varchar](4) NULL,
	[AgenciesID] [varchar](2) NULL,
	[Receive_Unitcd] [varchar](4) NULL,
	[Outagency_Waybillnum_Farmnm] [varchar](100) NULL,
	[Challan_No] [varchar](50) NULL,
	[CropCatg_ID] [varchar](2) NULL,
	[Crop_ID] [varchar](4) NULL,
	[Crop_Verid] [varchar](6) NOT NULL,
	[Class] [varchar](50) NULL,
	[SourceID] [varchar](4) NULL,
	[Lot_No] [varchar](50) NOT NULL,
	[Bag_Size_In_kg] [varchar](10) NULL,
	[Recv_No_Of_Bags] [int] NULL,
	[Recv_Date] [datetime] NULL,
	[Recv_Quantity] [decimal](10, 2) NULL,
	[User_Type] [varchar](4) NOT NULL,
	[EntryDate] [datetime] NULL,
	[UserID] [varchar](50) NULL,
	[UserIP] [nvarchar](50) NULL,
 CONSTRAINT [PK_Stock_ReceiveDetails] PRIMARY KEY CLUSTERED 
(
	[Recv_ID] ASC,
	[Crop_Verid] ASC,
	[Lot_No] ASC,
	[User_Type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Stock_Receive_Unit_Master]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_Receive_Unit_Master](
	[AgenciesID] [varchar](2) NULL,
	[Receive_Unitcd] [varchar](4) NULL,
	[Receive_Unitname] [varchar](50) NULL,
	[User_Type] [varchar](4) NULL,
	[IsActive] [char](1) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0101', N'M/s Bhulaxmi Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0102', N'M/s Arrnapurna Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0103', N'M/s Kalinga Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0104', N'M/s Green Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0105', N'M/s Sri Sai Agro Produects', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0106', N'M/s Champa Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0107', N'M/s Purnima Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0108', N'M/s Anima Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0109', N'M/s Amulya Seeds (P) Ltd.', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0110', N'M/s Kissan Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0111', N'M/s Mahananda Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0112', N'M/s Baitarani Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0113', N'M/s Basidev Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0114', N'M/s Dhanatri Shakti Agro Earth Pvt. Ltd.', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0115', N'M/s Jai Kisan Agro Farm', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0116', N'M/s R.K. Traders', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0117', N'M/s Sri Nirmal Sai Associates', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0118', N'M/s Morden Youth Club', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0119', N'M/s Basanti Jayasingh', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0120', N'M/s Sidha Barahi Agency', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0121', N'M/s Orissa Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0122', N'M/s Sri Sai Krishna Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0123', N'M/s Sri Sairam Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0124', N'M/s Samaleswari Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0125', N'M/s TRIPTI Seed Producers Association', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0126', N'TIRUPATI SEEDS,Dunguripali,Balangir', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0127', N'M/s Sood Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0128', N'M/s Subhalaxmi Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0129', N'M/s Tirupati Seeds,Sonepur', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0130', N'M/s Sree Ram Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0131', N'M/s Odisha Seeds,Balasore', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0132', N'Jageswari Seeds,Barapali', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0133', N'Swatantra sahu', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0134', N'M/s Chandra Sekhar Agro Biotech, Dhenkanal', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0135', N'M.s Krishna Seeds, Rasulpur, Balasore', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0136', N'Sai Sangrami Mahila Mahasangha', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0137', N'M/s Satyabrata Dash,Puri', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0138', N'M/s Dhantri Shakti Agro Earth Pvt. Ltd.,Khordha', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0139', N'M/s Balaji Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0140', N'M/s Gopinath Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0141', N'M/s Jagannath Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0142', N'M/s Maa Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0143', N'M/s Birtia Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0144', N'M/s Mahalaxmi Seeds', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0145', N'M/s Kalileswar Agro', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0146', N'M/s SRIMAD SEEDS,Khalapala,Keonjhar', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0147', N'OAIC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0148', N'OSSC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0149', N'NSC Ltd.', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0150', N'SFCI Ltd.', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0151', N'APSSDC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0152', N'MSSC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0153', N'KSSC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0154', N'US & TDC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0155', N'APOILFED', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0156', N'GSSC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0157', N'RSSC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0158', N'WBSSC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0159', N'Bihar Veej Nigane', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0160', N'UPSSC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0161', N'ASSC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0162', N'HSSC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0163', N'HACA', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0164', N'NAFED', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0165', N'OCCF', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0166', N'NERMAC', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'05', N'0167', N'Departmental Farm', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'06', N'0168', N'OUAT Farm', N'OAIC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0169', N'Jagannath Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0170', N'Lal Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0171', N'Vikash Agro Tech', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0172', N'Kalinga Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0173', N'Green Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0174', N'Rajdhani Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0175', N'Bhulaxmi Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0176', N'Jaykishan Agro Firm', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0177', N'Dhana Laxmi Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0178', N'Orissa Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0179', N'Birtia Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0180', N'Sai Ram Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0181', N'Adyaa Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0182', N'Babamani Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0183', N'Champa Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0184', N'Gopinath Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0185', N'Jagannath Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0186', N'Khireswar Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0187', N'Kishan Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0188', N'Namita Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0189', N'Purnima Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0190', N'Sai Ram Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0191', N'Maa Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0192', N'Hindustan Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0193', N'Amulya Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0194', N'Tirupati Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0195', N'KABS Pvt. Ltd.', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0196', N'Jageswari Seeds', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0197', N'Jay Kishan Agro Firm', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0198', N'Sansar Agro Polo', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0199', N'Sidhabatai Agency', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'01', N'0200', N'Gramatarang Self Help', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0201', N'NSC Ltd.', N'OSSC', N'Y')
GO
print 'Processed 100 total records'
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0202', N'SFCI Ltd.', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0203', N'APSSDC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0204', N'MSSC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0205', N'KSSC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0206', N'US & TDC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0207', N'APOILFED', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0208', N'GSSC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0209', N'RSSC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0210', N'WBSSC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0211', N'Bihar Veej Nigane', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0212', N'UPSSC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0213', N'ASSC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0214', N'HSSC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0215', N'HACA', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0216', N'NAFED', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0217', N'OCCF', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0218', N'NERMAC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0219', N'OAIC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'02', N'0220', N'OSSC', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'05', N'0221', N'Departmental Farm', N'OSSC', N'Y')
INSERT [dbo].[Stock_Receive_Unit_Master] ([AgenciesID], [Receive_Unitcd], [Receive_Unitname], [User_Type], [IsActive]) VALUES (N'06', N'0222', N'OUAT Farm', N'OSSC', N'Y')
/****** Object:  Table [dbo].[Stock_OpeningBalance]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_OpeningBalance](
	[OB_ID] [int] NOT NULL,
	[Dist_Code] [varchar](2) NULL,
	[Godown_ID] [varchar](4) NULL,
	[CropCatg_ID] [varchar](2) NULL,
	[Crop_ID] [varchar](4) NULL,
	[Crop_Verid] [varchar](6) NOT NULL,
	[Class] [varchar](50) NULL,
	[SourceID] [varchar](4) NULL,
	[Lot_No] [varchar](50) NOT NULL,
	[Bag_Size_In_kg] [varchar](10) NULL,
	[Recv_No_Of_Bags] [int] NULL,
	[OB_date] [datetime] NULL,
	[OB_Quantity] [decimal](10, 2) NULL,
	[User_Type] [varchar](4) NOT NULL,
	[EntryDate] [datetime] NULL,
	[UserID] [varchar](50) NULL,
	[UserIP] [nvarchar](50) NULL,
 CONSTRAINT [PK_Stock_OpeningBalance] PRIMARY KEY CLUSTERED 
(
	[OB_ID] ASC,
	[Crop_Verid] ASC,
	[Lot_No] ASC,
	[User_Type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_OpeningBalance] ([OB_ID], [Dist_Code], [Godown_ID], [CropCatg_ID], [Crop_ID], [Crop_Verid], [Class], [SourceID], [Lot_No], [Bag_Size_In_kg], [Recv_No_Of_Bags], [OB_date], [OB_Quantity], [User_Type], [EntryDate], [UserID], [UserIP]) VALUES (1, N'01', N'0101', N'01', N'0101', N'010164', N'Certified', N'0207', N'AA123', N'40', 100, CAST(0x0000A41A00000000 AS DateTime), CAST(40.00 AS Decimal(10, 2)), N'OAIC', CAST(0x0000A54200C7C770 AS DateTime), N'dmangul@orissaagro.com', N'::1')
INSERT [dbo].[Stock_OpeningBalance] ([OB_ID], [Dist_Code], [Godown_ID], [CropCatg_ID], [Crop_ID], [Crop_Verid], [Class], [SourceID], [Lot_No], [Bag_Size_In_kg], [Recv_No_Of_Bags], [OB_date], [OB_Quantity], [User_Type], [EntryDate], [UserID], [UserIP]) VALUES (2, N'01', N'0101', N'01', N'0101', N'010105', N'Certified', N'0207', N'ASAS123', N'40', 121, CAST(0x0000A41A00000000 AS DateTime), CAST(48.40 AS Decimal(10, 2)), N'OAIC', CAST(0x0000A54200C841A1 AS DateTime), N'dmangul@orissaagro.com', N'::1')
INSERT [dbo].[Stock_OpeningBalance] ([OB_ID], [Dist_Code], [Godown_ID], [CropCatg_ID], [Crop_ID], [Crop_Verid], [Class], [SourceID], [Lot_No], [Bag_Size_In_kg], [Recv_No_Of_Bags], [OB_date], [OB_Quantity], [User_Type], [EntryDate], [UserID], [UserIP]) VALUES (3, N'01', N'0101', N'01', N'0101', N'010131', N'Certified', N'0213', N'QW12', N'10', 12, CAST(0x0000A41A00000000 AS DateTime), CAST(1.20 AS Decimal(10, 2)), N'OAIC', CAST(0x0000A54200C8A3AE AS DateTime), N'dmangul@orissaagro.com', N'::1')
INSERT [dbo].[Stock_OpeningBalance] ([OB_ID], [Dist_Code], [Godown_ID], [CropCatg_ID], [Crop_ID], [Crop_Verid], [Class], [SourceID], [Lot_No], [Bag_Size_In_kg], [Recv_No_Of_Bags], [OB_date], [OB_Quantity], [User_Type], [EntryDate], [UserID], [UserIP]) VALUES (4, N'01', N'0101', N'01', N'0101', N'010124', N'Certified', N'0205', N'QQQ1212', N'40', 1212, CAST(0x0000A41A00000000 AS DateTime), CAST(484.80 AS Decimal(10, 2)), N'OAIC', CAST(0x0000A54200C8B3FC AS DateTime), N'dmangul@orissaagro.com', N'::1')
INSERT [dbo].[Stock_OpeningBalance] ([OB_ID], [Dist_Code], [Godown_ID], [CropCatg_ID], [Crop_ID], [Crop_Verid], [Class], [SourceID], [Lot_No], [Bag_Size_In_kg], [Recv_No_Of_Bags], [OB_date], [OB_Quantity], [User_Type], [EntryDate], [UserID], [UserIP]) VALUES (5, N'01', N'0101', N'01', N'0104', N'010404', N'Certified', N'0207', N'qaqa1234', N'40', 1212, CAST(0x0000A41A00000000 AS DateTime), CAST(484.80 AS Decimal(10, 2)), N'OAIC', CAST(0x0000A54200D9B36C AS DateTime), N'dmangul@orissaagro.com', N'::1')
/****** Object:  Table [dbo].[Stock_Godown_Master]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_Godown_Master](
	[Dist_Code] [varchar](2) NULL,
	[Godown_ID] [varchar](4) NOT NULL,
	[Godown_Name] [varchar](100) NULL,
	[User_Type] [varchar](4) NULL,
	[IsActive] [char](1) NULL,
 CONSTRAINT [PK_Stock_Godown_Master] PRIMARY KEY CLUSTERED 
(
	[Godown_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'01', N'0101', N'OAIC ANGUL', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'02', N'0102', N'OAIC BALASORE', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'03', N'0103', N'OAIC BARGARH', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'03', N'0104', N'M/s Tirupati Seeds at Dunguripali', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'03', N'0105', N'M/s Arnnapurna Seeds at Sonepur', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'03', N'0106', N'M/s Balaji Seeds at Paikmal', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'04', N'0107', N'OAIC BHADRAK', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'05', N'0108', N'OAIC BOLANGIR', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'06', N'0109', N'OAIC BOUDH', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'07', N'0110', N'OAIC CUTTACK', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'08', N'0111', N'OAIC DEOGARH', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'09', N'0112', N'OAIC DHENKANAL', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'10', N'0113', N'OAIC GAJAPATI', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'11', N'0114', N'OAIC GANJAM', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'12', N'0115', N'OAIC JAGATSINGHPUR', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'13', N'0116', N'OAIC JAJPUR', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'14', N'0117', N'OAIC JHARSUGUDA', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'15', N'0118', N'OAIC KALAHANDI', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'16', N'0119', N'OAIC KENDRAPARA', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'17', N'0120', N'OAIC KEONJHAR', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'18', N'0121', N'OAIC KHURDA', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'19', N'0122', N'OAIC KORAPUT', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'20', N'0123', N'OAIC MALKANGIRI', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'21', N'0124', N'OAIC MAYURBHANJ', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'22', N'0125', N'OAIC NABARANGAPUR', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'23', N'0126', N'OAIC NAYAGARH', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'24', N'0127', N'OAIC NUAPADA', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'25', N'0128', N'OAIC PHULBANI', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'26', N'0129', N'OAIC PURI', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'27', N'0130', N'OAIC RAYGADA', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'28', N'0131', N'OAIC SAMBALPUR', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'29', N'0132', N'OAIC SONEPUR', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'30', N'0133', N'OAIC SUNDERGARH', N'OAIC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'01', N'0134', N'D.Pradhan, Godown ,Karadagadia Chhak, Angul', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'02', N'0135', N'OSSC Own Godown,Industrial Estate, Balasore (Panda Go-down)', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'03', N'0136', N'OSSC own Godown, Ekamra Chowk, Sarsara, Bargarh', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'04', N'0137', N'OSSC own godown, Haladidiha, Bhadrak.', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'05', N'0138', N'OSSC Larkipalli, Godown, Blg', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'05', N'0139', N'OSSC Own Godown, Patnagarh, Blg', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'06', N'0140', N'OSSC Own Godown, Govt Farm, Paljhar', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'07', N'0141', N'OSSC Godown, Ranihat Cold Storage Campus, Cuttack', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'07', N'0142', N'Jagatpur-II (M.R. Swainl) Godown', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'08', N'0143', N'DDA Sadar Godown', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'09', N'0144', N'OSSC own Godown, Mahisapat, Dhenkanal', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'10', N'0145', N'RURMCS, Godown, Parlakhemundi', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'11', N'0146', N'Surendranath Dash, Godown, Kamapalli,   Berhampur', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'11', N'0147', N'DDA, Sadar Godown', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'11', N'0148', N'OSSC Own Godown, Rangeilunda', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'11', N'0149', N'OSSC Own Godown, Babanpur', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'12', N'0150', N'OSSC own Godown, Nimakana', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'13', N'0151', N'OSSC own Godown, Dhabalgiri, Jajpur Road', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'14', N'0152', N'Sai Agro Godown', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'15', N'0153', N'OSSC own Godown, Arkabahalipada, Bhawanipatna', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'15', N'0154', N'RMC Go-down, Medinapur', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'15', N'0155', N'Kesinga RMC Go-down, Kalahandi', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'16', N'0156', N'OSSC own godown, Jajanga farm', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'17', N'0157', N'Seed Processing Plant, Godown, Gohira', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'17', N'0158', N'OSSC, own godown, Keonjhar', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'18', N'0159', N'Nirmala Devi Agrawal Go-down, Ranipur, Khurda', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'19', N'0160', N'OSSC own godown, Randapalli,  Jeypore', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'20', N'0161', N'Govt. Farm Godown, Mathili', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'20', N'0162', N'DDA, Central Go-down, Malkangiri', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'21', N'0163', N'OSSC own godown, Shyamakhunta, Mayurbhanj', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'21', N'0164', N'Agriculture Godown, Betanati, Mayurbhanj', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'21', N'0165', N'Ananta Parida Godown, Udala, Mayurbhanj', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'22', N'0166', N'OSSC own Godown, Nowrangpur (Dabugaon)', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'22', N'0167', N'Umarkote Plant Go-down, Nawarangpur', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'23', N'0168', N'DDA Sadar Godown, Nayagarh', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'24', N'0169', N'OSSC, Nuapada Godown', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'25', N'0170', N'DDA Sadar Godown', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'26', N'0171', N'DDA, Sadar Godown, Baramunda, Bhubaneswar', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'26', N'0172', N'OSSC own Godown, Chandaka,Bhubaneswar', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'27', N'0173', N'OSSC own godown, Rayagada', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'28', N'0174', N'K.J Godown, Khetrajpur', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'28', N'0175', N'Kuchinda OSSC own Godown', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'29', N'0176', N'OSSC Godown, Jhinki', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'30', N'0177', N'OSSC own Godown', N'OSSC', N'Y')
INSERT [dbo].[Stock_Godown_Master] ([Dist_Code], [Godown_ID], [Godown_Name], [User_Type], [IsActive]) VALUES (N'30', N'0178', N'DAO, Panposh Go-down', N'OSSC', N'Y')
/****** Object:  Table [dbo].[Stock_Users]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_Users](
	[UserID] [varchar](50) NOT NULL,
	[Passwd] [varchar](100) NULL,
	[Passwd2] [varchar](100) NULL,
	[Passwd3] [varchar](100) NULL,
	[User_Type] [varchar](4) NULL,
	[Date_Create] [datetime] NULL,
	[Last_Pwd_Change] [datetime] NULL,
	[Login_Attempt_Fail] [int] NULL,
	[Login_Attempt_Last] [datetime] NULL,
	[Login_Success_Last] [datetime] NULL,
	[Login_Status] [varchar](10) NULL,
	[SessionID] [varchar](50) NULL,
 CONSTRAINT [PK_Stock_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_Users] ([UserID], [Passwd], [Passwd2], [Passwd3], [User_Type], [Date_Create], [Last_Pwd_Change], [Login_Attempt_Fail], [Login_Attempt_Last], [Login_Success_Last], [Login_Status], [SessionID]) VALUES (N'dmangul@orissaagro.com', N'12bce374e7be15142e8172f668da00d8', N'12bce374e7be15142e8172f668da00d8', N'12bce374e7be15142e8172f668da00d8', N'OAIC', CAST(0x0000A4E700C8F38C AS DateTime), CAST(0x0000A4EA00BDC5D1 AS DateTime), 1, CAST(0x0000A4F800DB4413 AS DateTime), CAST(0x0000A54200D99029 AS DateTime), N'Logout', NULL)
INSERT [dbo].[Stock_Users] ([UserID], [Passwd], [Passwd2], [Passwd3], [User_Type], [Date_Create], [Last_Pwd_Change], [Login_Attempt_Fail], [Login_Attempt_Last], [Login_Success_Last], [Login_Status], [SessionID]) VALUES (N'dmbalasore@orissaagro.com', N'12bce374e7be15142e8172f668da00d8', N'12bce374e7be15142e8172f668da00d8', N'12bce374e7be15142e8172f668da00d8', N'OAIC', CAST(0x0000A4E700C8F38C AS DateTime), CAST(0x0000A4EA00BDC5D1 AS DateTime), 1, NULL, CAST(0x0000A53000D957F8 AS DateTime), N'Logout', NULL)
INSERT [dbo].[Stock_Users] ([UserID], [Passwd], [Passwd2], [Passwd3], [User_Type], [Date_Create], [Last_Pwd_Change], [Login_Attempt_Fail], [Login_Attempt_Last], [Login_Success_Last], [Login_Status], [SessionID]) VALUES (N'ossc.anugul@gmail.com', N'12bce374e7be15142e8172f668da00d8', N'12bce374e7be15142e8172f668da00d8', N'12bce374e7be15142e8172f668da00d8', N'OSSC', CAST(0x0000A4E700C8F38C AS DateTime), CAST(0x0000A4EA00BDC5D1 AS DateTime), 2, CAST(0x0000A51501201187 AS DateTime), CAST(0x0000A52C00C4C236 AS DateTime), N'Logout', NULL)
INSERT [dbo].[Stock_Users] ([UserID], [Passwd], [Passwd2], [Passwd3], [User_Type], [Date_Create], [Last_Pwd_Change], [Login_Attempt_Fail], [Login_Attempt_Last], [Login_Success_Last], [Login_Status], [SessionID]) VALUES (N'ossc.malkangiri@gmail.com', N'12bce374e7be15142e8172f668da00d8', N'12bce374e7be15142e8172f668da00d8', N'12bce374e7be15142e8172f668da00d8', N'OSSC', CAST(0x0000A4E700C8F38C AS DateTime), CAST(0x0000A4EA00BDC5D1 AS DateTime), 3, CAST(0x0000A515011FED75 AS DateTime), CAST(0x0000A52B0105C9D9 AS DateTime), N'Logout', NULL)
/****** Object:  Table [dbo].[Stock_UserProfile]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_UserProfile](
	[UserId] [varchar](50) NOT NULL,
	[UID] [int] NULL,
	[Dist_Code] [varchar](2) NULL,
	[Name] [varchar](30) NULL,
	[FullName] [nvarchar](50) NULL,
	[Mobile] [varchar](12) NULL,
	[Contact] [varchar](12) NULL,
	[email] [varchar](50) NULL,
	[User_Type] [varchar](4) NULL,
	[delFlag] [char](1) NULL,
	[logStatus] [char](1) NULL,
 CONSTRAINT [PK_Stock_UserProfile] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_UserProfile] ([UserId], [UID], [Dist_Code], [Name], [FullName], [Mobile], [Contact], [email], [User_Type], [delFlag], [logStatus]) VALUES (N'dmangul@orissaagro.com', 1, N'01', N'dmangul@orissaagro.com', N'OAIC - ANGUL', N'9437024358', N'06742596778', N'dmangul@orissaagro.com', N'OAIC', N'I', N'A')
INSERT [dbo].[Stock_UserProfile] ([UserId], [UID], [Dist_Code], [Name], [FullName], [Mobile], [Contact], [email], [User_Type], [delFlag], [logStatus]) VALUES (N'dmbalasore@orissaagro.com', 4, N'02', N'dmbalasore@orissaagro.com', N'OAIC - BALASORE', NULL, NULL, N'dmbalasore@orissaagro.com', N'OAIC', N'I', N'A')
INSERT [dbo].[Stock_UserProfile] ([UserId], [UID], [Dist_Code], [Name], [FullName], [Mobile], [Contact], [email], [User_Type], [delFlag], [logStatus]) VALUES (N'ossc.anugul@gmail.com', 2, N'01', N'ossc.anugul@gmail.com', N'OSSC - ANGUL', NULL, NULL, N'ossc.anugul@gmail.com', N'OSSC', N'I', N'A')
INSERT [dbo].[Stock_UserProfile] ([UserId], [UID], [Dist_Code], [Name], [FullName], [Mobile], [Contact], [email], [User_Type], [delFlag], [logStatus]) VALUES (N'ossc.malkangiri@gmail.com', 3, N'20', N'ossc.malkangiri@gmail.com', N'OSSC - MALKANGIRI', NULL, NULL, N'ossc.malkangiri@gmail.com', N'OSSC', N'I', N'A')
/****** Object:  Table [dbo].[Stock_UserActivity_Log]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_UserActivity_Log](
	[UserID] [varchar](50) NOT NULL,
	[ActivityDate] [datetime] NOT NULL,
	[ActivityType] [varchar](6) NULL,
	[UserIP] [nvarchar](50) NULL,
	[Activity] [varchar](100) NULL,
	[Remark] [varchar](100) NULL,
	[PageURL] [varchar](100) NULL,
	[OS] [varchar](135) NULL,
	[BWSER] [nvarchar](135) NULL,
 CONSTRAINT [PK_Stock_UserActivity_Log] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[ActivityDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D00CFBD4A AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Firefox41')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D00D54E3B AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Firefox41')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D00D642BE AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Firefox41')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D00D69A7F AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Firefox41')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D00D6F5E6 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Firefox41')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D00D9FFC6 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Firefox41')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D00DC56D5 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Firefox41')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D00DD938B AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Firefox41')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D00DE5C81 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Firefox41')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D010DB978 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D010E1AD8 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D010EF0D7 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D01114520 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D01133DCC AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D01142AD1 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D011549AE AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D0118D8C8 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50D0129F8DF AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50F00C5164A AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50F00C6000D AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50F00C6FF99 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50F00C76104 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50F00C85962 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50F00CA3C4D AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50F00CBA199 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50F00CE261D AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50F00D5191E AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A50F00D7CD83 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51000C4CD13 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51000D66024 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51000DAB820 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51000DB5ECD AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51000DE88C2 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51300C01BD5 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51300C8A166 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51300D79D11 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51300D9E17E AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51300DB17D9 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51300DD6DD4 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51500C4274C AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51500C72984 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51500CBCB9F AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51500CC2CCF AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51500CF3695 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51500D2684F AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51500D2A737 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A5150104370C AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A5150104EDAC AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51501051E1E AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A5150105890F AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51501060834 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51501065E5E AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A5150106ADA5 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A515011B4B61 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A515011B6851 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A515011BA234 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A515011BC312 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A515011CDD86 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51501217891 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A5150121932A AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A515012247B8 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51501225C47 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51800C773B2 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51800F6280B AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51800F6F6B0 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51800F8553F AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51800F8671B AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51A00BDE0E1 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51A00C4D9D6 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51A00F6C6BC AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51A00FEB90B AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51A00FEFC25 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51A010DB36B AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51A011E6F55 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51B00D92219 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51B00D9DE58 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51B0107FE46 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51B010F0594 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51B012AB44C AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51C00CDC6EA AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51C00CFA281 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51C00D5B5D5 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51C00D7AD7C AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51C00D87011 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51D00BC6677 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51D00FCB225 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51D00FE4043 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51D00FED8A7 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51D00FF0D60 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51D0120D22C AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51F00BFDCE9 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51F00D5ACC7 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51F00F92B5A AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51F00FCF44A AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51F0105F0DD AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51F0107EEC6 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51F0109118F AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A51F012660D4 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52100BBEBC0 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52100BD1130 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52100BD3392 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
GO
print 'Processed 100 total records'
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52100BD506F AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52200CEB6AF AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52200D6E5FC AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52200F566DD AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52201008D80 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52201054F24 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A522010561F7 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52201057DA4 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52300CF55AF AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52301153884 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52400FD7DCE AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52400FE9FA7 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A5240108DBD4 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A524010984D7 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52401147513 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52600C2127A AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52600C908FD AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A5260111A0F9 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52800DC0EAB AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52800E468C0 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52900DF19AE AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52A00CE6DB0 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52A00CF6ECA AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52A0101411B AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52B00B31AC5 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52B00CB69B7 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52B00D55413 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A52B01051CE7 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A53200C6BC7F AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A53200C76316 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A53200C7724D AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A53200D28A3D AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A5320105863F AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A532010C7C85 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A53300D7DAA0 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A53300D82127 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A53300D8B5C9 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A53300DB4400 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A533011D3AA6 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A53400C2754E AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A53F00C021D4 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A54200C71D0C AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A54200C7C771 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A54200C841A2 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A54200C8A3AE AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A54200C8B3FD AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A54200D17B76 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A54200D25025 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A54200D39817 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A54200D49D73 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A54200D99029 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmangul@orissaagro.com', CAST(0x0000A54200D9B36D AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome46')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmbalasore@orissaagro.com', CAST(0x0000A53000D957F8 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'dmbalasore@orissaagro.com', CAST(0x0000A53000D97396 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.anugul@gmail.com', CAST(0x0000A50F00CCA356 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.anugul@gmail.com', CAST(0x0000A51501201187 AS DateTime), N'Audit', N'::1', N'Login Failure', N'Passowrd Does not Match', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.anugul@gmail.com', CAST(0x0000A51A010D604C AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.anugul@gmail.com', CAST(0x0000A51C00CF8706 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.anugul@gmail.com', CAST(0x0000A51F010BA70C AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Firefox41')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.anugul@gmail.com', CAST(0x0000A52B010936C8 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.anugul@gmail.com', CAST(0x0000A52C00C4C236 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.anugul@gmail.com', CAST(0x0000A52C00C64B8A AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A50F00CD695C AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A515011FBF8D AS DateTime), N'Audit', N'::1', N'Login Failure', N'Passowrd Does not Match', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A515011FED75 AS DateTime), N'Audit', N'::1', N'Login Failure', N'Passowrd Does not Match', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A515012040E1 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A5150120BC72 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A51800C70628 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A51800C723AB AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A51800C73E9A AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A51800F66AAF AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A51B00D94DEF AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A51D00BC2702 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A51D00FD9ED4 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A51F00D8F441 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A51F00E1BFC6 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A51F00F828DC AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A51F011051AE AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome42')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A52800E480E0 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A52B0105C9D9 AS DateTime), N'Audit', N'::1', N'Login Success', N'Login Success', N'http://localhost:12736/STOCK/Login.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A52B0106CA40 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
INSERT [dbo].[Stock_UserActivity_Log] ([UserID], [ActivityDate], [ActivityType], [UserIP], [Activity], [Remark], [PageURL], [OS], [BWSER]) VALUES (N'ossc.malkangiri@gmail.com', CAST(0x0000A52B0106E777 AS DateTime), N'Action', N'::1', N'OB Entry', N'OB Entry Successfully !', N'http://localhost:12736/STOCK/Masters/Stock_OBEntry.aspx', N'WinNT', N'Chrome45')
/****** Object:  Table [dbo].[Stock_TempLoginStatus]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_TempLoginStatus](
	[SessionID] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[UserIP] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'pwlaavi15u4bu2yo1gms4zqn', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'pwlaavi15u4bu2yo1gms4zqn', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'jmd5hzf5rfb14q1i5m0rsj53', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'h130nwhbrjbxczrhi0oipq0p', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'2q4qeunaetbzp2mq0deh1d1w', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'j45ei4dgacdpup1x4j1nh40n', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'f4sqa522plxjxkvvfk5xqjoz', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N't4vpboiefz32sqweahdvjhmx', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'5d5j45ya0kucyrlpqzg2bzny', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'e3bzin3la3mtfp4ixvqof03w', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'rcu2hqckvnio0idmwczc1vp1', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'u4uolpctum532whhmwh54j3y', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'cymtslbnkbtccn3a2gdimwuk', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'5aw3kuoprpuevp35u05z0oaq', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'kfevsyjqwvwdjwxfjbjh2sqy', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ssfqclng0i3uk30qqwwl01uz', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'pzyp5bewianme1r5nkj0faeu', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'3rauforgapvj4xvdycmr5svk', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'bfca0w3u3tbddu02rh3laldm', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'zfxpgn55q3jlp2a3jdgjesx0', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'cnmn3k5png3fbbrks1o1faum', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ojuyg22gq2y25obut3amyjon', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'wkbqnpvk5zmysd2konxnz2gd', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'i4l1tvxum43afgi2v2pehve3', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ttxzynt5hfbn0gxay4od5hnr', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'uota5c4hkbmgxfmf1qguckka', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'p5vp3peecktixn34of5yupes', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'fecwabundszf4123iubjcjwe', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'mxyvoode0xse2y4uewfnmgdq', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'r4z0lfioqr2oqytjgxzbbmvx', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'wcktliyu1umi13au1yygmkw3', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'0cpryoeevfmuukdeifllgzve', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'qk3x5vsldxettplsmbtrucbi', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'bzdktjnpywschwhzsofpjbxc', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'iext5neagpztpjjlrzccw0eb', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'v1zyu3eqrs4iirvhtsr30bqv', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'v1zyu3eqrs4iirvhtsr30bqv', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'2q21rpn3q3d3fvkdcsk4zi20', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'2zja5e20gc3afaebpnqtni55', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ohur3d20spte350nab5503of', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'i4lcyypyc5prrnsffsduajhf', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'xb2wqzuhlqqldmmxbgur0kns', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'35vcqfwedmwknyq4rssxvsrw', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'zlj3h2t1h1wv0z03d3bcb1lc', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'oxgl15q5clyvkps1nijyrdo0', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'r5e2drvmprhxju22l0co1bmf', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ehqgwla4c313xpalmtixteq1', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'v14ypt1kc5dkmcv0odupem0x', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'wbybe0tienhmcnpfgvbp1z0x', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'2zbfpa2caek2b1k33n4swgh2', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'c4d5rj2meqyp5rwua515dhqk', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'10dqz1wm3pz5ucw03urh0uwe', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'aqhjcgpjjhh53v4eunkntp00', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'lm5wr3qptsfzwx5dorkmmx5w', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'qcjnozalppj2et1gonqn1hue', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'wsxmywdkuarx0pw3y0xab0nu', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'pciupdwzskvzdbcuvie0s0i2', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'se1wnz2g4agydkl3sn1heydw', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'43vuvgnaemipt41ypoopk4yv', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'i1vxz30djwadosapvlg202bx', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'wh43pisfidifzec0mbleco0t', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'0pls1a3knf1e5pjo4rw2viqp', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'oozwvr0zgkbluvxkpp5ehinu', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'vu4kew425wvqcn2xg0eokltj', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'sstttueorb4uezneq2bbg5ip', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'a2tvyctkivznwjp0pkuaampv', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'eqgxqcl4xjkhk13v42bgz1zw', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ukz3xon0fdmbkhi3kf5knrd4', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'4twdqq4surdzbfdig4tb0usx', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'41g4r3dc32drpyfdgxmkwo2j', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'zntu5gjxtjwwxwtmyvgk1bni', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'aldmwdqso4k1cxmhnsl4iwwq', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'xm0mdpzcly4zxsc0dd41rhjc', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'mgspfb3bmak54frikcr1zret', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'mgspfb3bmak54frikcr1zret', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'milowi2jowsznuume13qhuue', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'renyo3bywa4lf2g5cgur52zj', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'sdezjvxdcqpbiqowdvrq323m', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'40p3xe0xwuvryg15trspzelc', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N's02fw5bshbrscnyquauslkew', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'fongeb4zej4sq5t3pmfwiwzf', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'hjeuwhvd5nriwsm2d4ahm4rr', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'kkuizmcgzzgebjhdbieg3as5', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'1b0hfrzfhctgb5n1glouhxg4', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ne1y4uhg5zhujm0om2t01xpi', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'mamy4hb3gp4sdpdxljsbbtum', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ytt2acikutfpk5ipcxj0ia04', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'satvy4j2nnbh5e0kr2zfl14q', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'13e4v0nwuzp0n3jzso2yrr0h', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'qdnriai1whmqiaxgbqoqv1dq', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'exlvdhmqt4xvkmstjk5zslo2', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'1walmuci0wnk0zwzmlykttp4', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'yqmhboju1dnanc23a4su4bbl', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'vadtootppaokagyibgi110mp', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'zz1nibfmxuzgzhvlzsj2b1dz', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'4syleabd1a0j2wu5fni1btr1', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'2q4qeunaetbzp2mq0deh1d1w', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'eq2lrl10nzdnfzpto5pklg5f', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'wxozgsbgrkdgb5j5nvddzqwc', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'0njdri12lpiu1c0usmzqsaqr', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'luxqrx1ljiip3ag41lmrbx0u', N'Logout Option Clicked by User', N'::1')
GO
print 'Processed 100 total records'
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'z015eba0axm2a4onowkp32ht', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'j45ei4dgacdpup1x4j1nh40n', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'hyzuft2onrwwgyxriiuz1qsk', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'2lltrgiefur5t5cde22fn450', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'jfwk0ne3slibvbbb1n0c4rsd', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'220quuwlnfuxwymxb0giaoxr', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'wdf0yhtbmdblg3pn4tz4g10t', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'5k0tromf4c4lnx1wfhmky12l', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'1ygvrqknvl13rgqcenpyi4kj', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'y41p2lyfgpxihvjbwwk4fhn0', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'gx0t3bptiw3g4fdw251lr5my', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'klj0fkyz2xrp55opasudibso', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ia0iycdrti5412zcgy4auz01', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'bxsgqm0icgc13d5nbgyjr2yn', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'r5g4jrgblpewa1sss5ra3a2o', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ieevmdfsaardbhcnvfz0hsbe', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'n0kpy2xkjh33dx2olw2ue2uj', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'tna1013c1jcqjmzibcababy4', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'tna1013c1jcqjmzibcababy4', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'xzxkz1cafprmfrnizaf0455n', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'nl3qn35yfpvvsgmhwkrsw2xy', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'nl3qn35yfpvvsgmhwkrsw2xy', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'nl3qn35yfpvvsgmhwkrsw2xy', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'nl3qn35yfpvvsgmhwkrsw2xy', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'nl3qn35yfpvvsgmhwkrsw2xy', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'3mfwp2d4qqbn4sb1314gy2ip', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ixbtoptnajhdut0343a0heh4', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'pantjovr30502jrptnzj352b', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'scs11m1avxbagsj00alkqw3n', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'1o3y1uxpx4tzkaaieh0x0t3i', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'hb4qtej2xsq5upf01yqlptmk', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ndmh5fsvspqpevwhin2l4esu', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'ndmh5fsvspqpevwhin2l4esu', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'f4uwncwc2dfhbvdffm5gwmsg', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'i1vxz30djwadosapvlg202bx', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'itfv2v5dgadrrnkaucfomx4o', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'v2ni0mfdj3ze21r5ggimnuxr', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'1b0hfrzfhctgb5n1glouhxg4', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'scswdx1nhnizfqltlvnmm5y3', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'd454wu3drt13dee1qgfau1j5', N'Logout Option Clicked by User', N'::1')
INSERT [dbo].[Stock_TempLoginStatus] ([SessionID], [Status], [UserIP]) VALUES (N'iqflddqxi1znn1g2wbwrv5c0', N'Logout Option Clicked by User', N'::1')
/****** Object:  Table [dbo].[Stock_StockDetails]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_StockDetails](
	[Stock_ID] [int] NOT NULL,
	[Dist_Code] [varchar](2) NULL,
	[Godown_ID] [varchar](4) NULL,
	[CropCatg_ID] [varchar](2) NULL,
	[Crop_ID] [varchar](4) NULL,
	[Crop_Verid] [varchar](6) NOT NULL,
	[Class] [varchar](50) NULL,
	[SourceID] [varchar](4) NULL,
	[Lot_No] [varchar](50) NOT NULL,
	[Bag_Size_In_kg] [varchar](10) NULL,
	[Recv_No_Of_Bags] [int] NULL,
	[Stock_Date] [datetime] NULL,
	[Stock_Quantity] [decimal](10, 2) NULL,
	[User_Type] [varchar](4) NOT NULL,
	[EntryDate] [datetime] NULL,
	[UserID] [varchar](50) NULL,
	[UserIP] [nvarchar](50) NULL,
	[Entry_Status] [char](1) NULL,
 CONSTRAINT [PK_Stock_StockDetails] PRIMARY KEY CLUSTERED 
(
	[Stock_ID] ASC,
	[Crop_Verid] ASC,
	[Lot_No] ASC,
	[User_Type] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_StockDetails] ([Stock_ID], [Dist_Code], [Godown_ID], [CropCatg_ID], [Crop_ID], [Crop_Verid], [Class], [SourceID], [Lot_No], [Bag_Size_In_kg], [Recv_No_Of_Bags], [Stock_Date], [Stock_Quantity], [User_Type], [EntryDate], [UserID], [UserIP], [Entry_Status]) VALUES (1, N'01', N'0101', N'01', N'0101', N'010164', N'Certified', N'0207', N'AA123', N'40', 100, CAST(0x0000A41A00000000 AS DateTime), CAST(40.00 AS Decimal(10, 2)), N'OAIC', CAST(0x0000A54200C7C770 AS DateTime), N'dmangul@orissaagro.com', N'::1', N'O')
INSERT [dbo].[Stock_StockDetails] ([Stock_ID], [Dist_Code], [Godown_ID], [CropCatg_ID], [Crop_ID], [Crop_Verid], [Class], [SourceID], [Lot_No], [Bag_Size_In_kg], [Recv_No_Of_Bags], [Stock_Date], [Stock_Quantity], [User_Type], [EntryDate], [UserID], [UserIP], [Entry_Status]) VALUES (2, N'01', N'0101', N'01', N'0101', N'010105', N'Certified', N'0207', N'ASAS123', N'40', 121, CAST(0x0000A41A00000000 AS DateTime), CAST(48.40 AS Decimal(10, 2)), N'OAIC', CAST(0x0000A54200C841A1 AS DateTime), N'dmangul@orissaagro.com', N'::1', N'O')
INSERT [dbo].[Stock_StockDetails] ([Stock_ID], [Dist_Code], [Godown_ID], [CropCatg_ID], [Crop_ID], [Crop_Verid], [Class], [SourceID], [Lot_No], [Bag_Size_In_kg], [Recv_No_Of_Bags], [Stock_Date], [Stock_Quantity], [User_Type], [EntryDate], [UserID], [UserIP], [Entry_Status]) VALUES (3, N'01', N'0101', N'01', N'0101', N'010131', N'Certified', N'0213', N'QW12', N'10', 12, CAST(0x0000A41A00000000 AS DateTime), CAST(1.20 AS Decimal(10, 2)), N'OAIC', CAST(0x0000A54200C8A3AE AS DateTime), N'dmangul@orissaagro.com', N'::1', N'O')
INSERT [dbo].[Stock_StockDetails] ([Stock_ID], [Dist_Code], [Godown_ID], [CropCatg_ID], [Crop_ID], [Crop_Verid], [Class], [SourceID], [Lot_No], [Bag_Size_In_kg], [Recv_No_Of_Bags], [Stock_Date], [Stock_Quantity], [User_Type], [EntryDate], [UserID], [UserIP], [Entry_Status]) VALUES (4, N'01', N'0101', N'01', N'0101', N'010124', N'Certified', N'0205', N'QQQ1212', N'40', 1212, CAST(0x0000A41A00000000 AS DateTime), CAST(484.80 AS Decimal(10, 2)), N'OAIC', CAST(0x0000A54200C8B3FC AS DateTime), N'dmangul@orissaagro.com', N'::1', N'O')
INSERT [dbo].[Stock_StockDetails] ([Stock_ID], [Dist_Code], [Godown_ID], [CropCatg_ID], [Crop_ID], [Crop_Verid], [Class], [SourceID], [Lot_No], [Bag_Size_In_kg], [Recv_No_Of_Bags], [Stock_Date], [Stock_Quantity], [User_Type], [EntryDate], [UserID], [UserIP], [Entry_Status]) VALUES (5, N'01', N'0101', N'01', N'0104', N'010404', N'Certified', N'0207', N'qaqa1234', N'40', 1212, CAST(0x0000A41A00000000 AS DateTime), CAST(484.80 AS Decimal(10, 2)), N'OAIC', CAST(0x0000A54200D9B36C AS DateTime), N'dmangul@orissaagro.com', N'::1', N'O')
/****** Object:  Table [dbo].[Stock_Status_Hint]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stock_Status_Hint](
	[Status] [char](1) NULL,
	[FullForm] [varchar](50) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Stock_Status_Hint] ([Status], [FullForm]) VALUES (N'O', N'OPENING BALANCE')
INSERT [dbo].[Stock_Status_Hint] ([Status], [FullForm]) VALUES (N'R', N'RECEIVE STOCK')
/****** Object:  StoredProcedure [dbo].[Stock_Sp_InsReceiveDetails]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Stock_Sp_InsReceiveDetails] 
(
	@Dist_Code VARCHAR(2), 
	@Godown_ID VARCHAR(4), 
	@AgenciesID VARCHAR(2), 
	@Receive_Unitcd VARCHAR(4), 
	@Outagency_Waybillnum_Farmnm VARCHAR(10), 
	@Challan_No VARCHAR(50), 
	@CropCatg_ID VARCHAR(2), 
	@Crop_ID VARCHAR(4), 
	@Crop_Verid VARCHAR(6), 
	@Class VARCHAR(50), 
	@SourceID VARCHAR(4), 
	@Lot_No VARCHAR(50), 
	@Bag_Size_In_kg VARCHAR(10), 
	@Recv_No_Of_Bags INT, 
	@Recv_Date DATETIME, 
	@Recv_Quantity DECIMAL(10,2), 
	@User_Type VARCHAR(4), 
	@UserID VARCHAR(50), 
	@UserIP NVARCHAR(50),
	@msg varchar(50) OUTPUT
)
AS
BEGIN
	BEGIN TRAN Add_Recv	
	DECLARE @ErrorCode INT
		BEGIN
			DECLARE @Recv_ID AS INT
			EXEC @Recv_ID=dbo.Stock_sp_GetMaxID  'Stock_ReceiveDetails','Recv_ID'	
			
			INSERT INTO Stock_ReceiveDetails( Recv_ID,Dist_Code, Godown_ID, AgenciesID, Receive_Unitcd, Outagency_Waybillnum_Farmnm, Challan_No, CropCatg_ID, Crop_ID, Crop_Verid, Class, SourceID, Lot_No, Bag_Size_In_kg, Recv_No_Of_Bags, Recv_Date, Recv_Quantity, User_Type, EntryDate, UserID, UserIP )
			VALUES (@Recv_ID,@Dist_Code,@Godown_ID,@AgenciesID,@Receive_Unitcd,@Outagency_Waybillnum_Farmnm,@Challan_No,@CropCatg_ID,@Crop_ID,@Crop_Verid,@Class,@SourceID,@Lot_No,@Bag_Size_In_kg,@Recv_No_Of_Bags,@Recv_Date,@Recv_Quantity,@User_Type,GETDATE(),@UserID,@UserIP)		
			
			DECLARE @Stock_ID AS INT
			EXEC @Stock_ID=dbo.Stock_sp_GetMaxID  'Stock_StockDetails','Stock_ID'
			
			INSERT INTO dbo.Stock_StockDetails (Stock_ID, Dist_Code, Godown_ID, CropCatg_ID, Crop_ID, Crop_Verid, Class, SourceID, Lot_No, Bag_Size_In_kg, Recv_No_Of_Bags, Stock_Date, Stock_Quantity, User_Type, EntryDate, UserID, UserIP, Entry_Status)
			VALUES (@Stock_ID, @Dist_Code, @Godown_ID, @CropCatg_ID, @Crop_ID, @Crop_Verid, @Class, @SourceID, @Lot_No, @Bag_Size_In_kg, @Recv_No_Of_Bags, @Recv_Date, @Recv_Quantity, @User_Type, GETDATE(), @UserID, @UserIP, 'R')	
			SET @ErrorCode=@@error
		END
	IF (@ErrorCode=0)
	BEGIN
		COMMIT TRAN Add_OB
		SELECT @msg='Receive Detail Added Successfully'
	END	
	ELSE 
	BEGIN
		ROLLBACK TRAN Add_OB
		SELECT @msg='Problem Accessing Database. Please Try Again'
	END
END
GO
/****** Object:  StoredProcedure [dbo].[Stock_Sp_InsOpeningBalance]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Stock_Sp_InsOpeningBalance] 
(
	@Dist_Code VARCHAR(2), 
	@Godown_ID VARCHAR(4), 
	@CropCatg_ID VARCHAR(2), 
	@Crop_ID VARCHAR(4), 
	@Crop_Verid VARCHAR(6), 
	@Class VARCHAR(50), 
	@SourceID VARCHAR(4), 
	@Lot_No VARCHAR(50), 
	@Bag_Size_In_kg VARCHAR(10), 
	@Recv_No_Of_Bags VARCHAR(10), 
	@OB_date DATETIME, 
	@OB_Quantity DECIMAL(10,2), 
	@User_Type VARCHAR(4),
	@UserID VARCHAR(50), 
	@UserIP NVARCHAR(50),
	@msg varchar(50) OUTPUT
)
AS
BEGIN
	BEGIN TRAN Add_OB		
	DECLARE @ErrorCode INT
		BEGIN
			DECLARE @OB_ID AS INT
			EXEC @OB_ID=dbo.Stock_sp_GetMaxID  'Stock_OpeningBalance','OB_ID'	
			
			INSERT INTO dbo.Stock_OpeningBalance (OB_ID, Dist_Code, Godown_ID, CropCatg_ID, Crop_ID, Crop_Verid, Class, SourceID, Lot_No, Bag_Size_In_kg, Recv_No_Of_Bags, OB_date, OB_Quantity, User_Type, EntryDate, UserID, UserIP)
			VALUES(@OB_ID,@Dist_Code,@Godown_ID,@CropCatg_ID,@Crop_ID,@Crop_Verid,@Class,@SourceID, @Lot_No, @Bag_Size_In_kg, @Recv_No_Of_Bags, @OB_date, @OB_Quantity, @User_Type, GETDATE(), @UserID, @UserIP)	
			
			DECLARE @Stock_ID AS INT
			EXEC @Stock_ID=dbo.Stock_sp_GetMaxID  'Stock_StockDetails','Stock_ID'
			
			INSERT INTO dbo.Stock_StockDetails (Stock_ID, Dist_Code, Godown_ID, CropCatg_ID, Crop_ID, Crop_Verid, Class, SourceID, Lot_No, Bag_Size_In_kg, Recv_No_Of_Bags, Stock_Date, Stock_Quantity, User_Type, EntryDate, UserID, UserIP, Entry_Status)
			VALUES (@Stock_ID, @Dist_Code, @Godown_ID, @CropCatg_ID, @Crop_ID, @Crop_Verid, @Class, @SourceID, @Lot_No, @Bag_Size_In_kg, @Recv_No_Of_Bags, @OB_date, @OB_Quantity, @User_Type, GETDATE(), @UserID, @UserIP, 'O')	
			SET @ErrorCode=@@error
		END
	IF (@ErrorCode=0)
	BEGIN
		COMMIT TRAN Add_OB
		SELECT @msg='OB Detail Added Successfully'
	END	
	ELSE 
	BEGIN
		ROLLBACK TRAN Add_OB
		SELECT @msg='Problem Accessing Database. Please Try Again'
	END
END
GO
/****** Object:  StoredProcedure [dbo].[Stock_SP_InsertLoginAccess]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Stock_SP_InsertLoginAccess]
(
	@UserID varchar(50),
	@UserIP nvarchar(50),
	@OS varchar(135),
	@BWSER nvarchar(135),
	@retVal int OUTPUT
)
AS
BEGIN
	INSERT INTO LoginAccess(UserID,ActivityDate,UserIP,Login_Status,OS,BWSER)
		VALUES(@UserID,getdate(),@UserIP,'Login',@OS,@BWSER)
		SELECT @retVal=@@IDENTITY
END
GO
/****** Object:  StoredProcedure [dbo].[Stock_SP_InsertActivityLog]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Stock_SP_InsertActivityLog]
(
	@UserID varchar(50),
	@UserIP nvarchar(20),
	@ActivityType varchar(6),
	@Activity varchar(100),
	@PageURL varchar(100),
	@Remark varchar(100),
	@OS varchar(135),
	@BWSER nvarchar(135),
	@retVal int OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRANSACTION
		IF EXISTS (SELECT UserID FROM Stock_Users WHERE UserID=@UserID)
		BEGIN
			INSERT INTO Stock_UserActivity_Log(UserID,ActivityType,ActivityDate,UserIP,Activity,PageURL,Remark,OS,BWSER)
				VALUES(@UserID,@ActivityType,getdate(),@UserIP,@Activity,@PageURL,@Remark,@OS,@BWSER)
			IF (@Activity = 'Login Failure')
				UPDATE Stock_Users SET Login_Attempt_Fail=ISNULL(Login_Attempt_Fail,0)+1, Login_Attempt_Last=getdate() WHERE UserID=@UserID
			IF (@Activity = 'Login Success')
				UPDATE Stock_Users SET Login_Success_Last=getdate() WHERE UserID=@UserID
		END
		ELSE
			INSERT INTO Stock_UserActivity_Log(UserID,ActivityType,ActivityDate,UserIP,Activity,PageURL,Remark,OS,BWSER)
				VALUES(@UserID,@ActivityType,getdate(),@UserIP,@Activity,@PageURL,@Remark,@OS,@BWSER)
		COMMIT
		SELECT @retVal=@@error
	END TRY
	BEGIN CATCH
		IF (@@trancount>0)
		BEGIN
			ROLLBACK
			SELECT @retVal=1
			DECLARE @ErrMsg varchar(1000), @ErrSeverity int
			SELECT @ErrMsg = ERROR_MESSAGE(),@ErrSeverity = ERROR_SEVERITY()

			RAISERROR(@ErrMsg, @ErrSeverity, 1)
		END
		
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Stock_GetPassword]    Script Date: 11/13/2015 10:44:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[Stock_GetPassword]
(
	@UserID varchar(50)
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT a.User_Type,a.UserID,a.Passwd,b.Name,b.UID,
	CONVERT(VARCHAR(10),Datediff(MINUTE,a.Date_Create,a.Last_Pwd_Change)) time_diff,b.Dist_Code,c.Dist_Name,UPPER(b.FullName) AS FullName
	FROM Stock_Users a,Stock_UserProfile b,Stock_District c
	WHERE a.UserID=b.UserID and b.Dist_Code=c.Dist_Code and a.UserID=@UserID 

END
GO
/****** Object:  Default [DF_Stock_UserProfile_delFlag]    Script Date: 11/13/2015 10:44:19 ******/
ALTER TABLE [dbo].[Stock_UserProfile] ADD  CONSTRAINT [DF_Stock_UserProfile_delFlag]  DEFAULT ('A') FOR [delFlag]
GO
/****** Object:  Default [DF_Stock_UserProfile_logStatus]    Script Date: 11/13/2015 10:44:19 ******/
ALTER TABLE [dbo].[Stock_UserProfile] ADD  CONSTRAINT [DF_Stock_UserProfile_logStatus]  DEFAULT ('I') FOR [logStatus]
GO
