CREATE TABLE [order].[order]
(
	[order_id]     INT  IDENTITY (1, 1) NOT NULL,
	[shipping_address_id]	 int NOT NULL,
	[customer_id]	 int NOT NULL,
    [create_logon_id]		VARCHAR (100) NOT NULL,
    [create_date]			DATETIME   NOT NULL,
    [last_update_logon_id]	VARCHAR (100)  NULL,
	[last_update_date]		DATETIME     NULL,
	CONSTRAINT [PK_order.order] PRIMARY KEY CLUSTERED ([order_id] ASC),
	CONSTRAINT [FK_order.order_order.shipping_address_shipping_address_id] FOREIGN KEY ([shipping_address_id]) REFERENCES [order].[shipping_address] ([shipping_address_id])
)
go

CREATE NONCLUSTERED INDEX [IX_shipping_address_id]
    ON [order].[order]([shipping_address_id] ASC);
GO
