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
    public class AgentController : Controller
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

        //
        // GET: /Agent/
        public ActionResult Index()
        {
            //return Redirect(ConfigHelper.AgentSiteDomain);
            return View();
        }

        public async Task<ActionResult> Regist()
        {
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
        public async Task<ActionResult> Program()
        {

            var result = await NewsService.GetNewsByKey("yc_program_1");
            if (!result.HasError)
            {
                ViewBag.Program = result.Data;
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
        public async Task<ActionResult> Protocol()
        {
            var result = await NewsService.GetNewsByKey("yc_protocol_1");
            if (!result.HasError)
            {
                ViewBag.Protocol = result.Data;
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
        public async Task<ActionResult> Brand()
        {
            var result = await NewsService.GetNewsByKey("yc_brand_1");
            if (!result.HasError)
            {
                ViewBag.Brand = result.Data;
            }
            return View();
        }
        public async Task<ActionResult> Agreement()
        {
            var result = await NewsService.GetNewsByKey("yc_argreement_1");
            if (!result.HasError)
            {
                ViewBag.Agreement = result.Data;
            }
            return View();
        }
        public async Task<ActionResult> DoRegist(string userName, string trueName, string password, string phone, string email,
                                     string source, string qq, string domain, string extendDesc, string authCode)
        {
            if (!string.IsNullOrEmpty(authCode))
            {
                string saveAuthCode = LoginHelper.GetAuthCode();
                if (saveAuthCode.Equals(authCode.Trim(), StringComparison.InvariantCultureIgnoreCase) == false)
                {
                    return this.JsonError("验证码不正确");
                }
            }

            #region 验证

            if (string.IsNullOrWhiteSpace(userName))
            {
                return this.JsonError("用户名不能为空");
            }

            if (string.IsNullOrWhiteSpace(trueName))
            {
                return this.JsonError("真实姓名不能为空");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return this.JsonError("登录密码不能为空");
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                return this.JsonError("手机号码不能为空");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return this.JsonError("电子邮件不能为空");
            }

            if (string.IsNullOrWhiteSpace(domain) && string.IsNullOrWhiteSpace(extendDesc))
            {
                return this.JsonError("网址和推广说明至少填写一项");
            }

            #endregion

            String ip = Request.GetClientIP();

            var result = await AccountService.RegistAgent(userName, trueName, password, phone, email, qq, null, domain, extendDesc, ip);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("");
        }
	}
}