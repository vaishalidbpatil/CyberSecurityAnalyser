using System.Linq;
using CyberSecurityAnalyser.Algoritham.Data;

namespace CyberSecurityAnalyser.Algoritham
{
    public class SqlResource : FileResource
    {
        public SqlResource(RepoLocation location):base(location)
        {
            RepoResourceType = RepoResourceType.SQLFile;
        }

        public override void ParseErrors()
        {
            var file = (IFileResource) this;
            var executableLinesInFile = FilterCommentsAndGetExecutableLines(file);
            ValudateExecuteDynamicSqlString(executableLinesInFile);
        }

        private static ExecutableLines FilterCommentsAndGetExecutableLines(IFileResource file)
        {
            var multiLineComment = new MultiLineComment("/*", @"*/");
            var fileMetadata = new FileMetadata("--", multiLineComment);

            foreach (var line in file.FileContent)
            {
                fileMetadata = ParseLine(line, fileMetadata);
            }
            return fileMetadata.ExecutableLines;
        }

        private static FileMetadata ParseLine(string toBeParsed, FileMetadata fileMetadata)
        {
            if (string.IsNullOrEmpty(toBeParsed))
                return fileMetadata;
            int cutToIndex;
            if (fileMetadata.CommentBlockActive)
            {
                var indexOfEndOfMultiLineComment = toBeParsed.IndexOf(fileMetadata.MultiLineComment.EndOfMultiLineComment);
                if (indexOfEndOfMultiLineComment == -1)
                {
                    return fileMetadata;
                }

                fileMetadata.CommentBlockActive = false;
                cutToIndex = indexOfEndOfMultiLineComment;
                if (cutToIndex + fileMetadata.MultiLineComment.EndOfMultiLineComment.Length + 1 < toBeParsed.Length)
                {
                    toBeParsed = toBeParsed.Substring(0,cutToIndex + fileMetadata.MultiLineComment.EndOfMultiLineComment.Length + 1);
                }
                else
                {
                    return fileMetadata;
                }
            }
            else
            {
                var indexOfStartOfSingleLineComment =
                    toBeParsed.IndexOf(fileMetadata.SingleLineComment);
                var indexOfStartOfMultiLineComment =
                    toBeParsed.IndexOf(fileMetadata.MultiLineComment.StartOfMultiLineComment);
                if (indexOfStartOfSingleLineComment == -1 && indexOfStartOfMultiLineComment == -1)
                {
                    fileMetadata.ExecutableLines.Add(toBeParsed);
                    return fileMetadata;
                }

                fileMetadata.CommentBlockActive = CheckIfMulticommentIsActive(indexOfStartOfMultiLineComment, indexOfStartOfSingleLineComment, out cutToIndex);

                if (!fileMetadata.CommentBlockActive)
                {
                    if (cutToIndex != 0)
                    {
                        if (cutToIndex + fileMetadata.MultiLineComment.StartOfMultiLineComment.Length + 1 <
                            toBeParsed.Length)
                        {
                            fileMetadata.ExecutableLines.Add(toBeParsed.Substring(0, cutToIndex + fileMetadata.MultiLineComment.StartOfMultiLineComment.Length + 1));
                            toBeParsed = toBeParsed.Substring(cutToIndex + fileMetadata.MultiLineComment.StartOfMultiLineComment.Length + 1, fileMetadata.MultiLineComment.StartOfMultiLineComment.Length);
                        }
                        else
                        {
                            return fileMetadata;
                        }
                    }
                    else
                    {
                        return fileMetadata;
                    }
                }
                else
                {
                    return ParseLine(toBeParsed, fileMetadata); 
                }
            }
           return ParseLine(toBeParsed.Substring(cutToIndex, toBeParsed.Length), fileMetadata);
        }

        private static bool CheckIfMulticommentIsActive(int indexOfStartOfMultiLineComment,
            int indexOfStartOfSingleLineComment, out int cutToIndex)
        {
            if (indexOfStartOfSingleLineComment >= 0)
            {
                if (indexOfStartOfMultiLineComment >= 0)
                {
                    if (indexOfStartOfSingleLineComment > indexOfStartOfMultiLineComment)
                    {
                        cutToIndex = indexOfStartOfMultiLineComment;
                        return true;
                    }
                }
                cutToIndex = indexOfStartOfSingleLineComment;
                return false;
            }

            cutToIndex = indexOfStartOfMultiLineComment;
            return true;
        }

        private void ValudateExecuteDynamicSqlString(ExecutableLines executableLinesInFile)
        {
            if (executableLinesInFile.Any(line => line.Contains(("EXECUTE"))) || executableLinesInFile.Any(line => line.Contains(("Execute"))) || executableLinesInFile.Any(line => line.Contains(("execute"))))
            { Errors.Add(new Error(ErrorType.Dynamic_Sql_Threat,"Error at Line : "+ executableLinesInFile.FirstOrDefault(line => line.Contains("EXECUTE"))));}
        }
    }
}