using System;
using CyberSecurityAnalyser.Algoritham.Data;

namespace CyberSecurityAnalyser.Models
{
    [Serializable]
    public class RepoViewModel
    {
        public string Path { get; set; }
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public RepoResourceType RepoResourceType { get; set; }
    }
}   