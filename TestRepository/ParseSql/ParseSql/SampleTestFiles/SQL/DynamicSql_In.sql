/* Variable Declaration */
DECLARE @EmpID AS NVARCHAR(50)
DECLARE @SQLQuery AS NVARCHAR(500)

/* Build and Execute a Transact-SQL String with a single 
parameter value Using sp_executesql Command */
SET @EmpID = '1001,1003' 
SET @SQLQuery = 'SELECT * FROM tblEmployees 
WHERE EmployeeID IN(' + @EmpID + ')'
EXECUTE sp_executesql @SQLQuery