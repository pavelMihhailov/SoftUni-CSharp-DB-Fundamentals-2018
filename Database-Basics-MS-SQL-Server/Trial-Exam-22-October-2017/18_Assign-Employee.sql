CREATE PROC usp_AssignEmployeeToReport(@employeeId INT, @reportId INT)
AS
BEGIN
	BEGIN TRANSACTION
		DECLARE @employeeDepartmentId INT
		SET @employeeDepartmentId = (
			SELECT DepartmentId FROM Employees
			WHERE Id = @employeeId
		)

		DECLARE @reportDepartmentId INT
		SET @reportDepartmentId = (
			SELECT c.DepartmentId FROM Reports AS r
			JOIN Categories AS c ON c.Id = r.CategoryId
			WHERE r.Id = @reportId
		)

		UPDATE Reports
		SET EmployeeId = @employeeId
		WHERE Id = @reportId

		IF(@employeeDepartmentId <> @reportDepartmentId)
		BEGIN
			ROLLBACK
			RAISERROR('Employee doesn''t belong to the appropriate department!', 16, 2)
			RETURN
		END
	COMMIT
END