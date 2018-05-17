CREATE TABLE People(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX),
	Height DECIMAL(4, 2),
	[Weight] DECIMAL(4, 2),
	Gender CHAR(1) NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX)
)

INSERT INTO People ([Name], Picture, Height, [Weight], Gender, Birthdate, Biography) VALUES
('Pavel', NULL, 1.90, 79.00, 'm', '1998/09/09', 'I am from Sofia'),
('Maria', NULL, 1.65, 50.33, 'f', '1993/02/28', 'I am from Varna'),
('Ivan', NULL, 2.00, 93.00, 'm', '2003/09/12', 'I am from Plovdiv'),
('Stefan', NULL, 2.10, 87.39, 'm', '1998/02/01', 'I am from Pleven'),
('Ioanna', NULL, 1.60, 48.00, 'f', '1999/07/09', 'I am from Sofia')