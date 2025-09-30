USE [master]
GO
CREATE DATABASE [DbCaixaBanco]
GO
USE [DbCaixaBanco]
GO

CREATE TABLE [dbo].[Conta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NomeCliente] [varchar](200) NOT NULL,
	[Documento] [varchar](20) NOT NULL,
	[Saldo] [decimal](18, 4) NOT NULL,
	[DataAbertura] [datetime] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_ContaBancaria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_ContaBancaria_Documento] UNIQUE NONCLUSTERED 
(
	[Documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContaLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroDocumento] [varchar](20) NOT NULL,
	[DataDesativacao] [datetime] NOT NULL,
	[UsuarioResponsavel] [varchar](50) NOT NULL,
	[IdContaBancaria] [int] NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transferencia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdContaOrigem] [int] NOT NULL,
	[IdContaDestino] [int] NOT NULL,
	[Valor] [decimal](18, 4) NOT NULL,
	[Data] [datetime] NOT NULL,
 CONSTRAINT [PK_Transferencia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContaLog]  WITH CHECK ADD  CONSTRAINT [FK_Log_ContaBancaria] FOREIGN KEY([IdContaBancaria])
REFERENCES [dbo].[Conta] ([Id])
GO
ALTER TABLE [dbo].[ContaLog] CHECK CONSTRAINT [FK_Log_ContaBancaria]
GO
ALTER TABLE [dbo].[Transferencia]  WITH CHECK ADD  CONSTRAINT [FK_Transferencia_ContaDestino] FOREIGN KEY([IdContaDestino])
REFERENCES [dbo].[Conta] ([Id])
GO
ALTER TABLE [dbo].[Transferencia] CHECK CONSTRAINT [FK_Transferencia_ContaDestino]
GO
ALTER TABLE [dbo].[Transferencia]  WITH CHECK ADD  CONSTRAINT [FK_Transferencia_ContaOrigem] FOREIGN KEY([IdContaOrigem])
REFERENCES [dbo].[Conta] ([Id])
GO
ALTER TABLE [dbo].[Transferencia] CHECK CONSTRAINT [FK_Transferencia_ContaOrigem]
GO

