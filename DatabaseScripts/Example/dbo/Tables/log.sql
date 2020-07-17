CREATE TABLE [dbo].[log] (
    [id]                  INT            IDENTITY (1, 1) NOT NULL,
    [application_name]    NVARCHAR (100) NOT NULL,
    [login_id]            NVARCHAR (50)  NULL,
    [log_ts]              DATETIME       NOT NULL,
    [log_lvl_code]        NVARCHAR (50)  NOT NULL,
    [log_msg_type_code]   NVARCHAR (100) NULL,
    [log_msg_text]        NVARCHAR (MAX) NOT NULL,
    [log_url]             NVARCHAR (MAX) NULL,
    [log_reqst_id]        NVARCHAR (50)  NULL,
    [log_excptn_msg_text] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_training.log] PRIMARY KEY CLUSTERED ([id] ASC) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

