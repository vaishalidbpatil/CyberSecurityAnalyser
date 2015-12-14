using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParseSql
{
    public class ParsingFile
    {
        public string FilePath { get; private set; }

        public ParsingFile(FileInfo fileInfo)
        {
            FilePath = fileInfo.DirectoryName;
            FileName = fileInfo.Name;
            FileType = IdentifyFileType(fileInfo.Extension);
            ReadFile(fileInfo);
        }

        private async void ReadFile(FileInfo fileInfo)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileInfo.FullName))
                {
                    var line = await sr.ReadToEndAsync();
                    FileContent = line;
                }
            }
            catch (Exception ex)
            {
                FileContent = "Could not read the file" + ex.Message;
            }
        }
  
        
        public string FileContent { get; private set; }

        public ParsingFileType FileType { get; private set; }

        private ParsingFileType IdentifyFileType(string extension)
        {
            switch (extension)
            {
                case "sql" :
                    return ParsingFileType.Sqlfile;
            }

            return ParsingFileType.Unknown;
        }

        public string FileName  { get; private set; }

        public MatchCollection ContentsDynamicSqlStatements()
        {
            var token = new Regex("SET @[A Z][a z] = N'S|select");
            var matches = token.Matches(FileContent);
            return matches;
        }
    }
}