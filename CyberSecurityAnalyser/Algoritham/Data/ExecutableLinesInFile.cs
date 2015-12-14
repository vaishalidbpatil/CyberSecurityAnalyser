using System.Collections.Generic;

namespace CyberSecurityAnalyser.Algoritham.Data
{
    public class FileMetadata 
    {
        public bool CommentBlockActive { get; set; }
        public string SingleLineComment { get; set; }
        public MultiLineComment MultiLineComment { get; set; }

        public ExecutableLines ExecutableLines { get; set; }

        public FileMetadata(string singleLineComment, MultiLineComment multiLineComment)
        {
            SingleLineComment = singleLineComment;
            MultiLineComment = multiLineComment;
            CommentBlockActive = false;
            ExecutableLines = new ExecutableLines();
        }
    }

    public class ExecutableLines : List<string>
    {
    }
}