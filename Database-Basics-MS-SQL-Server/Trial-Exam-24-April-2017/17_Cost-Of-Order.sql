CREATE FUNCTION udf_GetCost(@jobId INT) RETURNS DECIMAL(6, 2)
BEGIN
	DECLARE @totalCost DECIMAL(6, 2)
	SET @totalCost = (
		SELECT 
			ISNULL(SUM(p.Price * op.Quantity), 0) 
		FROM Jobs AS j
		LEFT JOIN Orders AS o ON o.JobId = j.JobId
		LEFT JOIN OrderParts AS op ON op.OrderId = o.OrderId
		LEFT JOIN Parts AS p ON p.PartId = op.PartId
		WHERE j.JobId = @jobId
		GROUP BY j.JobId
	)

	RETURN @totalCost
END