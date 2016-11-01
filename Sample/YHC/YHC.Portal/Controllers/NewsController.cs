using YHC.Portal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using YHC.Portal.Helper;
using YHC.Portal.Models;

namespace YHC.Portal.Controllers
{
    public class NewsController : Controller
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

        public async Task<ActionResult> Index()
        {
            return View();
        }

        public async Task<ActionResult> GetNotices(Int32? pageIndex, Int32? pageSize, Boolean? broadcasted)
        {
            int pIndex = pageIndex.GetValueOrDefault();
            int pSize = pageSize.GetValueOrDefault(10);
            var result = await NewsService.GetNotices(pIndex, pSize, broadcasted);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.Json(result.Data);
            }
        }

        public async Task<ActionResult> GetPageFooter()
        {
            var result = await NewsService.GetPageFooter();
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK(data: result.Data.Content);
        }

        public async Task<ActionResult> GetFloat()
        {
            var result = await NewsService.GetNewsByKey("index_block_ad");
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK(data: result.Data.Content);
        }

        public async Task<ActionResult> GetByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return this.JsonError("key 不能为空！");
            }
            var result = await NewsService.GetNewsByKey(key);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK(data: result.Data);
        }

        public async Task<ActionResult> GetAds(string type, int pageSize = 1, int pageIndex = 0)
        {
            if (string.IsNullOrEmpty(type))
            {
                return this.JsonError("type 不能为空！");
            }
            var result = await NewsService.GetAds(type, pageSize, pageIndex);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK(data: result.Data);
        }

        // 近两天的广播
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> GetTwoDaysBroadcast(Int32? pageIndex, Int32? pageSize)
        {
            DateTime now = SystemTime.UTCNow().AddDays(-2);
            var result = await NewsService.GetBroadcast(5, now);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.Json(result.Data);
        }
    }
}
