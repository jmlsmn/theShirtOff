using System.Web.Mvc;
using Contest.Generic.Helpers;
using DomainModel.Abstract.Services;
using Castle.Core.Logging;
using DomainModel.Abstract.Entities;
using System.Web.Security;
using Contest.Generic.Models;

namespace Contest.Generic.Controllers
{
    public abstract class BaseController : Controller
    {
        public ILogger Logger { get; set; }

        public IContestService ContestService { get; set; }

        public IContest CurrentContest
        {
            get
            {
                if (SessionManager.GetFromSession<IContest>("CurrentContest") == default(IContest))
                {
                    CurrentContest = ContestService.GetCurrentContest();
                }
                return SessionManager.GetFromSession<IContest>("CurrentContest");
            }
            set
            {
                SessionManager.SetInSession<IContest>("CurrentContest",value);
            }
        }

        public IAccount CurrentAccount
        {
            get
            {
                if (SessionManager.GetFromSession<IAccount>("CurrentAccount") == null && User.Identity.IsAuthenticated)
                {
                    FormsIdentity identity = (FormsIdentity)User.Identity;
                    FormsAuthenticationTicket ticket = identity.Ticket;
                    IAccount accountModel = new AccountModel(ticket.UserData);
                    CurrentAccount = accountModel;
                    return accountModel;
                }
               
                return SessionManager.GetFromSession<IAccount>("CurrentAccount");
            }
            set
            {
                SessionManager.SetInSession<IAccount>("CurrentAccount", value);
            }
        }

    }
}
