/* Variable Declaration */
DECLARE @EmpName AS NVARCHAR(50)
DECLARE @SQLQuery AS NVARCHAR(500)

/* Build and Execute a Transact-SQL String with a single parameter 
value Using sp_executesql Command */
SET @EmpName = 'John' 
SET @SQLQuery = 'SELECT * FROM tblEmployees 
WHERE EmployeeName LIKE '''+ '%' + @EmpName + '%' + '''' 
EXECUTE sp_executesql @SQLQuery