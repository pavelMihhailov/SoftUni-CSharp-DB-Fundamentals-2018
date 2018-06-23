SELECT 
	CONCAT(m.FirstName, ' ', m.LastName) AS Available
FROM Mechanics AS m
LEFT JOIN Jobs AS j ON j.MechanicId = m.MechanicId
GROUP BY m.FirstName, m.LastName, m.MechanicId
HAVING COUNT(
				CASE 
					WHEN j.Status IS NULL THEN NULL
					WHEN j.Status = 'Finished' THEN NULL
					ELSE 1
				END
			) = 0
ORDER BY m.MechanicId