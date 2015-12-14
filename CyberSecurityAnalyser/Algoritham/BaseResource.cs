using System;
using System.Collections.Generic;
using System.IO;
using CyberSecurityAnalyser.Algoritham.Data;

namespace CyberSecurityAnalyser.Algoritham
{
    public abstract class BaseResource : IResource
    {
        public RepoLocation Location { get; set; }
        public RepoResourceType RepoResourceType { get; set; }
        public Errors Errors { get; set; }
        

        public BaseResource(RepoLocation location)
        {
            Errors = new Errors();
            Location = location;
            ParseErrors();
        }

        public virtual void ParseErrors()
        {
            
        }

        public static BaseResource CreateResource(RepoLocation location, RepoResourceType resourceType)
        {
            var extention = ReadExtension(location.Path);
            if (resourceType == RepoResourceType.DIRECTORY)
            {
                return new FolderResource(location);
            }
            switch (extention)
            {
                case ".html":
                    return new HtmlResource(location);
                case ".sql":
                    return new SqlResource(location);
                default:
                    return new FileResource(location);
            }
        }

        private static string ReadExtension(string fullpath)
        {
            return (new FileInfo(fullpath)).Extension;
        }
    }
}