using YHC.Portal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using System.Net;
using System.Net.Sockets;
using System.Web.Script.Serialization;
using System.Text;
using YHC.Portal.Filters;
using YHC.Portal.Models;
using YHC.Portal.Extensions;
using YHC.Portal.Services;
using System.IO;

namespace YHC.Portal.Controllers
{
    public class HomeController : BaseAsyncController
    {
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

        private CommonService _commonService = null;
        public CommonService CommonService
        {
            get
            {
                if (_commonService == null)
                {
                    _commonService = new CommonService();
                }
                return _commonService;
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

        private UserService _userService = null;
        public UserService UserService
        {
            get
            {
                if (_userService == null)
                {
                    _userService = new UserService();
                }
                return _userService;
            }
        }

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

        private ConfigService _configService = null;
        public ConfigService ConfigService
        {
            get
            {
                if (_configService == null)
                {
                    _configService = new ConfigService();
                }
                return _configService;
            }
        }
        public async Task<ActionResult> Index(string a)
        {
            #region 通过推广链接进来

            // 推广码
            if (string.IsNullOrWhiteSpace(a) == false)
            {
                await AccountService.AgentExtension(a);
            }

            string recommendCode = Request["u"];
            // 推荐码
            if (string.IsNullOrWhiteSpace(recommendCode) == false)
            {
                await UserService.Recommend(recommendCode);
            }

            #endregion

            var loginedUser = await UserService.GetLoginedUser();
            if (!loginedUser.HasError)
            {
                ViewBag.LoginedUser = loginedUser.Data;

                String ipAddress = Request.GetClientIP();
                if (ipAddress != null)
                {
                    var gameSportData = await GameService.GetGameCash("Sport", ipAddress.ToString());
                    if (!gameSportData.HasError)
                    {
                        ViewBag.GameSportData = gameSportData.ToString();
                    }

                    var gameAGData = await GameService.GetGameCash("AG", ipAddress.ToString());
                    if (!gameAGData.HasError)
                    {
                        ViewBag.GameAGData = gameAGData.ToString();
                    }

                    var gameABData = await GameService.GetGameCash("AB", ipAddress.ToString());
                    if (!gameABData.HasError)
                    {
                        ViewBag.GameABData = gameABData.ToString();
                    }

                    var gameOGData = await GameService.GetGameCash("OG", ipAddress.ToString());
                    if (!gameOGData.HasError)
                    {
                        ViewBag.GameOGData = gameOGData.ToString();
                    }

                    var gameGDData = await GameService.GetGameCash("GD", ipAddress.ToString());
                    if (!gameGDData.HasError)
                    {
                        ViewBag.GameGDData = gameGDData.ToString();
                    }

                    var gameMGData = await GameService.GetGameCash("MG", ipAddress.ToString());
                    if (!gameMGData.HasError)
                    {
                        ViewBag.GameMGData = gameMGData.ToString();
                    }
                }
            }

            var result = await NewsService.GetAds("yc_index_carousel_1", 10, 0);
            if (!result.HasError)
            {
                ViewBag.Ad = result.Data.list;
            }

            var result2 = await NewsService.GetNewsByKey("index_block_promo");
            if (!result2.HasError)
            {
                ViewBag.Promo = result2.Data;
            }


            var seo = await NewsService.GetSEO("index");
            if (!seo.HasError)
            {
                ViewBag.SEO = seo.Data;
            }

            return View();
        }

        public async Task<ActionResult> Login(string redirectUrl)
        {
            var loginStatus = await AccountService.GetLoginStatus();
            if (loginStatus.HasError)
            {
                return Content("网络错误，请稍后再试...");
            }
            else
            {
                if (loginStatus.Data == 0)
                {
                    return Redirect("/index.html");
                }
            }

            var seo = await NewsService.GetSEO("login");
            if (!seo.HasError)
            {
                ViewBag.SEO = seo.Data;
            }
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> DoLogin(string userName, string password, string authCode)
        {
            if (!string.IsNullOrWhiteSpace(authCode))
            {
                if (authCode.Equals(LoginHelper.GetAuthCode(), StringComparison.InvariantCultureIgnoreCase) == false)
                {
                    return this.JsonError("验证码不正确！");
                }
            }
            string ip = Request.GetClientIP();
            string loginWebSite = Request.GetCurrentFullHost();
            ApiResult<LoginInfo> result = await AccountService.Login(userName, password, ip, loginWebSite);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else if (result.Data == null || string.IsNullOrEmpty(result.Data.Access_Token))
            {
                return this.JsonError("网络错误，请稍后再试");
            }

            return this.JsonOK("登录成功");
        }

        public async Task<ActionResult> Regist()
        {
            var loginStatus = await AccountService.GetLoginStatus();
            if (loginStatus.HasError)
            {
                return Content("网络错误，请稍后再试...");  
            }
            else
            {
                if (loginStatus.Data == 0)
                {
                    return Redirect("/index.html");
                }
            }

            #region 通过推广链接进来

            string agentCode = Request["a"];
            if (string.IsNullOrWhiteSpace(agentCode) == false)
            {
                await AccountService.AgentExtension(agentCode);
            }

            string recommendCode = Request["u"];
            if (string.IsNullOrWhiteSpace(recommendCode) == false)
            {
                await UserService.Recommend(recommendCode);
            }

            #endregion

             //获取推广码
            var extendCode = await UserService.GetRecommendCode();
            if (!extendCode.HasError)
            {
                ViewBag.ExtendCode = extendCode.Data;
            }

            return View();
        }

        public async Task<ActionResult> GetRecommendCode()
        {
            var extendCode = await UserService.GetRecommendCode();
            if (!extendCode.HasError)
            {
                return this.JsonOK("", extendCode.Data);
            }
            return this.JsonOK("", "");
        }

        public async Task<ActionResult> DoRegist(string extendCode, string userName, string trueName, string password, string phone, string email,
                                     string source, string qq, DateTime? birthday, bool isReceiveSMS,
                                     bool isReceiveEmail, string authCode, string withdrawalPassword)
        {
            #region 基本验证

            if (!string.IsNullOrWhiteSpace(authCode) && authCode.Equals(LoginHelper.GetAuthCode(), StringComparison.InvariantCultureIgnoreCase) == false)
            {
                return this.JsonError("验证码不正确！");
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                return this.JsonError("账户名称不能为空");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return this.JsonError("登录密码不能为空");
            }

            #endregion

            String ip = Request.GetClientIP();
            String regWebSite = Request.GetCurrentFullHost();
            source = regWebSite;

            ApiResult<LoginInfo> result = await AccountService.Regist(extendCode, userName, trueName, password, phone, email, source, qq, birthday, isReceiveSMS, isReceiveEmail, null, withdrawalPassword, ip, regWebSite);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.JsonOK("注册成功");
            }
        }

        public async Task<ActionResult> Logout()
        {
            ApiResult<bool> result = await AccountService.Logout();
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.JsonOK("注销成功");
            }
        }

        public async Task<ActionResult> ForgetPwd()
        {
            var result = await NewsService.GetAds("help_banner_1", 1, 0);
            if (!result.HasError)
            {
                if (result.Data != null && result.Data.count > 0)
                {
                    ViewBag.TopAd = result.Data.list;
                }
            }
            return View();
        }

        public async Task<ActionResult> DoForgetPwd(String userName, String authCode)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return this.JsonError("用户名不能为空！");
            }
            if (string.IsNullOrWhiteSpace(authCode))
            {
                return this.JsonError("验证码不能为空！");
            }

