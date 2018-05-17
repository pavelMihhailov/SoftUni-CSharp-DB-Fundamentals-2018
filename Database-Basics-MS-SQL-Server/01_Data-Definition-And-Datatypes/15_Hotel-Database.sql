CREATE DATABASE Hotel

CREATE TABLE Employees (
	Id INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Title NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Customers (
	AccountNumber INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	PhoneNumber NVARCHAR(20) NOT NULL,
	EmergencyName NVARCHAR(50),
	EmergencyNumber NVARCHAR(20),
	Notes NVARCHAR(MAX)
)

CREATE TABLE RoomStatus (
	RoomStatus NVARCHAR(50) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RoomTypes (
	RoomType NVARCHAR(50) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE BedTypes (
	BedType NVARCHAR(50) PRIMARY KEY NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Rooms (
	RoomNumber INT PRIMARY KEY NOT NULL,
	RoomType NVARCHAR(50) FOREIGN KEY REFERENCES RoomTypes(RoomType) NOT NULL,
	BedType NVARCHAR(50) FOREIGN KEY REFERENCES BedTypes(BedType) NOT NULL,
	Rate DECIMAL(15, 2) NOT NULL,
	RoomStatus NVARCHAR(50) FOREIGN KEY REFERENCES RoomStatus(RoomStatus) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Payments (
	Id INT IDENTITY PRIMARY KEY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	PaymentDate DATE NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL,
	FirstDateOccupied DATE NOT NULL,
	LastDateOccupied DATE NOT NULL,
	TotalDays INT NOT NULL,
	AmountCharged DECIMAL(15, 2) NOT NULL,
	TaxRate DECIMAL(15, 2) NOT NULL,
	TaxAmount DECIMAL(15, 2) NOT NULL,
	PaymentTotal DECIMAL(15, 2) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Occupancies (
	Id INT IDENTITY PRIMARY KEY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	DateOccupied DATE NOT NULL,
	AccountNumber INT FOREIGN KEY REFERENCES Customers(AccountNumber) NOT NULL,
	RoomNumber INT FOREIGN KEY REFERENCES Rooms(RoomNumber) NOT NULL,
	RateApplied DECIMAL(15, 2) NOT NULL,
	PhoneCharge DECIMAL(15, 2) NOT NULL DEFAULT 0, 
	Notes NVARCHAR(MAX)
)

INSERT INTO Employees (FirstName, LastName, Title, Notes)
VALUES
	('Tom', 'Barnes', 'Hotel Manager', NULL),
	('David', 'Jones', 'CEO', NULL),
	('Eva', 'Michado', 'Chambermaid', 'Late for work')

INSERT INTO Customers (FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber)
VALUES
	('Angela','Merkel', 49123456789, 'Barroso', 32987654321),
	('Barack','Obama', 1123456789, NULL, NULL),
	('Margaret','Thacher', 41987654321, NULL, NULL)

INSERT INTO RoomStatus (RoomStatus)
VALUES
	('Reserved'), ('Occupied'), ('Available')

INSERT INTO RoomTypes (RoomType)
VALUES
	('Single'), ('Double'), ('Suite')

INSERT INTO BedTypes (BedType)
VALUES
	('Single'), ('Twin'), ('Double')

INSERT INTO Rooms (RoomNumber, RoomType, BedType, Rate, RoomStatus)
VALUES
	(1, 'Single', 'Single', 70, 'Reserved'),
	(2, 'Double', 'Twin', 100, 'Occupied'),
	(3, 'Suite', 'Double', 110, 'Available')

INSERT INTO Payments (EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, TotalDays, AmountCharged, TaxRate, TaxAmount, PaymentTotal)
VALUES
	(1, '2017-01-22', 1, '2017-01-21', '2017-01-22', 1, 100, 0.20, 20, 120),
	(1, '2017-01-22', 2, '2017-01-20', '2017-01-22', 2, 200, 0.20, 40, 240),
	(1, '2017-01-22', 3, '2017-01-19', '2017-01-22', 3, 300, 0.20, 60, 360)

INSERT INTO Occupancies (EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge)
VALUES
	(1, '2014-01-22', 1, 1, 70, 0),
	(2, '2014-01-22', 2, 2, 100, 0),
	(3, '2014-01-22', 3, 3, 110, 10)