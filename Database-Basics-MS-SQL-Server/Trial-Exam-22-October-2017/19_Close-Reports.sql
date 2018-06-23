CREATE TRIGGER tr_UpdateStatus ON Reports AFTER UPDATE
AS
BEGIN
	UPDATE Reports
	SET StatusId = 3
	WHERE Id IN (
		SELECT i.Id FROM inserted AS i
		WHERE i.Id IN (
			SELECT d.Id FROM deleted AS d
			WHERE d.CloseDate IS NULL
		)
		AND i.CloseDate IS NOT NULL
	)
END