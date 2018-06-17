CREATE PROC usp_WithdrawMoney(@AccountId INT, @MoneyAmount DECIMAL(15, 4))
AS
BEGIN
	BEGIN TRANSACTION

	IF(@MoneyAmount < 0)
	BEGIN
		RAISERROR('Money cannot be negative', 16, 2)
		ROLLBACK
	END

	UPDATE Accounts
	SET Balance -= @MoneyAmount
	WHERE Id = @AccountId

	COMMIT
END