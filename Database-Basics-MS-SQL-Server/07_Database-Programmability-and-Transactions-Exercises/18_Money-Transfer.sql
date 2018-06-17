CREATE PROC usp_TransferMoney(@SenderId INT, @RecieverId INT, @Amount DECIMAL(15, 4))
AS
BEGIN
	BEGIN TRANSACTION
		IF(@Amount < 0)
		BEGIN
			RAISERROR('Amount is negative!', 16, 1)
			RETURN
		END

		UPDATE Accounts
		SET Balance -= @Amount 
		WHERE Id = @SenderId

		IF(@@ROWCOUNT <> 1)
		BEGIN
			RAISERROR('Invalid Sender id!', 16, 2)
			ROLLBACK
			RETURN
		END

		DECLARE @FinalAmount DECIMAL(15, 4)
		SET @FinalAmount = (SELECT Balance FROM Accounts WHERE Id = @SenderId)

		IF(@FinalAmount < 0)
		BEGIN
			RAISERROR('Insufficient funds!', 16, 3)
			ROLLBACK
			RETURN
		END

		UPDATE Accounts
		SET Balance += @Amount
		WHERE Id = @ReceiverId

		IF(@@ROWCOUNT <> 1)
		BEGIN
			RAISERROR('Invalid Receiver id!', 16, 4)
			ROLLBACK
			RETURN
		END

	COMMIT
END