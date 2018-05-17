CREATE DATABASE Movies

USE Movies

CREATE TABLE Directors(
	Id INT PRIMARY KEY IDENTITY,
	DirectorName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Genres(
	Id INT PRIMARY KEY IDENTITY,
	GenreName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Movies(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(50) NOT NULL,
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id) NOT NULL,
	CopyrightYear INT NOT NULL,
	[Length] INT NOT NULL,
	GenreId INT FOREIGN KEY REFERENCES Genres(Id) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Rating DECIMAL(2, 1),
	Notes NVARCHAR(MAX)
)

INSERT INTO Directors (DirectorName, Notes) VALUES
	('Justin Lin', 'Don`t have notes'),
	('Christopher Nolan', 'Best known for his cerebral, often nonlinear story-telling'),
	('Susanne Bier', 'Known for In a Better World (2010), After the Wedding (2006) and Brothers (2004).'),
	('Kathryn Bigelow', 'Director of The Hurt Locker'),
	('Ridley Scott', 'His reputation remains solidly intact.')

INSERT INTO Genres (GenreName) VALUES
	('Drama'),('History'),('Comedy'),('Romance'),('Action')

INSERT INTO Categories (CategoryName) VALUES
	('C1'),('C2'),('C3'),('C4'),('C5')

INSERT INTO Movies (Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes) VALUES
	('Gladiator', 5, 2000, 155, 1, 1, 8.5, NULL),
	('The Prestige', 2, 2006, 130, 5, 2, 8.5, 'One of my favourite movies'),
	('The Hurt Locker', 4, 2008, 131, 3, 1, 7.6, NULL),
	('After the Wedding', 3, 2006, 155, 1, 1, 7.8, 'Amazing performance from everyone'),
	('Placebo', 1, 2008, 120, 1, 1, 7.4, NULL)