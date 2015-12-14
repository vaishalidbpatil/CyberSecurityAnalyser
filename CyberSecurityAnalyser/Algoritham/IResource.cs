using CyberSecurityAnalyser.Algoritham.Data;

namespace CyberSecurityAnalyser.Algoritham
{
    public interface IResource
    {
        Errors Errors { get; set; }
        RepoLocation Location { get; set; }
        RepoResourceType RepoResourceType { get; set; }
        void ParseErrors();
    }
}