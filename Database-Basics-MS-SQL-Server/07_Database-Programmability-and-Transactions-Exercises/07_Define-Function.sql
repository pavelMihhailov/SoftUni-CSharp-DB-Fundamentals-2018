CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(MAX), @word NVARCHAR(MAX))
RETURNS BIT
AS
BEGIN
	DECLARE @isComprised BIT
	DECLARE @currIndex INT = 1
	DECLARE @currChar CHAR

	WHILE(@currIndex <= LEN(@word))
	BEGIN
		SET @currChar = SUBSTRING(@word, @currIndex, 1)
		IF(CHARINDEX(@setOfLetters, @currChar) = 0)
		BEGIN
			RETURN @isComprised
		END
		SET @currIndex += 1
	END

	RETURN @isComprised + 1
END