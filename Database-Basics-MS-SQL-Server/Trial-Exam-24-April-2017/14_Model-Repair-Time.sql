SELECT 
	m.ModelId, 
	m.Name, 
	CONCAT(AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate)), ' ', 'days') AS [Average Service Time]
FROM Models AS m
JOIN Jobs AS j ON j.ModelId = m.ModelId
GROUP BY m.Name, m.ModelId
ORDER BY [Average Service Time]