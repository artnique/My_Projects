using YHC.Portal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using YHC.Portal.Helper;
using YHC.Portal.Extensions;

namespace YHC.Portal.Controllers
{
    public class HelpController : Controller
    {
        private NewsService _newsService = null;
        public NewsService NewsService
        {
            get
            {
                if (_newsService == null)
                {
                    _newsService = new NewsService();
                }
                return _newsService;
            }
        }

        private AccountService _accountService = null;
        public AccountService AccountService
        {
            get
            {
                if (_accountService == null)
                {
                    _accountService = new AccountService();
                }
                return _accountService;
            }
        }

        public async Task<ActionResult> Index(string key = "")
        {
            //var result = await NewsService.GetAds("help_banner_1", 1, 0);
            //if (!result.HasError)
            //{
            //    if (result.Data != null && result.Data.count > 0)
            //    {
            //        ViewBag.TopAd = result.Data.list;
            //    }
            //}
            //var result2 = await NewsService.GetSubcategories("help");
            //if (!result2.HasError)
            //{
            //    ViewBag.HelpMenu = result2.Data;
            //}
            //ViewBag.Key = key;
            //switch (key)
            //{
            //    case "agent":
            //        ViewBag.NavOn = "nav-agent";
            //        break;
            //    default:
            //        ViewBag.NavOn = "nav-help";
            //        break;
            //}
            //if (key == "agent" || key == "dwhelp")
            //{
            //    var seo = await NewsService.GetSEO(key);
            //    if (!seo.HasError)
            //    {
            //        ViewBag.SEO = seo.Data;
            //    }
            //}
            //ViewBag.AgentLogin = "#";
            //if (!string.IsNullOrEmpty(key))
            //{
            //    var result = await NewsService.GetNews(key);
            //    if (!result.HasError)
            //    {
            //        if (result.Data != null)
            //        {
            //            ViewBag.Content = result.Data;
            //        }
            //    }
            //    if (key.ToLower() == "agent")
            //    {
            //        ViewBag.AgentLogin = ConfigHelper.AgentSiteDomain;
            //    }
            //}

            return View();
        }

        public async Task<ActionResult> SiteMap()
        {
            var content = await NewsService.GetNewsByKey("sitemap");
            if (!content.HasError)
            {
                ViewBag.Content = content.Data;
            }
            return View();
        }

        public async Task<ActionResult> GetHelpContent(string key)
        {
            var result = await NewsService.GetNews(key);
            if (!result.HasError)
            {
                if (result.Data != null)
                {
                    return this.Content(result.Data.Content);
                }
            }
            return this.Content("");
        }

        public async Task<ActionResult> Partner()
        {
            return View();
        }

        public ActionResult Licence()
        {
            return View();
        }

        public ActionResult PwdForget()
        {
            return View();
        } 
    }
}
