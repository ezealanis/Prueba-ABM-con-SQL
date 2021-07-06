# Prueba-ABM-con-SQL

Para funcionar se debe crear la siguiente tabla en una base SQL llamada "Persona"

USE [Persona]
GO

/****** Object:  Table [dbo].[Personas]    Script Date: 6/7/2021 19:20:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Personas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
