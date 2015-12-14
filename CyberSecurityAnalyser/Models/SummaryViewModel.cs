using System.Collections.Generic;

namespace CyberSecurityAnalyser.Models
{
    public class SummaryViewModel
    {
        public IList<RepoViewModel> RepoViewModels { set; get; }
        public IList<ErrorViewModel> ErrorViewModels { set; get; }
    }
}