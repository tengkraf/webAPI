CREATE TABLE [order].[ordered_item]
(
	[ordered_item_id]     INT  IDENTITY (1, 1) NOT NULL,
	[order_id]	 int NOT NULL,
	[item_id]	 int NOT NULL,
	[item_cnt]	 int NOT NULL,
    [create_logon_id]		VARCHAR (100) NOT NULL,
    [create_date]			DATETIME   NOT NULL,
    [last_update_logon_id]	VARCHAR (100)  NULL,
	[last_update_date]		DATETIME     NULL,
	CONSTRAINT [PK_order.ordered_item] PRIMARY KEY CLUSTERED ([ordered_item_id] ASC),
	CONSTRAINT [FK_order.ordered_item_order.order_order_id] FOREIGN KEY ([order_id]) REFERENCES [order].[order] ([order_id])
)
go

CREATE NONCLUSTERED INDEX [IX_order_id]
    ON [order].[ordered_item]([order_id] ASC);
GO