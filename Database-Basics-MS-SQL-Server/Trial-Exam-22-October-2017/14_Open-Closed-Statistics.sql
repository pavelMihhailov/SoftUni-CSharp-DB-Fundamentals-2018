SELECT 
	CONCAT(e.FirstName, ' ', e.LastName) AS [Name], 
	CONCAT(COUNT(r.CloseDate), '/', COUNT(r.OpenDate)) AS [Closed Open Reports]
    FROM Employees AS e
JOIN Reports AS r ON r.EmployeeId = e.Id
WHERE
YEAR(r.OpenDate) = 2016 OR YEAR(r.CloseDate) = 2016
GROUP BY CONCAT(e.FirstName, ' ', e.LastName), e.Id
ORDER BY Name, e.Id