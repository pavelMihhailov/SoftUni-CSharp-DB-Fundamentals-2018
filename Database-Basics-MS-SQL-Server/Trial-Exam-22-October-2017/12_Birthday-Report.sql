SELECT 
	c.Name
    FROM Categories AS c
JOIN Reports AS r ON r.CategoryId = c.Id
JOIN Users AS u ON u.Id = r.UserId
WHERE 
MONTH(u.BirthDate) = MONTH(r.OpenDate) AND 
DAY(u.BirthDate) = DAY(r.OpenDate)
GROUP BY c.Name
ORDER BY c.Name