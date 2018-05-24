CREATE TABLE ItemTypes (
	ItemTypeID INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Items (
	ItemID INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	ItemTypeID INT FOREIGN KEY REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE Cities (
	CityID INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Customers (
	CustomerID INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(50) NOT NULL,
	Birthday DATE NOT NULL,
	CityID INT FOREIGN KEY REFERENCES Cities(CityID)
)

CREATE TABLE Orders (
	OrderID INT IDENTITY PRIMARY KEY,
	CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID)
)

CREATE TABLE OrderItems (
	OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
	ItemID INT FOREIGN KEY REFERENCES Items(ItemID),
	CONSTRAINT PK_OrderIdItemId PRIMARY KEY(OrderID, ItemID)
)