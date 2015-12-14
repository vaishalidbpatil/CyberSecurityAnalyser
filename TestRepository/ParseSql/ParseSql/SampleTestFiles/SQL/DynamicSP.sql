/* This stored procedure builds dynamic SQL and executes 
using sp_executesql */
Create Procedure sp_EmployeeSelect
    /* Input Parameters */
    @EmployeeName NVarchar(100),
    @Department NVarchar(50),
    @Designation NVarchar(50),
    @StartDate DateTime,
    @EndDate DateTime,
    @Salary    Decimal(10,2)
        
AS
    Set NoCount ON
    /* Variable Declaration */
    Declare @SQLQuery AS NVarchar(4000)
    Declare @ParamDefinition AS NVarchar(2000) 
    /* Build the Transact-SQL String with the input parameters */ 
    Set @SQLQuery = 'Select * From tblEmployees where (1=1) ' 
    /* check for the condition and build the WHERE clause accordingly */
    If @EmployeeName Is Not Null 
         Set @SQLQuery = @SQLQuery + ' And (EmployeeName = @EmployeeName)'

    If @Department Is Not Null
         Set @SQLQuery = @SQLQuery + ' And (Department = @Department)' 
  
    If @Designation Is Not Null
         Set @SQLQuery = @SQLQuery + ' And (Designation = @Designation)'
  
    If @Salary Is Not Null
         Set @SQLQuery = @SQLQuery + ' And (Salary >= @Salary)'

    If (@StartDate Is Not Null) AND (@EndDate Is Not Null)
         Set @SQLQuery = @SQLQuery + ' And (JoiningDate 
         BETWEEN @StartDate AND @EndDate)'
    /* Specify Parameter Format for all input parameters included 
     in the stmt */
    Set @ParamDefinition =      ' @EmployeeName NVarchar(100),
                @Department NVarchar(50),
                @Designation NVarchar(50),
                @StartDate DateTime,
                @EndDate DateTime,
                @Salary    Decimal(10,2)'
    /* Execute the Transact-SQL String with all parameter value's 
       Using sp_executesql Command */
    Execute sp_Executesql     @SQLQuery, 
                @ParamDefinition, 
                @EmployeeName, 
                @Department, 
                @Designation, 
                @StartDate, 
                @EndDate,
                @Salary
                
    If @@ERROR <> 0 GoTo ErrorHandler
    Set NoCount OFF
    Return(0)
  
ErrorHandler:
    Return(@@ERROR)
GO