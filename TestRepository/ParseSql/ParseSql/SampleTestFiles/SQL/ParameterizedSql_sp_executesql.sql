/* Using sp_executesql */
/* Build and Execute a Transact-SQL String with a single parameter 
value Using sp_executesql Command */

/* Variable Declaration */
DECLARE @EmpID AS SMALLINT
DECLARE @SQLQuery AS NVARCHAR(500)
DECLARE @ParameterDefinition AS NVARCHAR(100)
/* set the parameter value */
SET @EmpID = 1001
/* Build Transact-SQL String by including the parameter */
SET @SQLQuery = 'SELECT * FROM tblEmployees WHERE EmployeeID = @EmpID' 
/* Specify Parameter Format */
SET @ParameterDefinition =  '@EmpID SMALLINT'
/* Execute Transact-SQL String */
EXECUTE sp_executesql @SQLQuery, @ParameterDefinition, @EmpID