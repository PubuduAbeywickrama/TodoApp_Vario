create table [dbo].[todo_task] 
(
	id bigint identity(1,1),
	task varchar(max),
	description varchar(max),
	createDate date,
	dueDate date,
	isComplete bit,
	userId varchar(max)
)