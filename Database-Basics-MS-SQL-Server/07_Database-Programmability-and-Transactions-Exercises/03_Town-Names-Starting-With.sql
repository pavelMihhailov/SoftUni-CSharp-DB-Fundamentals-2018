CREATE PROC usp_GetTownsStartingWith(@StartWith NVARCHAR(50))
		 AS
	 SELECT [Name] AS [Town] FROM Towns
	  WHERE LEFT([Name], LEN(@StartWith)) = @StartWith