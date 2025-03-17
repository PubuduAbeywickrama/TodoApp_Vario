create table [dbo].[users] 
(
	id bigint identity(1,1),
	userId varchar(50),
	userName varchar(max)
)