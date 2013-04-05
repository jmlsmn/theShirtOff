using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Contest.Generic.Models;
using Contest.Generic.Helpers;
using DomainModel.Abstract.Services;
using DomainModel.Abstract.Entities;
using System.Web.Helpers;
using Contest.Generic.ViewModels;

namespace Contest.Generic.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AccountModel accountModel = new AccountModel();
                accountModel.Email = model.Email;
                accountModel.Password = model.Password;
                accountModel.Token = model.Token;

                if (Authenticate(accountModel, model.RememberMe))
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Json(new { success = true });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "The email or password provided is incorrect. Please try again." });
                }
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            FormsService.SignOut();

            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AccountModel model)
        {
            _accountService.AddAccount(model); 
            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public JsonResult ChangeEmail(string newEmail)
        {
            _accountService.ChangeEmail(CurrentAccount.AccountID, newEmail);
            return Json(true);
        }

        public PartialViewResult SignInView()
        {
            return PartialView("SignIn");
        }

        public ActionResult SignOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                CurrentAccount = null;
                FormsAuthentication.SignOut();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MyAccount()
        {
            return View(CurrentAccount);
        }

        #region Private Methods

        private bool Authenticate(IAccount accountModel, bool rememberMe)
        {
            IAccount authenticatedAccount = _accountService.AuthenticateAccount(accountModel);
            if (authenticatedAccount != null)
            {
                SetAuthenticationCookie(authenticatedAccount, rememberMe);
                ProcessQueryStringParameters();

                return true;
            }
            return false;
        }

        private void SetAuthenticationCookie(IAccount account, bool isPersistent)
        {
            var ticket = new FormsAuthenticationTicket(1,
                                                        account.Email,
                                                        DateTime.Now,
                                                        DateTime.Now.AddMinutes(30),
                                                        isPersistent,
                                                        account.ToObjectString(),
                                                        FormsAuthentication.FormsCookiePath);

            var strEncryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, strEncryptedTicket);
            Response.Cookies.Add(cookie);
        }

        private void ProcessQueryStringParameters()
        {
            ProcessEmailQueryStringParameters();
        }

        private void ProcessEmailQueryStringParameters()
        {
            string guid = Request.QueryString["guid"];
            string email = Request.QueryString["email"];

            if (!guid.IsNullOrEmpty() && email.IsNullOrEmpty())
            {
                //activate email
                _accountService.ActivateAccount(CurrentAccount.AccountID, new Guid(guid));
            }
            else if (!guid.IsNullOrEmpty() && !email.IsNullOrEmpty())
            {
                //change email
                _accountService.ChangeEmail(CurrentAccount.AccountID, email);
            }
        }

        #endregion
    }
}
