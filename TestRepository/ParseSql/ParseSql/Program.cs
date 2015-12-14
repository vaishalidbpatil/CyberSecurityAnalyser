using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParseSql
{
    class Program
    {
        public static void Main()
        {
           ReadFileName();
           Console.ReadLine();
        }

        public static List<Error> CompilationErrors = new List<Error>();
        private static void ReadFileName()
        {
            string filename;
            do
            {
                Console.WriteLine("\nEnter File name to be parced");
                filename = Console.ReadLine();
            } while (!CheckFileExists(filename));

            var fileInfo = new FileInfo(filename);
            switch (fileInfo.Extension)
            {
                case ".sql":
                    ParseForDynamicSql(fileInfo);
                    break;
            }
        }

        private static void ParseForDynamicSql(FileInfo fileInfo)
        {
            var myParsingFile = ConvertToMyAppFileObject(fileInfo);
            //DecideTypeOfSqlStatementItIs();
            CompilationErrors = CheckForDynamicSqlMatch(myParsingFile);
            PrintParsingErrors(CompilationErrors);
            //AppendOverAllErrors();

        }

        private static void PrintParsingErrors(List<Error> compilationErrors)
        {
            int count = 0;
            if (compilationErrors.Count == 0)
            {
                Console.WriteLine("No OWASP Errors !!");
            }
            foreach (var compilationError in compilationErrors)
            {
                Console.WriteLine(count++ + "\t" +compilationError.ErrorType.ToString() + "\t" + compilationError.ErrorString);
            }
        }

       private static List<Error> CheckForDynamicSqlMatch(ParsingFile myParsingFile)
       {
           var matchErrorCollection = myParsingFile.ContentsDynamicSqlStatements();
           var compilationErrors = new List<Error>();
           if (matchErrorCollection.Count > 0)
           {
               foreach (Match matchError in matchErrorCollection)
               {
                   var endOfLineIndex = myParsingFile.FileContent.IndexOf("\n", matchError.Groups[0].Index, StringComparison.Ordinal);
                   var error = new Error(ErrorType.Dynamic_Sql_Threat,
                       myParsingFile.FileContent.Substring(matchError.Groups[0].Index,endOfLineIndex- matchError.Groups[0].Index));

                   compilationErrors.Add(error);
               }
           }

            return compilationErrors;
        }

        private static ParsingFile ConvertToMyAppFileObject(FileInfo fileInfo)
        {
            return new ParsingFile(fileInfo);
        }

        private void DecideTypeOfSqlStatementItIs()
        {
            
        }

        private static bool CheckFileExists(string filename)
        {
            var file = new FileInfo(filename);
            return file.Exists;
        }
    }
}
