using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using DomainModel.Concrete.Extensions;
using DomainModel.Abstract.Entities;
using Contest.Generic.Models;

namespace Contest.Generic.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public PartialViewResult FeaturedSubmissions()
        {
            var featuredSubmissions = ContestService.GetFeaturedSubmissions(CurrentContest.ContestID);
            List<ISubmission> submissionModels = new List<ISubmission>();
            
            foreach (var submission in featuredSubmissions)
            {
                submissionModels.Add(submission.ConvertTo<ISubmission>(typeof(SubmissionModel)));
            }
            return PartialView("FeaturedSubmissions", submissionModels);
        }

        public PartialViewResult LatestSubmissions()
        {
            var latestSubmissions = ContestService.GetLatestSubmissions(CurrentContest.ContestID);
            List<ISubmission> submissionModels = new List<ISubmission>();

            foreach (var submission in latestSubmissions)
            {
               submissionModels.Add(submission.ConvertTo<ISubmission>(typeof(SubmissionModel)));
            }
            return PartialView("LatestSubmissions", submissionModels);
        }

        public PartialViewResult ContestInformation()
        {
            return PartialView(CurrentContest);
        }
    }
}
