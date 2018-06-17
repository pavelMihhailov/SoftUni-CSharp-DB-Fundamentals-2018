CREATE PROC usp_DepositMoney(@AccountId INT, @MoneyAmount DECIMAL(15, 4))
AS
BEGIN 
	BEGIN TRANSACTION
		IF(@MoneyAmount < 0)
		BEGIN
			ROLLBACK
			RAISERROR('Money cannot be negative', 16, 1)
			RETURN
		END

		UPDATE Accounts
		SET Balance += @MoneyAmount
		WHERE Id = @AccountId

		COMMIT
END