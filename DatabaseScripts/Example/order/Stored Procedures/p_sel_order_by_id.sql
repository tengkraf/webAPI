Create PROCEDURE [dbo].[p_sel_order_by_id]
(
	@orderId int
)
AS
BEGIN

SELECT [order].[order].[order_id]
      ,[customer_id]
	  ,[ordered_item_id]
      ,[item_id]
      ,[item_cnt]
	  ,[order].shipping_address.[shipping_address_id]
      ,[addr1_text]
      ,[addr2_text]
      ,[city_name]
      ,[state_code]
      ,[zip_code]
      ,[country_code]
  FROM [order].[order]
  join [order].shipping_address on [order].[order].shipping_address_id = [order].shipping_address.shipping_address_id
  join [order].ordered_item on [order].[order].order_id = [order].ordered_item.order_id
  where [order].[order].order_id = @orderId

END