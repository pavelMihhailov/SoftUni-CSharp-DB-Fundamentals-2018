WITH cte
AS (
	SELECT 
		d.Id, 
		COUNT(r.Id) AS total 
	FROM Departments AS d
	INNER JOIN Categories AS c ON c.DepartmentId = d.Id
	INNER JOIN Reports AS r ON r.CategoryId = c.Id
	GROUP BY d.Id)


SELECT 
	d.Name AS [Department Name], 
	c.Name AS [Category Name], 
	ROUND((CAST(COUNT(r.Id) AS FLOAT) * 100 / cte.total),0)  AS Percentage
FROM cte 
	INNER JOIN Departments AS d ON d.Id = cte.Id
	INNER JOIN Categories AS c ON c.DepartmentId = d.Id
	INNER JOIN Reports AS r ON r.CategoryId = c.Id
GROUP BY d.Name, c.Name, cte.total
ORDER BY d.Name