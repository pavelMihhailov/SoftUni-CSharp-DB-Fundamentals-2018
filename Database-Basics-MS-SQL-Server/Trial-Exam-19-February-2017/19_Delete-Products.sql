CREATE TRIGGER tr_Delete ON Products INSTEAD OF DELETE
AS
BEGIN
	DECLARE @productId INT
	SET @productId = (
		SELECT d.Id FROM deleted AS d
	)
	DELETE FROM Feedbacks WHERE ProductId = @productId
	DELETE FROM ProductsIngredients WHERE ProductId = @productId
	DELETE FROM Products WHERE Id = @productId
END