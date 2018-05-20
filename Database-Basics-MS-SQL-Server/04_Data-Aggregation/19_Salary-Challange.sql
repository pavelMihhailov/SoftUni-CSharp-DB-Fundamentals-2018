SELECT TOP 10 FirstName, LastName, DepartmentID FROM Employees AS [Table1]
WHERE Salary > 
(SELECT AVG(Salary) FROM Employees AS [Table2]
 WHERE Table1.DepartmentID = Table2.DepartmentID
 GROUP BY DepartmentID
)
ORDER BY DepartmentID