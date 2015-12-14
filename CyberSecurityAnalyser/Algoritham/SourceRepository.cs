using System.Collections.Generic;
using System.IO;
using System.Linq;
using CyberSecurityAnalyser.Algoritham.Data;
using WebGrease.Css.Extensions;

namespace CyberSecurityAnalyser.Algoritham
{
    public class SourceRepository
    {
        private string _path;

        public string Path { get { return _path; } private set { _path = value; } }
        public IList<BaseResource> RepoResources { get; set; }
        public SourceRepository(string path)
        {
            _path = path;
            RepoResources = new List<BaseResource>();
        }

        public void CollectReourcesInTheRepo()
        {
            var paths = new List<string> {Path};
            RepoResources=ProcessDirectoryResource(paths, RepoResources);
        }

        public IList<string> CollectResourcesAt(string targetDirectory, out IList<BaseResource> resources)
        {
            resources = new List<BaseResource>();
            // Process the list of files found in the directory.
            foreach (var file in Directory.GetFiles(targetDirectory))
            {
                resources.Add(CreateResource(file, RepoResourceType.FILE));
            }
            var subDirectories = new List<string>();
            foreach (var directory in Directory.GetDirectories(targetDirectory))
            {
                subDirectories.Add(directory);
                resources.Add(CreateResource(directory, RepoResourceType.DIRECTORY));
            }
            
            return subDirectories;
        }

        public IList<BaseResource> ProcessDirectoryResource(IList<string> targetDirectory, IList<BaseResource> resources)
        {
            foreach (var dir in targetDirectory)
            {
                IList<BaseResource> resourcesAtDir;
                var subDirPaths = CollectResourcesAt(dir, out resourcesAtDir);
                resourcesAtDir.ForEach(resource => resources.Add(resource));
                subDirPaths.ForEach(dirPath => targetDirectory.Add(dirPath));
                targetDirectory.Remove(dir);
                return ProcessDirectoryResource(targetDirectory, resources);
            }
            return resources;
        }

        private BaseResource CreateResource(string fileName, RepoResourceType resourceType)
        {
            var location = new RepoLocation(fileName);
            return BaseResource.CreateResource(location, resourceType);
        }
    }
}