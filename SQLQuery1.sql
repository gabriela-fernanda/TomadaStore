CREATE DATABASE [TomadaStore.CustomerDB]

USE [TomadaStore.CustomerDB]

CREATE TABLE [dbo].[Customers](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL UNIQUE,
	[PhoneNumber] [nvarchar](15) NULL,
	[Status] [bit] NOT NULL DEFAULT 1
	)




select * from Customers