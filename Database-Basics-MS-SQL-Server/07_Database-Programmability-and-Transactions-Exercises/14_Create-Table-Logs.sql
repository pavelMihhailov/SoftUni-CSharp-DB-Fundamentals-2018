CREATE TABLE Logs(
	LogId INT PRIMARY KEY IDENTITY,
	AccountId INT FOREIGN KEY REFERENCES Accounts(Id) NOT NULL,
	OldSum DECIMAL(15, 2) NOT NULL,
	NewSum DECIMAL(15, 2) NOT NULL
)
GO

CREATE TRIGGER tr_AccountsUpdate ON Accounts AFTER UPDATE
AS
BEGIN
	INSERT INTO Logs (AccountId, OldSum, NewSum)
	SELECT i.Id, deleted.Balance, i.Balance FROM inserted AS i
	JOIN deleted ON deleted.Id = i.Id
END