CREATE TABLE NotificationEmails(
	Id INT IDENTITY PRIMARY KEY,
	Recipient INT FOREIGN KEY REFERENCES Accounts(Id) NOT NULL,
	[Subject] NVARCHAR(MAX) NOT NULL,
	Body NVARCHAR(MAX) NOT NULL
)

GO

CREATE TRIGGER tr_UpdateNotificationEmails ON Logs AFTER INSERT
AS
BEGIN
	INSERT INTO NotificationEmails(Recipient, [Subject], Body)
	SELECT LogId, 
		   CONCAT('Balance change for account: ', AccountId),
		   CONCAT('On ', GETDATE(), ' your balance was changed from ', OldSum,' to ', NewSum, '.')
		   FROM inserted
END