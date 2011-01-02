using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.Messaging;
using System.Web.Security;
using Wom.Core;
using Wom.Commands;

namespace WorksOnMono.Controllers
{
    public class HomeController : Controller
    {
        private static OpenIdRelyingParty openid = new OpenIdRelyingParty();
        private ICommandInvoker commandInvoker;

        public HomeController(ICommandInvoker commandInvoker)
        {
            this.commandInvoker = commandInvoker;
        }
        
        public ActionResult Index()
        {
            Response.AppendHeader(
                "X-XRDS-Location",
                new Uri(Request.Url, Response.ApplyAppPathModifier("~/Home/xrds")).AbsoluteUri);
            return View("Index");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        public ActionResult Xrds()
        {
            return View("Xrds");
        }

        [ValidateInput(false)]
        public ActionResult Authenticate(string returnUrl)
        {
            var response = openid.GetResponse();
            if (response == null && Request.HttpMethod == "POST")
            {
                Identifier id;
                if (Identifier.TryParse(Request.Form["openid_identifier"], out id))
                {
                    try
                    {
                        return (ActionResult)(Object)openid.CreateRequest(Request.Form["openid_identifier"]).RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException)
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            else if (response == null && Request.HttpMethod == "GET")
            {
                return View();
            }
            else
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:

                        EnsureUserExists(response);
                        SetAuthCookie(response);
                     return RedirectAfterLogon(returnUrl);

                    case AuthenticationStatus.Canceled:
                        return View();
                    case AuthenticationStatus.Failed:
                        return View();
                }
            }
            return new EmptyResult();
        }

        private static void SetAuthCookie(IAuthenticationResponse response)
        {
            FormsAuthentication.SetAuthCookie(response.ClaimedIdentifier, false);
        }

        private void EnsureUserExists(IAuthenticationResponse response)
        {
            commandInvoker.Execute(new EnsureUserExistsCommand(response.ClaimedIdentifier, response.FriendlyIdentifierForDisplay));
        }

        private ActionResult RedirectAfterLogon(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
    }
}
