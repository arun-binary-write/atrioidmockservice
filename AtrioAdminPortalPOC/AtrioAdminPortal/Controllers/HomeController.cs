using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Claims;
using System.Threading;

namespace AtrioAdminPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string swt = ((ClaimsIdentity)(Thread.CurrentPrincipal.Identity)).BootstrapToken.ToString();
            // TODO:
            // 1. Make call to ATRIO MOCK REST API
            // 2. Pass received permission set to view.
            // 3. Depending on permission set, decide to enable or disable controls.
            return View();
        }
    }
}
