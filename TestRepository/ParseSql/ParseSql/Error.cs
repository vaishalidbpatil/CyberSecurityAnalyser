namespace ParseSql
{
    internal class Error
    {
        public ErrorType ErrorType { get; private set; }

        public string ErrorString { get; private set; }

        public Error(ErrorType errorType, string errorString)
        {
            ErrorType = errorType;
            ErrorString = errorString;
        }
    }

    public enum ErrorType
    {
        Dynamic_Sql_Threat
    }
}