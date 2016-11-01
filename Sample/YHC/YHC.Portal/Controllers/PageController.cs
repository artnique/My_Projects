using YHC.Portal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using YHC.Portal.Filters;
using YHC.Portal.Models;
using YHC.Portal.Extensions;
using YHC.Portal.Services;
using YHC.Portal.Core;
using System.IO;


namespace YHC.Portal.Controllers
{
    public class PageController : BaseAsyncController
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

        public async Task<ActionResult> About()
        {
            ViewBag.Page = "About";
            var result = await NewsService.GetNewsByKey("yc_about_us_1");
            if (!result.HasError)
            {
                ViewBag.About = result.Data;
            }

            var result1 = await NewsService.GetAds("yc_help_banner_1", 10, 0);
            if (!result1.HasError)
            {
                if (result1.Data != null && result1.Data.count > 0)
                {
                    ViewBag.Banner = result1.Data.list;
                }
            }

            return View();
        }

        public async Task<ActionResult> AlternateURL()
        {
            ViewBag.Page = "Alternate";
            var result = await NewsService.GetNewsByKey("yc_alternate_url_1");
            if (!result.HasError)
            {
                ViewBag.AlternateURL = result.Data;
            }

            var result1 = await NewsService.GetAds("yc_help_banner_1", 10, 0);
            if (!result1.HasError)
            {
                if (result1.Data != null && result1.Data.count > 0)
                {
                    ViewBag.Banner = result1.Data.list;
                }
            }

            return View();
        }
        public async Task<ActionResult> Contact()
        {
            ViewBag.Page = "Contact";
            var result = await NewsService.GetNewsByKey("yc_contact_us_1");
            if (!result.HasError)
            {
                ViewBag.Contact = result.Data;
            }

            var result1 = await NewsService.GetAds("yc_help_banner_1", 10, 0);
            if (!result1.HasError)
            {
                if (result1.Data != null && result1.Data.count > 0)
                {
                    ViewBag.Banner = result1.Data.list;
                }
            }

            return View();
        }
        public async Task<ActionResult> Guide()
        {
            ViewBag.Page = "Guide";
            var result = await NewsService.GetNewsByKey("yc_new_hand_1");
            if (!result.HasError)
            {
                ViewBag.Guide = result.Data;
            }

            var result1 = await NewsService.GetAds("yc_help_banner_1", 10, 0);
            if (!result1.HasError)
            {
                if (result1.Data != null && result1.Data.count > 0)
                {
                    ViewBag.Banner = result1.Data.list;
                }
            }

            return View();
        }
        public async Task<ActionResult> Provision()
        {
            ViewBag.Page = "Provision";
            var result = await NewsService.GetNewsByKey("yc_privacy_policy_1");
            if (!result.HasError)
            {
                ViewBag.Provision = result.Data;
            }

            var result1 = await NewsService.GetAds("yc_help_banner_1", 10, 0);
            if (!result1.HasError)
            {
                if (result1.Data != null && result1.Data.count > 0)
                {
                    ViewBag.Banner = result1.Data.list;
                }
            }

            return View();
        }
        public async Task<ActionResult> Preferential()
        {
            ViewBag.Page = "Preferential";
            var result = await NewsService.GetNewsByKey("yc_preferential_1");
            if (!result.HasError)
            {
                ViewBag.Preferential = result.Data;
            }

            var result1 = await NewsService.GetAds("yc_help_banner_1", 10, 0);
            if (!result1.HasError)
            {
                if (result1.Data != null && result1.Data.count > 0)
                {
                    ViewBag.Banner = result1.Data.list;
                }
            }

            return View();
        }
        public async Task<ActionResult> Privacy()
        {
            ViewBag.Page = "Privacy";
            var result = await NewsService.GetNewsByKey("yc_privacy_protection_1");
            if (!result.HasError)
            {
                ViewBag.Privacy = result.Data;
            }

            var result1 = await NewsService.GetAds("yc_help_banner_1", 10, 0);
            if (!result1.HasError)
            {
                if (result1.Data != null && result1.Data.count > 0)
                {
                    ViewBag.Banner = result1.Data.list;
                }
            }

            return View();
        }
        public async Task<ActionResult> GameHelp()
        {
            ViewBag.Page = "Help";
            var result = await NewsService.GetNewsByKey("yc_game_help_1");
            if (!result.HasError)
            {
                ViewBag.GameHelp = result.Data;
            }

            var result1 = await NewsService.GetAds("yc_help_banner_1", 10, 0);
            if (!result1.HasError)
            {
                if (result1.Data != null && result1.Data.count > 0)
                {
                    ViewBag.Banner = result1.Data.list;
                }
            }

            return View();
        }
        public async Task<ActionResult> Notice()
        {
            NewsService Svc = new NewsService();
            ApiResult<PagerFrontViewModel<News>> notices = await Svc.GetAllNotices();
            Svc = null;
            if (!notices.HasError)
            {
                ViewBag.Notices = notices.Data;
            }

            var result1 = await NewsService.GetAds("yc_help_banner_1", 10, 0);
            if (!result1.HasError)
            {
                if (result1.Data != null && result1.Data.count > 0)
                {
                    ViewBag.Banner = result1.Data.list;
                }
            }

            ViewBag.Page = "Notice";
            return View();
        }

    }
}
