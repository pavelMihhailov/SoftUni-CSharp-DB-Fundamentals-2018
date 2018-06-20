SELECT 
	CountryName,
	DistributorName
	FROM (
			SELECT
			  c.Name AS CountryName,
			  d.Name AS DistributorName,
			  COUNT(i.DistributorId) AS IngredientsByDistributor,
			  DENSE_RANK()
			  OVER ( PARTITION BY c.Name
			    ORDER BY COUNT(i.DistributorId) DESC ) AS Rank
			FROM Countries AS c
			  LEFT OUTER JOIN Distributors AS d ON d.CountryId = c.Id
			  LEFT OUTER JOIN Ingredients AS i ON i.DistributorId = d.Id
			GROUP BY c.Name,
			  d.Name
) AS ranked
WHERE Rank = 1
ORDER BY CountryName, DistributorName