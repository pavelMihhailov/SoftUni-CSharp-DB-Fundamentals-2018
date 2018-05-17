CREATE TABLE Users(
	Id BIGINT IDENTITY,
	Username NVARCHAR(30) UNIQUE NOT NULL,
	[Password] NVARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(900),
	LastLoginTime TIME,
	IsDeleted BIT DEFAULT 0,
	CONSTRAINT PK_Id PRIMARY KEY(Id)
)

INSERT INTO Users (Username, [Password], ProfilePicture, LastLoginTime, IsDeleted) VALUES
('Pavel', '123456', NULL, '2016/01/03 00:10:23', 0),
('Ivan', '1234523', NULL, '2012/01/03 00:10:23', 0),
('Stefan', '123456789', NULL, '2014/02/23 00:10:23', 0),
('Iordan', '1234563', NULL, '2018/09/03 00:10:23', 0),
('Nikolai', '123456333', NULL, '2013/03/03 00:10:23', 1)