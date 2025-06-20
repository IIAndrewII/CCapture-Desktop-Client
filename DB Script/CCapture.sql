USE [CCaptureClientDB]
GO
/****** Object:  Table [dbo].[API_batch_class]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[API_batch_class](
	[id_batch_class] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_batch_class] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[API_batch_field_def]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[API_batch_field_def](
	[id_batch_field_def] [int] IDENTITY(1,1) NOT NULL,
	[id_batch_class] [int] NOT NULL,
	[field_name] [nvarchar](255) NOT NULL,
	[id_field_type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_batch_field_def] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[API_document_class]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[API_document_class](
	[id_document_class] [int] IDENTITY(1,1) NOT NULL,
	[id_batch_class] [int] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_document_class] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[API_field_type]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[API_field_type](
	[id_field_type] [int] IDENTITY(1,1) NOT NULL,
	[type_name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_field_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[API_page_type]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[API_page_type](
	[id_page_type] [int] IDENTITY(1,1) NOT NULL,
	[id_document_class] [int] NOT NULL,
	[name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id_page_type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchClasses]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchClasses](
	[BatchClassId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BatchClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Batches]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Batches](
	[BatchId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CloseDate] [datetime] NOT NULL,
	[BatchClassId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BatchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchFields]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchFields](
	[BatchFieldId] [int] IDENTITY(1,1) NOT NULL,
	[BatchId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[Confidence] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BatchFieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BatchStates]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchStates](
	[BatchStateId] [int] IDENTITY(1,1) NOT NULL,
	[BatchId] [int] NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
	[TrackDate] [datetime] NOT NULL,
	[Workstation] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[BatchStateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocumentFields]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentFields](
	[DocumentFieldId] [int] IDENTITY(1,1) NOT NULL,
	[VerificationDocumentId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[Confidence] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[DocumentFieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Documents]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documents](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[SubmissionId] [int] NOT NULL,
	[FilePath] [nvarchar](500) NOT NULL,
	[PageType] [nvarchar](50) NULL,
	[FileName] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fields]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fields](
	[FieldId] [int] IDENTITY(1,1) NOT NULL,
	[SubmissionId] [int] NOT NULL,
	[FieldName] [nvarchar](100) NOT NULL,
	[FieldValue] [nvarchar](500) NOT NULL,
	[FieldType] [nvarchar](50) NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[FieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NOT NULL,
	[IsSubmitted] [bit] NOT NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pages](
	[PageId] [int] IDENTITY(1,1) NOT NULL,
	[VerificationDocumentId] [int] NOT NULL,
	[FileName] [nvarchar](255) NOT NULL,
	[Sections] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[PageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageTypes]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageTypes](
	[PageTypeId] [int] IDENTITY(1,1) NOT NULL,
	[PageId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Confidence] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PageTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Signatures]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Signatures](
	[SignatureId] [int] IDENTITY(1,1) NOT NULL,
	[VerificationDocumentId] [int] NOT NULL,
	[SignatureData] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[SignatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Submissions]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Submissions](
	[SubmissionId] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[BatchClassName] [nvarchar](100) NOT NULL,
	[SourceSystem] [nvarchar](50) NOT NULL,
	[Channel] [nvarchar](50) NOT NULL,
	[SessionId] [nvarchar](50) NOT NULL,
	[MessageId] [nvarchar](50) NOT NULL,
	[UserCode] [nvarchar](50) NOT NULL,
	[InteractionDateTime] [datetime] NOT NULL,
	[RequestGuid] [nvarchar](36) NOT NULL,
	[AuthToken] [nvarchar](500) NULL,
	[SubmittedAt] [datetime] NULL,
	[Checked_GUID] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VerificationDocumentClasses]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VerificationDocumentClasses](
	[DocumentClassId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DocumentClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VerificationDocuments]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VerificationDocuments](
	[VerificationDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[BatchId] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[DocumentClassId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[VerificationDocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VerificationResponses]    Script Date: 5/22/2025 3:33:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VerificationResponses](
	[VerificationResponseId] [int] IDENTITY(1,1) NOT NULL,
	[BatchId] [int] NULL,
	[Status] [int] NOT NULL,
	[ExecutionDate] [datetime] NOT NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[RequestGuid] [nvarchar](255) NULL,
	[SourceSystem] [nvarchar](50) NULL,
	[Channel] [nvarchar](50) NULL,
	[SessionId] [nvarchar](100) NULL,
	[MessageId] [nvarchar](100) NULL,
	[UserCode] [nvarchar](50) NULL,
	[InteractionDateTime] [datetime] NULL,
	[ResponseJson] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[VerificationResponseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[API_batch_class] ON 

INSERT [dbo].[API_batch_class] ([id_batch_class], [name]) VALUES (1, N'DDI Classification and Field Extraction')
INSERT [dbo].[API_batch_class] ([id_batch_class], [name]) VALUES (2, N'ExpField Classification')
INSERT [dbo].[API_batch_class] ([id_batch_class], [name]) VALUES (3, N'Classifier DocumentAI')
INSERT [dbo].[API_batch_class] ([id_batch_class], [name]) VALUES (4, N'Extractor DocumentAI')
INSERT [dbo].[API_batch_class] ([id_batch_class], [name]) VALUES (5, N'Extractor Signature')
INSERT [dbo].[API_batch_class] ([id_batch_class], [name]) VALUES (6, N'Classifier Gemini')
INSERT [dbo].[API_batch_class] ([id_batch_class], [name]) VALUES (7, N'Extractor Gemini')
SET IDENTITY_INSERT [dbo].[API_batch_class] OFF
GO
SET IDENTITY_INSERT [dbo].[API_batch_field_def] ON 

INSERT [dbo].[API_batch_field_def] ([id_batch_field_def], [id_batch_class], [field_name], [id_field_type]) VALUES (1, 1, N'NAME_IN', 1)
INSERT [dbo].[API_batch_field_def] ([id_batch_field_def], [id_batch_class], [field_name], [id_field_type]) VALUES (2, 1, N'SURNAME_IN', 1)
INSERT [dbo].[API_batch_field_def] ([id_batch_field_def], [id_batch_class], [field_name], [id_field_type]) VALUES (3, 1, N'CF_IN', 1)
INSERT [dbo].[API_batch_field_def] ([id_batch_field_def], [id_batch_class], [field_name], [id_field_type]) VALUES (4, 1, N'NAME_OUT', 1)
INSERT [dbo].[API_batch_field_def] ([id_batch_field_def], [id_batch_class], [field_name], [id_field_type]) VALUES (5, 1, N'SURNAME_OUT', 1)
INSERT [dbo].[API_batch_field_def] ([id_batch_field_def], [id_batch_class], [field_name], [id_field_type]) VALUES (6, 1, N'EXP_DATE_OUT', 8)
SET IDENTITY_INSERT [dbo].[API_batch_field_def] OFF
GO
SET IDENTITY_INSERT [dbo].[API_document_class] ON 

INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (1, 1, N'CIE')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (2, 1, N'CIC')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (3, 1, N'PAT')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (4, 1, N'PAS')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (5, 2, N'554')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (7, 2, N'4')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (11, 2, N'1010')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (12, 2, N'272')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (15, 2, N'13')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (30, 2, N'921')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (40, 2, N'783')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (58, 2, N'574')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (59, 4, N'793')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (72, 2, N'646')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (80, 5, N'4')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (85, 6, N'272')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (88, 6, N'4')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (106, 6, N'554')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (117, 6, N'783')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (128, 6, N'921')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (131, 6, N'574')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (132, 6, N'13')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (135, 6, N'1010')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (137, 6, N'11')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (138, 6, N'801')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (139, 6, N'804')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (141, 7, N'4')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (142, 7, N'13')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (153, 7, N'272')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (172, 7, N'554')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (175, 7, N'574')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (185, 7, N'646')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (196, 7, N'783')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (205, 7, N'921')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (207, 7, N'1010')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (214, 3, N'272')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (215, 4, N'272')
INSERT [dbo].[API_document_class] ([id_document_class], [id_batch_class], [name]) VALUES (216, 2, N'793')
SET IDENTITY_INSERT [dbo].[API_document_class] OFF
GO
SET IDENTITY_INSERT [dbo].[API_field_type] ON 

INSERT [dbo].[API_field_type] ([id_field_type], [type_name]) VALUES (1, N'STRING')
INSERT [dbo].[API_field_type] ([id_field_type], [type_name]) VALUES (2, N'INTEGER')
INSERT [dbo].[API_field_type] ([id_field_type], [type_name]) VALUES (3, N'DATE')
INSERT [dbo].[API_field_type] ([id_field_type], [type_name]) VALUES (4, N'ARRAY')
INSERT [dbo].[API_field_type] ([id_field_type], [type_name]) VALUES (5, N'SMALLINT')
INSERT [dbo].[API_field_type] ([id_field_type], [type_name]) VALUES (6, N'BOOLEAN')
INSERT [dbo].[API_field_type] ([id_field_type], [type_name]) VALUES (7, N'BIGINT')
INSERT [dbo].[API_field_type] ([id_field_type], [type_name]) VALUES (8, N'DATETIME')
INSERT [dbo].[API_field_type] ([id_field_type], [type_name]) VALUES (9, N'DECIMAL')
SET IDENTITY_INSERT [dbo].[API_field_type] OFF
GO
SET IDENTITY_INSERT [dbo].[API_page_type] ON 

INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (1, 2, N'CDI Esterno')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (2, 2, N'CDI Interno')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (3, 1, N'CIE Fronte')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (4, 1, N'CIE Retro')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (5, 3, N'PAT Fronte')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (6, 3, N'PAT Retro')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (7, 4, N'PAS Pagina 1')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (8, 4, N'PAS Pagina 3')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (9, 5, N'CDI Esterno')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (10, 5, N'CDI Interno')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (11, 5, N'CIE Fronte')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (12, 5, N'CIE Retro')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (13, 5, N'PAT Fronte')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (14, 5, N'PAT Retro')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (15, 5, N'PAS Pagina 1')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (16, 5, N'PAS Pagina 3')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (18, 7, N'4 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (22, 11, N'1010 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (23, 12, N'272 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (26, 15, N'13 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (41, 30, N'921 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (51, 40, N'783 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (69, 58, N'574 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (70, 12, N'272 Revocati')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (71, 59, N'793 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (87, 72, N'646 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (95, 80, N'4 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (99, 85, N'272 Revocati')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (100, 85, N'272 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (103, 88, N'4 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (121, 106, N'CIE Retro')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (122, 106, N'PAT Fronte')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (123, 106, N'CDI Interno')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (124, 106, N'PAS Pagina 3')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (125, 106, N'CDI Esterno')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (126, 106, N'PAS Pagina 1')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (127, 106, N'PAT Retro')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (128, 106, N'CIE Fronte')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (139, 117, N'783 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (150, 128, N'921 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (153, 131, N'574 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (154, 132, N'13 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (157, 135, N'1010 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (159, 137, N'11 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (160, 138, N'801 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (161, 139, N'804 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (163, 141, N'4 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (164, 142, N'13 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (175, 153, N'272 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (194, 172, N'554 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (197, 175, N'574 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (207, 185, N'646 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (218, 196, N'783 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (227, 205, N'921 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (229, 207, N'1010 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (236, 214, N'Pagina_272')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (237, 215, N'272 Pagina')
INSERT [dbo].[API_page_type] ([id_page_type], [id_document_class], [name]) VALUES (238, 216, N'793 Pagina')
SET IDENTITY_INSERT [dbo].[API_page_type] OFF
GO
SET IDENTITY_INSERT [dbo].[BatchClasses] ON 

INSERT [dbo].[BatchClasses] ([BatchClassId], [Name]) VALUES (1, N'ALLIANZ')
SET IDENTITY_INSERT [dbo].[BatchClasses] OFF
GO

SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Submissi__27CC2CAA2E291F11]    Script Date: 5/22/2025 3:33:03 AM ******/
ALTER TABLE [dbo].[Submissions] ADD UNIQUE NONCLUSTERED 
(
	[RequestGuid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Documents] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Fields] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Groups] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Submissions] ADD  DEFAULT (getdate()) FOR [SubmittedAt]
