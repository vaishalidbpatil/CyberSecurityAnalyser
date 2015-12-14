using CyberSecurityAnalyser.Algoritham.Data;

namespace CyberSecurityAnalyser.Algoritham
{
    public class FolderResource : BaseResource
    {
        public FolderResource(RepoLocation location):base(location)
        {
            RepoResourceType = RepoResourceType.DIRECTORY;
        }
    }
}