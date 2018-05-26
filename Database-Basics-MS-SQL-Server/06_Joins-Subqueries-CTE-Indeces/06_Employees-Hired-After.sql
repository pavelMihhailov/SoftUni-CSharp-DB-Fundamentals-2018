  SELECT e.FirstName, 
		 e.LastName, 
		 e.HireDate, 
		 d.[Name]
	FROM Employees AS e
	JOIN Departments AS d
	  ON d.DepartmentID = e.DepartmentID
   WHERE HireDate > '01-01-1999'
	 AND d.[Name] IN ('Finance', 'Sales')
ORDER BY e.HireDate ASC