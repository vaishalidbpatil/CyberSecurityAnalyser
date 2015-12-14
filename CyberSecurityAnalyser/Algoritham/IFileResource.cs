using System.Collections.Generic;

namespace CyberSecurityAnalyser.Algoritham
{
    public interface IFileResource:IResource
    {
        IList<string> FileContent { get; }
        IList<string> ReadFile(IResource resource);
    }
}