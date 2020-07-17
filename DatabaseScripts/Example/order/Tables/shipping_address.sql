CREATE TABLE [order].[shipping_address] (
    [shipping_address_id]	INT IDENTITY (1, 1) NOT NULL,
	[addr1_text]	VARCHAR (150) NULL,
	[addr2_text]	VARCHAR (150) NULL,
	[city_name]		VARCHAR (100) NULL,
	[state_code]	CHAR (2) NULL,
	[zip_code]		VARCHAR (10) NULL,
	[country_code]	CHAR (2) NULL,
    [create_logon_id]		VARCHAR (100) NOT NULL,
    [create_date]			DATETIME   NOT NULL,
    [last_update_logon_id]	VARCHAR (100)  NULL,
	[last_update_date]		DATETIME     NULL,
	CONSTRAINT [PK_order.shipping_address] PRIMARY KEY CLUSTERED ([shipping_address_id] ASC)
)
go