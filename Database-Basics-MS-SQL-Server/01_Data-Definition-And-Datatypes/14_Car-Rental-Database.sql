CREATE DATABASE CarRental

CREATE TABLE Categories (
	Id INT IDENTITY PRIMARY KEY,
	CategoryName NVARCHAR(50) NOT NULL,
	DailyRate DECIMAL(15, 2),
	WeeklyRate DECIMAL(15, 2),
	MonthlyRate DECIMAL(15, 2),
	WeekendRate DECIMAL(15, 2)
)

CREATE TABLE Cars (
	Id INT IDENTITY PRIMARY KEY,
	PlateNumber NVARCHAR(20) NOT NULL,
	Manufacturer NVARCHAR(20) NOT NULL,
	Model NVARCHAR(20) NOT NULL,
	CarYear INT NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Doors INT NOT NULL,
	Picture VARBINARY(MAX),
	Condition NVARCHAR(50),
	Available BIT NOT NULL
)

CREATE TABLE Employees (
	Id INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(20) NOT NULL,
	LastName NVARCHAR(20) NOT NULL,
	Title NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE Customers (
	Id INT IDENTITY PRIMARY KEY,
	DriverLicenseNumber NVARCHAR(50) NOT NULL,
	FullName NVARCHAR(50) NOT NULL,
	[Address] NVARCHAR(50) NOT NULL,
	City NVARCHAR(50) NOT NULL,
	ZIPCode NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RentalOrders (
	Id INT IDENTITY PRIMARY KEY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id) NOT NULL,
	CarId INT FOREIGN KEY REFERENCES Cars(Id) NOT NULL,
	TankLevel FLOAT NOT NULL,
	KilometrageStart FLOAT NOT NULL,
	KilometrageEnd FLOAT NOT NULL,
	TotalKilometrage FLOAT NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	TotalDays INT NOT NULL,
	RateApplied DECIMAL(15, 2) NOT NULL,
	TaxRate DECIMAL(15, 2) NOT NULL,
	OrderStatus NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
)

INSERT INTO Categories (CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate)
VALUES
	('Economy', 30, 44, 1020, 60),
	('Standard', 50, 500, 2000, 200),
	('Premium', 100, 800, NULL, NULL)

INSERT INTO Cars (PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
VALUES
	('CA12345XB', 'Sedan', 'Renault', 2016, 1, 4, 0101010, 'Excellent', 1),
	('TT4444TT', 'SUV', 'VW', 2016, 1, 4, 111000, 'Outstanding', 1),
	('VV5555HH', 'Sedan', 'Mercedes', 2016, 3, 4, 01111, 'Excellent', 0)

INSERT INTO Employees (FirstName, LastName, Title)
VALUES
	('Tom', 'Barnes', 'Sales Representative'),
	('David', 'Jones', 'CEO'),
	('Eva', 'Michado', 'Software developer')

INSERT INTO Customers (DriverLicenseNumber, FullName, Address, City, ZIPCode, Notes)
VALUES
	('A111111', 'Angela MErkel', 'Willy-Brandt-Strasse 1', 'Berlin', '10557', 'New leader of the free world'),
	('B222222', 'Barack Obama', '1600 Pennsylvania Ave NW', 'Washington', 'DC 20500', 'Previous leader of the free world'),
	('C333333', 'Bill Clinton', '555 Bloomberg Avenue', 'New York', 'NY 1000', NULL)

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes)
VALUES
	(1, 1, 1, 100, 30100, 30200.5, 100.5, '2017-01-22', '2017-01-22', 1, 15, 0.20, 'Rented', NULL),
	(2, 2, 2, 100, 30100, 30250.5, 150.5, '2017-01-20', '2017-01-22', 3, 80, 0.20, 'Pending', 'TBD'),
	(3, 3, 3, 100, 30000, 30200.5, 200.5, '2017-01-21', '2017-01-22', 2, 110, 0.20, 'Closed', NULL)