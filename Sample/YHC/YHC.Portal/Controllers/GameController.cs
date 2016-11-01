using YHC.Portal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net;
using YHC.Portal.Models;
using YHC.Portal.Extensions;
using YHC.Portal.Helper;
using System.Collections.Specialized;
using YHC.Portal.Core;
using System.Globalization;
using System.Xml.Linq;

namespace YHC.Portal.Controllers
{
    public class GameController : BaseAsyncController
    {
        private GameService _gameService = null;
        public GameService GameService
        {
            get
            {
                if (_gameService == null)
                {
                    _gameService = new GameService();
                }
                return _gameService;
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

        public async Task<ActionResult> Sport(string game)
        {
            ViewBag.Game = game;
            string type = "";
            switch (game)
            {
                case "IBC":
                    type = "yc_sport_ibc_flash_1";
                    break;
                case "TB":
                    type = "yc_sport_tb_flash_1";
                    break;
                case "BB":
                    type = "yc_sport_bbin_flash_1";
                    break;
                default:
                    type = "yc_sport_t118_flash_1";
                    break;
            }

            var result = await NewsService.GetAds(type, 10, 0);
            if (!result.HasError)
            {
                ViewBag.Ad = result.Data.list;
            }

            var seo = await NewsService.GetSEO("sport");
            if (!seo.HasError)
            {
                ViewBag.SEO = seo.Data;
            }
            return View();
        }

        public async Task<ActionResult> LaunchSport(String game, String type, String target)
        {
            ApiResult<string> result = await GameService.LaunchSport(game, type);
            if (result.HasError)
            {
                if (result.ErrorCode == -1)
                {
                    return Redirect("/maintenance.html?type=api&game=" + game);
                }
                return Content(result.ErrorMessage);
            }
            else
            {
                return Redirect(result.Data);
            }
        }

        public async Task<ActionResult> Play(String game, String type, String gameId, Boolean? demo, String customGameUrl)
        {
            ApiResult<string> result = await GameService.Play(game, type, gameId, demo, customGameUrl);
            if (result.HasError)
            {
                return Content(result.ErrorMessage);
            }
            else
            {
                // 跳转到游戏平台
                if (game.Equals("PT", StringComparison.InvariantCultureIgnoreCase))
                {
                    // 如果是PT游戏，则跳转到单点登录页面
                    return Redirect("/game/ptloading.html?redirect=" + result.Data.UrlEncode());
                }
                if (game.ToUpper() == "T188")
                {
                    ViewBag.GameUrl = result.Data;
                    return View();
                }
                return Redirect(result.Data);
            }
        }

        // 试玩
        public async Task<ActionResult> TryPlay(String game, String type = "", String gameId = "")
        {
            if (string.IsNullOrWhiteSpace(game))
            {
                return Content("参数错误");
            }

            ApiResult<string> result = await GameService.TryPlay(game, type, gameId);
            if (result.HasError)
            {
                return Content(result.ErrorMessage);
            }
            else
            {
                return Redirect(result.Data);
            }
        }

        // PT游戏登录
        public async Task<ActionResult> PTLoading(String redirect)
        {
            String game = "PT";
            ApiResult<GameApi> gameApi = await GameService.GetGameApi(game);
            if (gameApi.HasError)
            {
                return Content(gameApi.ErrorMessage);
            }

            var gameAccount = await GameService.GetUserGameAccount(game);
            if (gameAccount.HasError)
            {
                return Content(gameAccount.ErrorMessage);
            }

            ViewBag.Api = gameApi.Data;
            ViewBag.GameAccount = gameAccount.Data;
            ViewBag.GameUrl = redirect;

            return View();
        }

        public async Task<ActionResult> Casino()
        {
            var banner = await NewsService.GetAds("yc_casino_flash_1", 10, 0);
            if (!banner.HasError)
            {
                if (banner.Data != null && banner.Data.count > 0)
                {
                    ViewBag.Banner = banner.Data.list;
                }
            }

            var ads = await NewsService.GetAds("yc_casino_ads_1", 10, 0);
            if (!ads.HasError)
            {
                if (ads.Data != null && ads.Data.count > 0)
                {
                    ViewBag.Ads = ads.Data.list;
                }
            }

            var page_images = await NewsService.GetAds("yc_casino_images", 10, 0);
            if (!page_images.HasError)
            {
                if (page_images.Data != null && page_images.Data.count > 0)
                {
                    ViewBag.PageImages = page_images.Data.list;
                }
            }

            var link_images = await NewsService.GetAds("yc_casino_link_images_1", 20, 0);
            if (!link_images.HasError)
            {
                if (link_images.Data != null && link_images.Data.count > 0)
                {
                    ViewBag.LinkImages = link_images.Data.list;
                }
            }

            var hover_images = await NewsService.GetAds("yc_casino_hover_images_1", 20, 0);
            if (!hover_images.HasError)
            {
                if (hover_images.Data != null && hover_images.Data.count > 0)
                {
                    ViewBag.HoverImages = hover_images.Data.list;
                }
            }

            var apis = await GameService.GetAllGameApi();
            if (!apis.HasError)
            {
                ViewBag.ApiList = apis.Data;
            }

            var seo = await NewsService.GetSEO("casino");
            if (!seo.HasError)
            {
                ViewBag.SEO = seo.Data;
            }
            return View();
        }

        public async Task<ActionResult> Slot(string platform = "MG")
        {
            ViewBag.Platform = platform;

            var carousel = await NewsService.GetAds("yc_slot_carousel_1", 10, 0);
            if (!carousel.HasError)
            {
                if (carousel.Data != null && carousel.Data.count > 0)
                {
                    ViewBag.Carousel = carousel.Data.list;
                }
            }

            var slot_images = await NewsService.GetAds("yc_slot_images_1", 10, 0);
            if (!slot_images.HasError)
            {
                if (slot_images.Data != null && slot_images.Data.count > 0)
                {
                    ViewBag.SlotImages = slot_images.Data.list;
                }
            }
            
            var categories = await GameService.GetGameCategories("electron");
            if (!categories.HasError)
            {
                ViewBag.Categories = categories.Data;
            }

            var seo = await NewsService.GetSEO("slot");
            if (!seo.HasError)
            {
                ViewBag.SEO = seo.Data;
            }

            var slot_promo = await NewsService.GetNewsList("Electronic_Entertainment", 10, 0);
            if (!slot_promo.HasError)
            {
                ViewBag.SlotPromo = slot_promo.Data;
            }

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("top", ConfigHelper.RecommendedGameLimit.ToString());
            ApiResult<IList<Game>> topresult = await GameService.GetGameTopRecommended(nvc);
            if (!topresult.HasError)
            {
                ViewBag.TopRecommendedGame = topresult.Data;
            }

            return View();
        }

        public async Task<ActionResult> GetJackpotsGames(int? pageIndex, int? pageSize)
        {
            int pIndex = pageIndex.GetValueOrDefault(0);
            int pSize = pageSize.GetValueOrDefault(5);
            var result = await GameService.GetJackpotsGames(pIndex, pSize);
            if (result.HasError)
            {
                return this.JsonError("");
            }
            return this.JsonOK("", result.Data);
        }

        public async Task<ActionResult> PTSlot()
        {
            ViewBag.Platform = "PT";

            var carousel = await NewsService.GetAds("yc_ptslot_carousel_1", 10, 0);
            if (!carousel.HasError)
            {
                if (carousel.Data != null && carousel.Data.count > 0)
                {
                    ViewBag.Carousel = carousel.Data.list;
                }
            }

            var ptslot_images = await NewsService.GetAds("yc_ptslot_images_1", 10, 0);
            if (!ptslot_images.HasError)
            {
                if (ptslot_images.Data != null && ptslot_images.Data.count > 0)
                {
                    ViewBag.PTSlotImages = ptslot_images.Data.list;
                }
            }

            var slot_promo = await NewsService.GetNewsList("Electronic_Entertainment", 10, 0);
            if (!slot_promo.HasError)
            {
                ViewBag.SlotPromo = slot_promo.Data;
            }

            var categories = await GameService.GetGameCategories("pt_slot");
            if (!categories.HasError)
            {
                ViewBag.Categories = categories.Data;
            }

            var seo = await NewsService.GetSEO("slot");
            if (!seo.HasError)
            {
                ViewBag.SEO = seo.Data;
            }

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("top", ConfigHelper.RecommendedGameLimit.ToString());
            ApiResult<IList<Game>> topresult = await GameService.GetGameTopRecommended(nvc);
            if (!topresult.HasError)
            {
                ViewBag.TopRecommendedGame = topresult.Data;
            }

            return View();
        }

        public async Task<ActionResult> MGSlot()
        {
            ViewBag.Platform = "MG";

            var carousel = await NewsService.GetAds("yc_mgslot_carousel_1", 10, 0);
            if (!carousel.HasError)
            {
                if (carousel.Data != null && carousel.Data.count > 0)
                {
                    ViewBag.Carousel = carousel.Data.list;
                }
            }

            var mgslot_images = await NewsService.GetAds("yc_mgslot_images_1", 10, 0);
            if (!mgslot_images.HasError)
            {
                if (mgslot_images.Data != null && mgslot_images.Data.count > 0)
                {
                    ViewBag.MGSlotImages = mgslot_images.Data.list;
                }
            }

            var slot_promo = await NewsService.GetNewsList("Electronic_Entertainment", 10, 0);
            if (!slot_promo.HasError)
            {
                ViewBag.SlotPromo = slot_promo.Data;
            }

            ApiResult<IList<GameCategory>> result = await GameService.GetGameCategories("mg_slot");

            if (!result.HasError)
            {
                ViewBag.Categories = result.Data;
            }

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("top", ConfigHelper.RecommendedGameLimit.ToString());
            ApiResult<IList<Game>> topresult = await GameService.GetGameTopRecommended(nvc);
            if (!topresult.HasError)
            {
                ViewBag.TopRecommendedGame = topresult.Data;
            }

            return View();
        }

        public async Task<ActionResult> BBINSlot()
        {
            ViewBag.Platform = "BBIN";

            var carousel = await NewsService.GetAds("yc_bbinslot_carousel_1", 10, 0);
            if (!carousel.HasError)
            {
                if (carousel.Data != null && carousel.Data.count > 0)
                {
                    ViewBag.Carousel = carousel.Data.list;
                }
            }

            var bbinslot_images = await NewsService.GetAds("yc_bbinslot_images_1", 10, 0);
            if (!bbinslot_images.HasError)
            {
                if (bbinslot_images.Data != null && bbinslot_images.Data.count > 0)
                {
                    ViewBag.BBINSlotImages = bbinslot_images.Data.list;
                }
            }

            var slot_promo = await NewsService.GetNewsList("Electronic_Entertainment", 10, 0);
            if (!slot_promo.HasError)
            {
                ViewBag.SlotPromo = slot_promo.Data;
            }

            ApiResult<IList<GameCategory>> result = await GameService.GetGameCategories("bbin_slot");
            if (!result.HasError)
            {
                ViewBag.Categories = result.Data;
            }

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("top", ConfigHelper.RecommendedGameLimit.ToString());
            ApiResult<IList<Game>> topresult = await GameService.GetGameTopRecommended(nvc);
            if (!topresult.HasError)
            {
                ViewBag.TopRecommendedGame = topresult.Data;
            }

            return View();
        }

        public async Task<ActionResult> AGSlot()
        {
            ViewBag.Platform = "AG";

            var carousel = await NewsService.GetAds("yc_agslot_carousel_1", 10, 0);
            if (!carousel.HasError)
            {
                if (carousel.Data != null && carousel.Data.count > 0)
                {
                    ViewBag.Carousel = carousel.Data.list;
                }
            }

            var agslot_images = await NewsService.GetAds("yc_agslot_images_1", 10, 0);
            if (!agslot_images.HasError)
            {
                if (agslot_images.Data != null && agslot_images.Data.count > 0)
                {
                    ViewBag.AGSlotImages = agslot_images.Data.list;
                }
            }

            var slot_promo = await NewsService.GetNewsList("Electronic_Entertainment", 10, 0);
            if (!slot_promo.HasError)
            {
                ViewBag.SlotPromo = slot_promo.Data;
            }

            ApiResult<IList<GameCategory>> result = await GameService.GetGameCategories("ag_slot");
            if (!result.HasError)
            {
                ViewBag.Categories = result.Data;
            }

            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("top", ConfigHelper.RecommendedGameLimit.ToString());
            ApiResult<IList<Game>> topresult = await GameService.GetGameTopRecommended(nvc);
            if (!topresult.HasError)
            {
                ViewBag.TopRecommendedGame = topresult.Data;
            }

            return View();
        }

        // 获取小游戏列表
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> GetElectrones(String categoryId, int? type, String title, string platform, Int32? pageIndex, Int32? pageSize)
        {
            int pIndex = pageIndex.GetValueOrDefault(0);
            int pSize = pageSize.GetValueOrDefault(24);
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("pageIndex", pIndex.ToString());
            nvc.Add("pageSize", pSize.ToString());
            nvc.Add("categoryId", categoryId);
            nvc.Add("isNew", type.HasValue && type.Value == 2 ? "true" : "");
            nvc.Add("isHot", type.HasValue && type.Value == 1 ? "true" : "");
            nvc.Add("isTry", "");
            nvc.Add("title", title);
            nvc.Add("platform", platform);
            var result = await GameService.GetGameList(nvc);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.Json(result.Data);
        }

        public async Task<ActionResult> Lottery()
        {
            var result = await NewsService.GetAds("yc_lottery_flash_1", 10, 0);
            if (!result.HasError)
            {
                if (result.Data != null && result.Data.count > 0)
                {
                    ViewBag.FlashAds = result.Data.list;
                }
            }
            var images = await NewsService.GetAds("yc_lottery_images_1", 10, 0);
            if (!images.HasError)
            {
                if (images.Data != null && images.Data.count > 0)
                {
                    ViewBag.Images = images.Data.list;
                }
            }
            var seo = await NewsService.GetSEO("lottery");
            if (!seo.HasError)
            {
                ViewBag.SEO = seo.Data;
            }
            return View();
        }


        public ActionResult GetJackpots(String url)
        {
            Decimal amount = 0;

            try
            {
                String xml = UtilHelper.SendHttpRequest(url, null, "get", null, false);
                XElement element = XElement.Parse(xml);

                amount = Convert.ToDecimal(element.Descendants("amount").First().Value);
            }
            catch
            {
                
            }

            return this.JsonOK(amount.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// 获取MG游戏API
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetJackpotsForGamePlatForm(String game, Int32? jackpotInfoType, string gameNameId, string jackpotId, string currency, string extendInfo)
        {
            ApiResult<decimal> gameApi = await GameService.GetJackpots(game, jackpotInfoType, gameNameId, jackpotId, currency, extendInfo);
            if (gameApi.HasError)
            {
                return this.JsonError(gameApi.ErrorMessage);
            }

            return this.JsonOK(gameApi.Data.ToString());
        }
    }
}
