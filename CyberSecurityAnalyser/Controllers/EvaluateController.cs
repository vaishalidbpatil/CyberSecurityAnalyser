using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using CyberSecurityAnalyser.Algoritham;
using CyberSecurityAnalyser.Models;

namespace CyberSecurityAnalyser.Controllers
{
    public class EvaluateController : Controller
    {
        public IList<RepoViewModel> RepoViewModels = new List<RepoViewModel>();
        private SourceRepository _evaluationRepo;
        public IList<ErrorViewModel> ErrorViewModels= new List<ErrorViewModel>();

       public ActionResult Index()
        {
           return View("Index");
        }


        public ActionResult Summary(RepoViewModel repo)
        {
            if (!string.IsNullOrEmpty(repo.Path))
            {
                if (IsValidUserInput(repo.Path))
                {
                    _evaluationRepo = new SourceRepository(repo.Path);
                    CollectResources();
                    ProcessResources();
                }
                else
                {
                    repo.HasError = true;
                    repo.ErrorMessage = "Couldnot find path " + repo.Path;
                    RepoViewModels.Add(repo);
                }
            }
            var summary = new SummaryViewModel()
            {
                RepoViewModels = RepoViewModels,
                ErrorViewModels = ErrorViewModels,
            };
            return View("Summary", summary);
        }

        private void ProcessResources()
        {
            foreach (var resource in _evaluationRepo.RepoResources)
            {
                var repo = new RepoViewModel();
                repo.Path = resource.Location.Path;
                repo.RepoResourceType = resource.RepoResourceType;
                var errors = resource.Errors;

                foreach (var error in  errors)
                {
                    repo.ErrorMessage = "Has Errors";
                    repo.HasError = true;
                    ErrorViewModels.Add(new ErrorViewModel(error,resource));
                }
                RepoViewModels.Add(repo);
            }
        }

        private void CollectResources()
        {
            _evaluationRepo.CollectReourcesInTheRepo();
        }

        private bool IsValidUserInput(string path)
        {
            return new FileInfo(path).Exists || Directory.Exists(path);
        }
    }
}