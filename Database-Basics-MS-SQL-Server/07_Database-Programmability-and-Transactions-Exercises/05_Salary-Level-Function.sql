CREATE FUNCTION	ufn_GetSalaryLevel(@salary DECIMAL(18, 4)) RETURNS NVARCHAR(50)
AS
BEGIN
	DECLARE @levelOfSalary NVARCHAR(50)
	IF(@salary < 30000) 
	BEGIN
		SET @levelOfSalary = 'Low'
	END

	ELSE IF(@salary BETWEEN 30000 AND 50000) 
	BEGIN
		SET @levelOfSalary = 'Average'
	END

	ELSE
	BEGIN
		SET @levelOfSalary = 'High'
	END

	RETURN @levelOfSalary
END