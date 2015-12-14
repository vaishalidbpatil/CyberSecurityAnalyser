using System.Collections.Generic;

namespace CyberSecurityAnalyser.Algoritham.Data
{
    public class Errors : List<Error>
    {
        public Errors ()
        {            
        }
    }

    public class Error
    {
        public ErrorType ErrorType { get; private set; }

        public string ErrorMessage { get; private set; }
        public string ErrorDescription { get; private set; }
        public string SecurityViolationCode { get; private set; }
        public string Source { get; private set; }
        public string SourcePath { get; private set; }

        public Error(string error)
        {
            ErrorMessage = error;
            ErrorType = ErrorType.Unknown_Error;
            SecurityViolationCode = "";
            Source = "";
            SourcePath = "";
        }

        public Error(ErrorType errorType, string error)
        {
            ErrorMessage = error;
            ErrorType = errorType;
            SecurityViolationCode = "";
            Source = "";
            SourcePath = "";
        }

    }
    public enum ErrorType
    {
        Dynamic_Sql_Threat,
        Unknown_Error,
        Sensative_Data_Exposure
    }
}