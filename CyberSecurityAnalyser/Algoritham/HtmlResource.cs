using System;
using System.Linq;
using CyberSecurityAnalyser.Algoritham.Data;

namespace CyberSecurityAnalyser.Algoritham
{
    public class HtmlResource : FileResource
    {
        public HtmlResource(RepoLocation location) : base(location)
        {
            RepoResourceType = RepoResourceType.HTMLPage;
        }

        public override void ParseErrors()
        {
            try
            {
                    var htmlDoc = new HtmlAgilityPack.HtmlDocument { OptionFixNestedTags = true };

                    htmlDoc.Load(Location.Path);
                    if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Any())
                    {
                    }
                    else
                    {
                        if (htmlDoc.DocumentNode != null)
                        {
                            var formNode = htmlDoc.DocumentNode.SelectSingleNode("//form");

                            if (formNode != null)
                            {
                                if (formNode.HasAttributes)
                                {
                                    var autoComplete = formNode.Attributes.FirstOrDefault(a => a.Name.ToUpper() == "AUTOCOMPLETE");
                                    if (autoComplete != null && autoComplete.Value.ToUpper() != "OFF".ToUpper())
                                    {
                                        Errors.Add(new Error(ErrorType.Sensative_Data_Exposure,
                                            "Form Autocomplete is On"));
                                    }
                                }
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                Errors.Add(new Error(ErrorType.Sensative_Data_Exposure, "Could not read the file" + ex.Message));
            }
        }
    }
}