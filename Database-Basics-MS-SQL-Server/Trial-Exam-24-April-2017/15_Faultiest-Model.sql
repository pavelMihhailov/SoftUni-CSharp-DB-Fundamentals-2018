SELECT TOP 1 
	m.Name,
	COUNT(DISTINCT j.JobId) AS [Time Serviced],
	ISNULL(SUM(p.Price * op.Quantity), 0) AS [Parts Total]
FROM Models AS m
LEFT JOIN Jobs AS j ON j.ModelId = m.ModelId
LEFT JOIN Orders AS o ON o.JobId = j.JobId
LEFT JOIN OrderParts AS op ON op.OrderId = o.OrderId
LEFT JOIN Parts AS p ON p.PartId = op.PartId
GROUP BY m.ModelId, m.Name
ORDER BY COUNT(DISTINCT j.JobId) DESC