CREATE FUNCTION udf_GetRating(@Name NVARCHAR(50)) RETURNS NVARCHAR(20)
AS
BEGIN
	DECLARE @rating NVARCHAR(20)
	SET @rating = (
		SELECT CASE
			WHEN AVG(f.Rate) IS NULL THEN 'No rating'
			WHEN AVG(f.Rate) < 5 THEN 'Bad'
			WHEN AVG(f.Rate) BETWEEN 5 AND 8 THEN 'Average'
			WHEN AVG(f.Rate) > 8 THEN 'Good'
			END
		FROM Feedbacks AS f
		JOIN Products AS p ON p.Id = f.ProductId
		WHERE p.Name = @Name
	)
	RETURN @rating
END