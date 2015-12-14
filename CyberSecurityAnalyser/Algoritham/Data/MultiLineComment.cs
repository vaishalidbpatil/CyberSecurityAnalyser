namespace CyberSecurityAnalyser.Algoritham.Data
{
    public class MultiLineComment
    {
        public string StartOfMultiLineComment { get; private set; }
        public string EndOfMultiLineComment { get; private set; }

        public MultiLineComment(string startOfComment, string endOfComment)
        {
            StartOfMultiLineComment = startOfComment;
            EndOfMultiLineComment = endOfComment;
        }
    }
}