GO
ALTER TABLE [dbo].[Submissions] ADD  DEFAULT ((0)) FOR [Checked_GUID]
GO
ALTER TABLE [dbo].[API_batch_field_def]  WITH CHECK ADD FOREIGN KEY([id_batch_class])
REFERENCES [dbo].[API_batch_class] ([id_batch_class])
GO
ALTER TABLE [dbo].[API_batch_field_def]  WITH CHECK ADD  CONSTRAINT [FK_batch_field_def_field_type] FOREIGN KEY([id_field_type])
REFERENCES [dbo].[API_field_type] ([id_field_type])
GO
ALTER TABLE [dbo].[API_batch_field_def] CHECK CONSTRAINT [FK_batch_field_def_field_type]
GO
ALTER TABLE [dbo].[API_document_class]  WITH CHECK ADD FOREIGN KEY([id_batch_class])
REFERENCES [dbo].[API_batch_class] ([id_batch_class])
GO
ALTER TABLE [dbo].[API_page_type]  WITH CHECK ADD FOREIGN KEY([id_document_class])
REFERENCES [dbo].[API_document_class] ([id_document_class])
GO
ALTER TABLE [dbo].[Batches]  WITH CHECK ADD FOREIGN KEY([BatchClassId])
REFERENCES [dbo].[BatchClasses] ([BatchClassId])
GO
ALTER TABLE [dbo].[BatchFields]  WITH CHECK ADD FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batches] ([BatchId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BatchStates]  WITH CHECK ADD FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batches] ([BatchId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DocumentFields]  WITH CHECK ADD FOREIGN KEY([VerificationDocumentId])
REFERENCES [dbo].[VerificationDocuments] ([VerificationDocumentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Documents]  WITH CHECK ADD FOREIGN KEY([SubmissionId])
REFERENCES [dbo].[Submissions] ([SubmissionId])
GO
ALTER TABLE [dbo].[Fields]  WITH CHECK ADD FOREIGN KEY([SubmissionId])
REFERENCES [dbo].[Submissions] ([SubmissionId])
GO
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD FOREIGN KEY([VerificationDocumentId])
REFERENCES [dbo].[VerificationDocuments] ([VerificationDocumentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PageTypes]  WITH CHECK ADD FOREIGN KEY([PageId])
REFERENCES [dbo].[Pages] ([PageId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Signatures]  WITH CHECK ADD FOREIGN KEY([VerificationDocumentId])
REFERENCES [dbo].[VerificationDocuments] ([VerificationDocumentId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Submissions]  WITH CHECK ADD FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupId])
GO
ALTER TABLE [dbo].[VerificationDocuments]  WITH CHECK ADD FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batches] ([BatchId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VerificationDocuments]  WITH CHECK ADD FOREIGN KEY([DocumentClassId])
REFERENCES [dbo].[VerificationDocumentClasses] ([DocumentClassId])
GO
ALTER TABLE [dbo].[VerificationResponses]  WITH CHECK ADD FOREIGN KEY([BatchId])
REFERENCES [dbo].[Batches] ([BatchId])
GO
