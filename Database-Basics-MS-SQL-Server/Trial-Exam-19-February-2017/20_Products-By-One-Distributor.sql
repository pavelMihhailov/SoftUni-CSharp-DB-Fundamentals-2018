SELECT 
	p.Name AS [ProductName],
	AVG(f.Rate) AS [ProductAverageRate],
	d.Name AS [DistributorName],
	c.Name 
	FROM Products AS p
JOIN ProductsIngredients AS [pi] ON [pi].ProductId = p.Id
JOIN Ingredients AS i ON i.Id = [pi].IngredientId
JOIN Distributors AS d ON d.Id = i.DistributorId
JOIN Feedbacks AS f ON f.ProductId = p.Id
JOIN Countries AS c ON c.Id = d.CountryId
GROUP BY p.Name, d.Name, c.Name, p.Id
HAVING p.Id IN
       (
         SELECT p.Id
         FROM Products AS p
           JOIN ProductsIngredients AS [pi] ON [pi].ProductId = p.Id
           JOIN Ingredients AS i ON i.Id = [pi].IngredientId
         GROUP BY p.Name,
           p.Id
         HAVING COUNT(DISTINCT i.DistributorId) = 1
       )
ORDER BY p.Id