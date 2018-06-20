CREATE TABLE Products (
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(25) UNIQUE,
	[Description] NVARCHAR(250),
	Recipe NVARCHAR(MAX),
	Price MONEY CHECK(Price > 0)
)

CREATE TABLE Countries (
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) UNIQUE
)

CREATE TABLE Customers (
	Id INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(25),
	LastName NVARCHAR(25),
	Gender CHAR(1) CHECK(Gender = 'M' OR Gender = 'F'),
	Age INT,
	PhoneNumber CHAR(10),
	CountryId INT FOREIGN KEY REFERENCES Countries(Id)
)

CREATE TABLE Distributors (
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(25) UNIQUE,
	AddressText NVARCHAR(30),
	Summary NVARCHAR(200),
	CountryId INT FOREIGN KEY REFERENCES Countries(Id)
)

CREATE TABLE Ingredients (
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(30),
	[Description] NVARCHAR(200),
	OriginCountryId INT FOREIGN KEY REFERENCES Countries(Id),
	DistributorId INT FOREIGN KEY REFERENCES Distributors(Id)
)

CREATE TABLE ProductsIngredients (
	ProductId INT,
	IngredientId INT
	CONSTRAINT PK_ProductsIngredients PRIMARY KEY(ProductId, IngredientId),
	CONSTRAINT FK_ProductsIngredients_Products FOREIGN KEY (ProductId) REFERENCES Products (Id),
	CONSTRAINT FK_ProductsIngredients_Ingredients FOREIGN KEY (IngredientId) REFERENCES Ingredients (Id)
)

CREATE TABLE Feedbacks (
	Id INT IDENTITY PRIMARY KEY,
	[Description] NVARCHAR(255),
	Rate DECIMAL(4, 2),
	ProductId INT FOREIGN KEY REFERENCES Products(Id),
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id)
)