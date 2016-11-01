using YHC.Portal.Extensions;
using YHC.Portal.Models;
using YHC.Portal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using YHC.Portal.Helper;

namespace YHC.Portal.Controllers
{
    public class PromoController : BaseAsyncController
    {
        private PromoInfoService _promoInfoService = null;
        public PromoInfoService PromoInfoService
        {
            get
            {
                if (_promoInfoService == null)
                {
                    _promoInfoService = new PromoInfoService();
                }
                return _promoInfoService;
            }
        }

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

        public async Task<ActionResult> Index(int? type, int? id)
        {
            var types = await PromoInfoService.GetAllType();
            if (!types.HasError)
            {
                ViewBag.PromoTypes = types.Data;
            }

            var result2 = await PromoInfoService.GetList(null, 0, 20);
            if (!result2.HasError)
            {
                ViewBag.PromoList = result2.Data.list;
            }

            var promoImages = await NewsService.GetAds("yc_promo_images_1", 10, 0);
            if (!promoImages.HasError)
            {
                if (promoImages.Data != null && promoImages.Data.count > 0)
                {
                    ViewBag.Images = promoImages.Data.list;
                }
            }


            var seo = await NewsService.GetSEO("promo");
            if (!seo.HasError)
            {
                ViewBag.SEO = seo.Data;
            }
            return View();
        }

        public async Task<ActionResult> Content(int? id)
        {
            if (id.HasValue && id.Value > 0)
            {
                var result2 = await PromoInfoService.GetInfo(id.Value);
                if (!result2.HasError)
                {
                    if (result2.Data != null)
                    {
                        ViewBag.Promo = result2.Data;
                    }
                }
            }
            
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> GetTypes()
        {
            var result = await PromoInfoService.GetAllType(true);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("", result.Data);
        }

        public async Task<ActionResult> GetList(int? type, Int32? pageIndex, Int32? pageSize)
        {
            int pIndex = pageIndex.GetValueOrDefault(0);
            int pSize = pageSize.GetValueOrDefault(10);
            var result = await PromoInfoService.GetList(type,pIndex, pSize);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("", result.Data);
        }

    }
}
