CREATE PROC usp_AssignProject(@EmployeeId INT, @ProjectId INT)
AS
BEGIN
	BEGIN TRANSACTION
		INSERT INTO EmployeesProjects(EmployeeID, ProjectID) VALUES
		(@EmployeeId, @ProjectId)

		DECLARE @numberOfProjects INT = 
		(SELECT COUNT(ep.ProjectID) FROM Employees AS e
		JOIN EmployeesProjects AS ep ON ep.EmployeeID =  e.EmployeeID
		WHERE e.EmployeeID = @EmployeeId)

		IF(@numberOfProjects > 3)
		BEGIN
			ROLLBACK
			RAISERROR('The employee has too many projects!', 16, 1)
			RETURN
		END

		COMMIT
END