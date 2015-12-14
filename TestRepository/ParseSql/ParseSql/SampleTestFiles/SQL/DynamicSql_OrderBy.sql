/* Variable Declaration */
DECLARE @OrderBy AS NVARCHAR(50)
DECLARE @SQLQuery AS NVARCHAR(500)

/* Build and Execute a Transact-SQL String with a single parameter 
value Using sp_executesql Command */
SET @OrderBy = 'Department' 
SET @SQLQuery = 'SELECT * FROM tblEmployees Order By ' + @OrderBy

EXECUTE sp_executesql @SQLQuery