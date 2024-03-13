create database DBStackOverflow
use DBStackOverflow

create table Category(
	IdCategory int identity primary key,
	[Name] varchar(50) not null,
)

create table Post(
	IdPost int identity primary key,
	Title varchar(250) not null,
	[Description] varchar(Max) not null,
	Creation_Date date not null,
	UpVote int not null,
	DownVote int not null,
	IdCategory int not null, 
	Foreign Key (IdCategory) references Category(IdCategory)
)

/*stored procedure*/

--
Create Procedure sp_List
	as
	begin 
		select * from Post;
	end;
--
create Procedure sp_List_Category (@id int)
	as
	begin
		select * from Post where IdCategory = @id;
	end
--
Create Procedure sp_Get(@id int)
	as
	begin
		select * from Post where IdPost = @id;
	end
--
CREATE PROCEDURE sp_Insert
    @Title NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @Creation_Date DATE,
    @IdCategory INT
AS
BEGIN
    INSERT INTO Post (Title, [Description], Creation_Date, UpVote, DownVote, IdCategory)
    VALUES (@Title, @Description, @Creation_Date, 0, 0, @IdCategory);
END;
--
CREATE PROCEDURE sp_Update
    @IdPost int,
	@Title NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @Creation_Date DATE,
    @IdCategory INT
AS
BEGIN
    Update Post set Title = @Title, [Description] = @Description, Creation_Date = @Creation_Date, IdCategory = @IdCategory where IdPost = @IdPost;
END;
--
Create Procedure sp_Delete(@Id int)
	as
	begin 
		Delete Post where IdPost = @Id;
	end
--
Create Procedure sp_VoteUp(@IdPost int, @VotingStatus int)
	as
	begin
		Update Post set UpVote = @VotingStatus where IdPost = @IdPost;
	end;
--
Create Procedure sp_VoteDown(@IdPost int, @VotingStatus int)
	as
	begin
		Update Post set UpVote = @VotingStatus where IdPost = @IdPost;
	end;
--
CREATE PROCEDURE sp_Insert_Category (@Title NVARCHAR(255))
	AS
	BEGIN
		INSERT INTO Category([Name])
		VALUES (@Title);
	END;
--
Create Procedure sp_Delete_Category(@Id int)
	as
	begin 
		Delete Category where IdCategory = @Id;
	end
--
Create Procedure sp_List_Categorys
	as
	begin 
		select * from Category;
	end;

-- Estableciendo categorias
insert Into Category values ('Java')
insert Into Category values ('C#')
insert Into Category values ('Algorithms')
insert Into Category values ('Java Script')
insert Into Category values ('Python')

select * from Category