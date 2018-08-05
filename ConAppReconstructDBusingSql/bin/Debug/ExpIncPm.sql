IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMExcelOutputHR]') AND type in (N'U'))
DROP TABLE [dbo].[PMExcelOutputHR]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PMExcelOutputHR](
	[BATCH_NAME] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ELEMENT_TYPE] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ASSIGNMENT_NUMBER] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PAY_VALUE] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMExpressRaw]') AND type in (N'U'))
DROP TABLE [dbo].[PMExpressRaw]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PMExpressRaw](
	[GKEY] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EQ_NBR] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FROM_CHE_ID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FROM_CHE_OPR] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TO_CHE_ID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TO_CHE_OPR] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FROM_POS_ID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TO_POS_ID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FROM_LOC_TYPE] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TO_LOC_TYPE] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TRANSACTIONS] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CARRY_CHE_ID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CARRY_CHE_OPR] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CARRY_STARTED] [datetime] NULL,
	[PUT_COMPLETED] [datetime] NULL,
	[SHIP_ID] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VOY_NBR] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MOVE_TYPE] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FETCH_SPCL] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PERFORMED] [datetime] NULL
) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMFinalPayout]') AND type in (N'U'))
DROP TABLE [dbo].[PMFinalPayout]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PMFinalPayout](
	[MonthYear] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Week] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OperatorId] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PMMoves] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Shift] [datetime] NULL,
	[BookDateTimeBegin] [datetime] NULL,
	[BookDateTimeEnd] [datetime] NULL,
	[MoveToGo] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CurrentPayout] [float] NULL,
	[Bonus1] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Bonus2] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Bonus3] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MoveTarget1] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MoveTarget2] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MoveTarget3] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PayoutTarget1] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PayoutTarget2] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PayoutTarget3] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Skill] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMFinalPayoutHR]') AND type in (N'U'))
DROP TABLE [dbo].[PMFinalPayoutHR]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PMFinalPayoutHR](
	[MonthYear] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OperatorId] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Skill] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PMMoves] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Payout] [float] NULL
) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMTotalMoves]') AND type in (N'U'))
DROP TABLE [dbo].[PMTotalMoves]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PMTotalMoves](
	[OperatorId] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PMMoves] [float] NULL,
	[Shift] [datetime] NULL,
	[StartShift] [datetime] NULL,
	[EndShift] [datetime] NULL
) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMTotalMoves2V]') AND type in (N'U'))
DROP TABLE [dbo].[PMTotalMoves2V]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PMTotalMoves2V](
	[OperatorId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PMMoves] [float] NULL,
	[Shift] [datetime] NULL,
	[StartShift] [datetime] NULL,
	[EndShift] [datetime] NULL,
	[Added] [int] NULL
) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMTotalMovesDelete]') AND type in (N'U'))
DROP TABLE [dbo].[PMTotalMovesDelete]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PMTotalMovesDelete](
	[OperatorId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PMMoves] [float] NULL,
	[Shift] [datetime] NULL,
	[StartShift] [datetime] NULL,
	[EndShift] [datetime] NULL
) ON [PRIMARY]

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMTotalMovesFinal]') AND type in (N'U'))
DROP TABLE [dbo].[PMTotalMovesFinal]
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[PMTotalMovesFinal](
	[OperatorId] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PMMoves] [float] NULL,
	[Shift] [datetime] NULL,
	[StartShift] [datetime] NULL,
	[EndShift] [datetime] NULL
) ON [PRIMARY]

