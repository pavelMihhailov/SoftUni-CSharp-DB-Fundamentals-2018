CREATE FUNCTION udf_GetReportsCount(@employeeId INT, @statusId INT)
RETURNS INT
BEGIN
	DECLARE @sum INT
	SET @sum = (
			SELECT COUNT(*) FROM Reports AS r
			WHERE r.EmployeeId = @employeeId AND
			r.StatusId = @statusId
	)

	RETURN @sum
END