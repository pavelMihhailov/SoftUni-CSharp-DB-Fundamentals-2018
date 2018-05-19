CREATE TABLE People(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
	Birthdate DATE NOT NULL
)

INSERT INTO People ([Name], Birthdate) VALUES
('Viktor', '2000-12-07 00:00:00.000'),
('Steven', '1992-09-10 00:00:00.000'),
('Stephen', '1910-09-19 00:00:00.000'),
('John', '2010-01-06 00:00:00.000')

SELECT [Name], 
	   DATEDIFF(YY, Birthdate, GETDATE()) AS [Age in Years],
	   DATEDIFF(MM, Birthdate, GETDATE()) AS [Age in Months],
	   DATEDIFF(DD, Birthdate, GETDATE()) AS [Age in Days],
	   DATEDIFF(MINUTE, Birthdate, GETDATE()) AS [Age in Years]
FROM People