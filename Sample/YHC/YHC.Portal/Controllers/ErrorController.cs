using YHC.Portal.Helper;
using YHC.Portal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using YHC.Portal.Extensions;

namespace YHC.Portal.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Error403()
        {
            return View();
        }
        public ActionResult Error404()
        {
            return View();
        }
        public ActionResult Error500()
        {
            return View();
        }
        public ActionResult Limit()
        {
            return View();
        }
        public ActionResult Maintainance()
        {
            return View();
        }
	}
}