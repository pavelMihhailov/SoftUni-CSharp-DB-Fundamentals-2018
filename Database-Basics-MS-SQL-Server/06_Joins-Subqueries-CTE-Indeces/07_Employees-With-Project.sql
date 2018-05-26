SELECT TOP 5
		     e.EmployeeID,
			 e.FirstName,
			 projects.[Name]
		FROM Employees AS e
		JOIN EmployeesProjects AS emp
		  ON emp.EmployeeID = e.EmployeeID
		JOIN Projects AS projects
		  ON projects.ProjectID = emp.ProjectID
	   WHERE projects.StartDate > '08-13-2002'
		 AND projects.EndDate IS NULL
	ORDER BY e.EmployeeID ASC