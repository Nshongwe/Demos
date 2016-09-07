SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Devi](
	[DeviceID] [nvarchar](30) NOT NULL,
	[DeviceName] [nvarchar](50) NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[DateRegistered] [smalldatetime] NULL,
	[RegIPAddress] [nvarchar](20) NULL,
	[DeviceTypeID] [int] NULL,
	[RegIPAddressLocation] [nvarchar](50) NULL,
 CONSTRAINT [PK_Devi] PRIMARY KEY CLUSTERED 
(
	[DeviceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

