using CyberSecurityAnalyser.Algoritham;
using CyberSecurityAnalyser.Algoritham.Data;

namespace CyberSecurityAnalyser.Models
{
    public class ErrorViewModel
    {
        public string ErrorMessage { get; set; }
        public ErrorType ErrorType { get; set; }
        public RepoLocation ResourceLocation { get; set; }
        public ErrorViewModel(Error error , BaseResource resource)
        {
            ErrorMessage = error.ErrorMessage;
            ErrorType = error.ErrorType;
            ResourceLocation = resource.Location;
        }
    }
}