CREATE PROC usp_GetHoldersWithBalanceHigherThan(@number DECIMAL(15, 2))
AS
BEGIN
	SELECT ah.FirstName AS [First Name], ah.LastName AS [Last Name] FROM Accounts AS a
	JOIN AccountHolders AS ah ON ah.Id = a.AccountHolderId
	GROUP BY a.AccountHolderId, ah.FirstName, ah.LastName
	HAVING SUM(a.Balance) > @number
	ORDER BY ah.LastName, ah.FirstName
END