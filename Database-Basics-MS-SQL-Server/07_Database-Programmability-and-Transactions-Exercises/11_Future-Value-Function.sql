CREATE FUNCTION ufn_CalculateFutureValue(@sum DECIMAL(15, 4), @yearlyInterestRate FLOAT, @numberOfYears INT)
RETURNS DECIMAL(15, 4)
AS
BEGIN
	DECLARE @value DECIMAL(15, 4)

	SET @value = @sum * (POWER(1 + @yearlyInterestRate, @numberOfYears))

	RETURN @value
END