            if (authCode.Equals(LoginHelper.GetAuthCode(), StringComparison.InvariantCultureIgnoreCase) == false)
            {
                return this.JsonError("验证码不正确！");
            }

            var result = await UserService.CheckUserName(userName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            if (!result.Data)
            {
                return this.JsonError("用户名不存在！");
            }

            return this.JsonOK();
        }

        public async Task<ActionResult> ForgetPwd2(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName))
            {
                return Content("用户名不能为空！");
            }

            var result = await UserService.GetByUserName(userName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            ViewBag.User = result.Data;

            var result2 = await NewsService.GetAds("help_banner_1", 1, 0);
            if (!result2.HasError)
            {
                if (result2.Data != null && result2.Data.count > 0)
                {
                    ViewBag.TopAd = result2.Data.list;
                }
            }
            return View();
        }

        /// <summary>
        /// 获取忘记密码的验证码
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<ActionResult> DoGetForgetPwdValidateCode(String userName, String authCode, String type)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return this.JsonError("用户名不能为空！");
            }

            var result = await UserService.GetForgetPwdValidateCode(userName, type, Request.GetClientIP());
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            ViewBag.Type = type;

            if (!result.Data)
            {
                return this.JsonError("网络故障，请稍后再试！");
            }

            return this.JsonOK();
        }


        public async Task<ActionResult> ForgetPwdModifyPwd(String userName, String type)
        {
            if (String.IsNullOrWhiteSpace(userName))
            {
                return Content("用户名不能为空！");
            }
            if (String.IsNullOrWhiteSpace(type))
            {
                return Content("验证方式不能为空！");
            }
            var result = await UserService.GetByUserName(userName);
            if (!result.HasError)
            {
                ViewBag.User = result.Data;
            }
            ViewBag.Type = type;

            return View();
        }

        public async Task<ActionResult> DoForgetPwdModifyPwd(string userName, string newPassword, string confirmPassword, string emailValidateCode, string phoneValidateCode, string type)
        {
            var result = await UserService.ChangePasswordByForget(userName, newPassword, confirmPassword, emailValidateCode, phoneValidateCode, type);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            if (!result.Data)
            {
                return this.JsonError("网络故障，请稍后再试！");
            }
            return this.JsonOK();
        }

        public async Task<ActionResult> Download()
        {
            var result = await NewsService.GetNewsByKey("download");
            if (!result.HasError)
            {
                ViewBag.News = result.Data;
            }
            return View();
        }

        public async Task<ActionResult> Signal()
        {
            var result = await ConfigService.GetSpareDomain();
            if (!result.HasError)
            {
                ViewBag.SpareDomain = result.Data;
            }
            return View();
        }

        public async Task<ActionResult> Maintenance(string type, string game)
        {
            string msg = string.Empty;
            if (string.IsNullOrWhiteSpace(type) == false && type == "api"
                && string.IsNullOrWhiteSpace(game) == false)
            {
                var result = await GameService.GetGameApi(game);
                if (!result.HasError && result.Data != null)
                {
                    msg = result.Data.MaintainMessage;
                }
            }

            ViewBag.Message = msg;

            return View();
        }
        public async Task<ActionResult> RegistSuccess()
        {
            ViewBag.Page = "RegistrationSuccess";
            return View();
        }
        public async Task<ActionResult> Mobile()
        {
            ViewBag.Page = "Mobile";

            var downloadImages = await NewsService.GetAds("yc_download_images_1", 10, 0);
            if (!downloadImages.HasError)
            {
                if (downloadImages.Data != null && downloadImages.Data.count > 0)
                {
                    ViewBag.Images = downloadImages.Data.list;
                }
            }

            var mobileDownloadList = await NewsService.GetAds("yc_download_mobile_1", 10, 0);
            if (!mobileDownloadList.HasError)
            {
                if (mobileDownloadList.Data != null && mobileDownloadList.Data.count > 0)
                {
                    ViewBag.MobileDownloadList = mobileDownloadList.Data.list;
                }
            }

            var computerDownloadList = await NewsService.GetAds("yc_download_computer_1", 10, 0);
            if (!computerDownloadList.HasError)
            {
                if (computerDownloadList.Data != null && computerDownloadList.Data.count > 0)
                {
                    ViewBag.ComputerDownloadList = computerDownloadList.Data.list;
                }
            }

            return View();
        }

    }
}
