  SELECT
	     e.EmployeeID,
		 e.FirstName,
		 IIF(DATEPART(YEAR, projects.StartDate) >= 2005, NULL, projects.Name) 
		 AS [ProjectName]
	FROM Employees AS e
	JOIN EmployeesProjects AS emp
	  ON emp.EmployeeID = e.EmployeeID
	JOIN Projects AS projects
	  ON projects.ProjectID = emp.ProjectID
   WHERE e.EmployeeID = 24