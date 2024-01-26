USE [master]
GO

/****** Object:  Database [DotNetDevSample]    Script Date: 26-01-2024 00:27:54 ******/
CREATE DATABASE [DotNetDevSample]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DotNetDevSample', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.INTELLIPAATSQL\MSSQL\DATA\DotNetDevSample.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DotNetDevSample_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.INTELLIPAATSQL\MSSQL\DATA\DotNetDevSample_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DotNetDevSample].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [DotNetDevSample] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [DotNetDevSample] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [DotNetDevSample] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [DotNetDevSample] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [DotNetDevSample] SET ARITHABORT OFF 
GO

ALTER DATABASE [DotNetDevSample] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [DotNetDevSample] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [DotNetDevSample] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [DotNetDevSample] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [DotNetDevSample] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [DotNetDevSample] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [DotNetDevSample] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [DotNetDevSample] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [DotNetDevSample] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [DotNetDevSample] SET  DISABLE_BROKER 
GO

ALTER DATABASE [DotNetDevSample] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [DotNetDevSample] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [DotNetDevSample] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [DotNetDevSample] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [DotNetDevSample] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [DotNetDevSample] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [DotNetDevSample] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [DotNetDevSample] SET RECOVERY FULL 
GO

ALTER DATABASE [DotNetDevSample] SET  MULTI_USER 
GO

ALTER DATABASE [DotNetDevSample] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [DotNetDevSample] SET DB_CHAINING OFF 
GO

ALTER DATABASE [DotNetDevSample] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [DotNetDevSample] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [DotNetDevSample] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [DotNetDevSample] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [DotNetDevSample] SET QUERY_STORE = ON
GO

ALTER DATABASE [DotNetDevSample] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [DotNetDevSample] SET  READ_WRITE 
GO

----------------------------------------------------------------------------------------------
USE [DotNetDevSample]
GO

/****** Object:  Table [dbo].[Widget]    Script Date: 26-01-2024 00:28:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Widget](
	[WidgetID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryCode] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[QuantityOnHand] [int] NOT NULL,
	[ReorderQuantity] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


--------------------------------------------------------------------------------------------------

USE [DotNetDevSample]
GO
/****** Object:  StoredProcedure [dbo].[CreateOrUpdateWidgetDetails]    Script Date: 26-01-2024 00:29:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateOrUpdateWidgetDetails]
    @WidgetID INT = NULL,
    @InventoryCode NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @QuantityOnHand INT,
    @ReorderQuantity INT
AS
BEGIN
    -- Check if the record already exists
    IF @WidgetID IS NOT NULL AND EXISTS (SELECT 1 FROM Widget WHERE WidgetID = @WidgetID)
    BEGIN
        -- Update the existing record
       UPDATE dbo.Widget
        SET InventoryCode = @InventoryCode,
            Description = @Description,
            QuantityOnHand = @QuantityOnHand,
            ReorderQuantity = @ReorderQuantity
        WHERE WidgetID = @WidgetID;
    END
    ELSE
    BEGIN
        -- Insert a new record
         INSERT INTO dbo.Widget (InventoryCode, Description, QuantityOnHand, ReorderQuantity)
        VALUES (@InventoryCode, @Description, @QuantityOnHand, @ReorderQuantity);
    END
END

---------------------------------------------------------------------------------------------
USE [DotNetDevSample]
GO
/****** Object:  StoredProcedure [dbo].[DeleteWidgetDetails]    Script Date: 26-01-2024 00:29:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteWidgetDetails]
    @WidgetID INT
AS
BEGIN
    DELETE FROM dbo.Widget
    WHERE WidgetID = @WidgetID;
END

----------------------------------------------------------------------------------------------------
USE [DotNetDevSample]
GO
/****** Object:  StoredProcedure [dbo].[GetWidgetDetailsById]    Script Date: 26-01-2024 00:30:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetWidgetDetailsById]
    @WidgetID INT
AS
BEGIN
    SELECT * FROM dbo.Widget
    WHERE WidgetID = @WidgetID;
END
-------------------------------------------------------------------------------------------------------
USE [DotNetDevSample]
GO
/****** Object:  StoredProcedure [dbo].[GetWidgetRecordCount]    Script Date: 26-01-2024 00:31:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetWidgetRecordCount]
AS
BEGIN
    SELECT COUNT(*) AS TotalWidgetRecordCount FROM dbo.Widget
END
-------------------------------------------------------------------------------------------------------
USE [DotNetDevSample]
GO
/****** Object:  StoredProcedure [dbo].[RetrieveWidgetsDetails]    Script Date: 26-01-2024 00:31:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RetrieveWidgetsDetails]
AS
BEGIN
SELECT * FROM dbo.Widget ORDER BY WidgetID DESC;
END
----------------------------------------------------------------------------------------------------------------

USE [DotNetDevSample];

-- Inserting data into the Widget table
INSERT INTO [dbo].[Widget] ([InventoryCode], [Description], [QuantityOnHand], [ReorderQuantity])
VALUES
    ('INV001', 'Widget A', 100, 20),
    ('INV002', 'Widget B', 150, 25),
    ('INV003', 'Widget C', 120, 30),
    ('INV004', 'Widget D', 200, 40),
    ('INV005', 'Widget E', 80, 15),
    ('INV006', 'Widget F', 110, 22),
    ('INV007', 'Widget G', 90, 18),
    ('INV008', 'Widget H', 180, 35),
    ('INV009', 'Widget I', 130, 28),
    ('INV010', 'Widget J', 160, 32);

-- You can add more rows as needed

