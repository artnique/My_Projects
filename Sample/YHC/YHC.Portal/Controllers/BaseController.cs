using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using YHC.Portal.Helper;
using System.Web.Script.Serialization;

namespace YHC.Portal.Controllers
{
    public class BaseController : Controller
    {
        protected internal async Task<ViewResult> ViewAsync()
        {
            var t = Task.Delay(100);
            return View(1);
        }
    }
}
