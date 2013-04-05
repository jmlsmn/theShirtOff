using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainModel.Abstract.Services;
using DomainModel.Abstract.Entities;
using Contest.Generic.Models;
using Contest.Generic.Helpers;
using Castle.Core.Logging;

namespace Contest.Generic.Controllers
{
    public class VoteController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly ISubmissionService _submissionService;

        public VoteController(IAccountService accountService, ISubmissionService submissionService)
        {
            _accountService = accountService;
            _submissionService = submissionService;
        }

        public JsonResult VoteFB(AccountModel account, int submissionID)
        {
            _accountService.Vote(account, submissionID);
            int voteCount = _submissionService.GetVoteCount(submissionID);
            return Json(voteCount);
        }

        public JsonResult Vote(int submissionID)
        {
            _accountService.Vote(CurrentAccount, submissionID);
            int voteCount = _submissionService.GetVoteCount(submissionID);
            return Json(voteCount);
        }
        
    }
}
