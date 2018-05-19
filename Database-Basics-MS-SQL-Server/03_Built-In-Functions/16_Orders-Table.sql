SELECT ProductName, 
	   OrderDate, 
	   DATEADD(DD, 3, OrderDate) AS [Pay Due],
	   DATEADD(MM, 1, OrderDate) AS [Deliver Due]
FROM Orders