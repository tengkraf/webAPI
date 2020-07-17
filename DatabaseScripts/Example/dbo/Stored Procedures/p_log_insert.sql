create PROCEDURE [dbo].[p_log_insert]
(
	@Application nvarchar(100),
	@UserName nvarchar(50),      
	@TimeStamp datetime ,
	@Level nvarchar(50) ,
	@MessageType nvarchar(100),
	@RequestID nvarchar(50),	
	@Message nvarchar(max),
	@Url nvarchar(max),
	@Exception nvarchar(max) 
)
AS
insert into [dbo].[log] 
(
    [application_name], 
	[log_ts], 
	[log_lvl_code], 
	[log_msg_text],
    [login_id],
	[log_msg_type_code], 
	[log_url],
    [log_reqst_id], 
	[log_excptn_msg_text]
    ) values (
    @Application, 
	@TimeStamp, 
	@Level, 
	@Message,
	NULLIF(@UserName, ''),
	NULLIF(@MessageType, ''), 
	NULLIF(@Url, ''), 
	NULLIF(@RequestID, ''),
    NULLIF(@Exception, '')
);