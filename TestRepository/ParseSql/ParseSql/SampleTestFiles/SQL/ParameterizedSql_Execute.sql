/* Using EXECUTE Command */
/* Build and Execute a Transact-SQL String with a single parameter 
 value Using EXECUTE Command */

/* Variable Declaration */
DECLARE @EmpID AS SMALLINT
DECLARE @SQLQuery AS NVARCHAR(500)
/* set the parameter value */
SET @EmpID = 1001
/* Build Transact-SQL String with parameter value */
SET @SQLQuery = 'SELECT * FROM tblEmployees WHERE EmployeeID = ' + 
CAST(@EmpID AS NVARCHAR(10))
/* Execute Transact-SQL String */
EXECUTE(@SQLQuery)