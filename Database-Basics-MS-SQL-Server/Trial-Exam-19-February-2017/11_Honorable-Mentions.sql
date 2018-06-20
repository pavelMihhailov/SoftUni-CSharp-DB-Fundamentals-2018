SELECT 
	f.ProductId, 
	CONCAT(c.FirstName, ' ', c.LastName) AS CustomerName, 
	f.Description AS FeedbackDescription 
	FROM Feedbacks AS f
INNER JOIN Customers AS c ON c.Id = f.CustomerId
WHERE c.Id IN (
				SELECT 
				c.Id 
				FROM Feedbacks AS f
				INNER JOIN Customers AS c ON c.Id = f.CustomerId
				GROUP BY c.Id
				HAVING COUNT(f.Id) > 3
			  )
ORDER BY f.ProductId, CustomerName, f.Id