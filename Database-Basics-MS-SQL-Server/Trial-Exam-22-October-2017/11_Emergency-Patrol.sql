SELECT 
	r.OpenDate, 
	r.Description, 
	u.Email AS [Reporter Email] 
	FROM Reports AS r
INNER JOIN Categories AS c ON c.Id = r.CategoryId
INNER JOIN Users AS u ON u.Id = r.UserId
WHERE 
r.CloseDate IS NULL
AND LEN(r.Description) > 20 
AND r.Description LIKE '%str%'
AND c.DepartmentId IN (1, 4, 5)
ORDER BY r.OpenDate, u.Email, u.Id