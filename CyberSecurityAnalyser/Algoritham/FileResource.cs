using System;
using System.Collections.Generic;
using System.IO;
using CyberSecurityAnalyser.Algoritham.Data;

namespace CyberSecurityAnalyser.Algoritham
{
    public class FileResource : BaseResource, IFileResource
    {
        public FileResource(RepoLocation location):base(location)
        {
            RepoResourceType = RepoResourceType.FILE;
        }

        public IList<string> FileContent
        {
            get { return ReadFile(this); }
        }

        public virtual IList<string> ReadFile(IResource resource)
        {
            var fileContent = new List<string>();
            try
            {
                using (var sr = new StreamReader(resource.Location.Path))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        fileContent.Add(line);
                    }
                }

            }
            catch (Exception ex)
            {
                Errors.Add(new Error(ErrorType.Sensative_Data_Exposure, "Could not read the file" + ex.Message));
            }

            return fileContent;
        }
    }
}