SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[WriteTrace]
(
	@InstanceName [nvarchar](1000),
	@EventSourceId [uniqueidentifier],
	@EventSourceName [nvarchar](1000),
	@EventId [int],
	@EventKeywords [bigint],
	@Level [nvarchar](50),
	@Opcode [int],
	@Task [int],
	@Timestamp [datetimeoffset](7),
	@Version [tinyint],
	@RawMessage [nvarchar](max),
	@FormattedMessage [nvarchar](max),
	@Payload [nvarchar](max),
	@TraceId [bigint] OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Traces] (
		[InstanceName],
		[EventSourceId],
		[EventSourceName],
		[EventId],
		[EventKeywords],
		[Level],
		[Opcode],
		[Task],
		[Timestamp],
		[Version],
		[RawMessage],
		[FormattedMessage],
		[Payload]
	)
	VALUES (
		@InstanceName,
	    @EventSourceId,
	    @EventSourceName,
		@EventId,
		@EventKeywords,
		@Level,
		@Opcode,
		@Task,
		@Timestamp,
		@Version,
		@RawMessage,
		@FormattedMessage,
		@Payload)

	SET @TraceId = @@IDENTITY
	RETURN @TraceId
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Traces](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[InstanceName] [nvarchar](1000) NOT NULL,
	[EventSourceId] [uniqueidentifier] NOT NULL,
	[EventSourceName] [nvarchar](1000) NOT NULL,
	[EventId] [int] NOT NULL,
	[EventKeywords] [bigint] NOT NULL,
	[Level] [nvarchar](50) NOT NULL,
	[Opcode] [int] NOT NULL,
	[Task] [int] NOT NULL,
	[Timestamp] [datetimeoffset](7) NOT NULL,
	[Version] [tinyint] NOT NULL,
	[RawMessage] [nvarchar](max) NULL,
	[FormattedMessage] [nvarchar](max) NULL,
	[Payload] [nvarchar](max) NULL,
 CONSTRAINT [PK_Traces] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF)
)

GO
