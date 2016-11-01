using YHC.Portal.Filters;
using YHC.Portal.Helper;
using YHC.Portal.Models;
using YHC.Portal.Services;
using YHC.Portal.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YHC.Portal.Core;
using System.Text.RegularExpressions;

namespace YHC.Portal.Controllers
{
    public class UserController : BaseAsyncController
    {
        #region Init

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

        private BetService _betService = null;
        public BetService BetService
        {
            get
            {
                if (_betService == null)
                {
                    _betService = new BetService();
                }
                return _betService;
            }
        }

        private DepositService _depositService = null;
        public DepositService DepositService
        {
            get
            {
                if (_depositService == null)
                {
                    _depositService = new DepositService();
                }
                return _depositService;
            }
        }

        private WithdrawalService _withdrawalService = null;
        public WithdrawalService WithdrawalService
        {
            get
            {
                if (_withdrawalService == null)
                {
                    _withdrawalService = new WithdrawalService();
                }
                return _withdrawalService;
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

        private TransferService _transferService = null;
        public TransferService TransferService
        {
            get
            {
                if (_transferService == null)
                {
                    _transferService = new TransferService();
                }
                return _transferService;
            }
        }

        private PromoService _promoService = null;
        public PromoService PromoService
        {
            get
            {
                if (_promoService == null)
                {
                    _promoService = new PromoService();
                }
                return _promoService;
            }
        }

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

        #endregion

        #region 通用（获取头部，底部，登录信息，在线客服，公告）

        [HttpPost, CheckLogin]
        public async Task<ActionResult> GetHeader()
        {
            var result = await ConfigService.GetNewsContent("user_block_header");
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("", result.Data);
        }

        [HttpPost, CheckLogin]
        public async Task<ActionResult> GetFooter()
        {
            var result = await ConfigService.GetNewsContent("user_block_footer");
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("", result.Data);
        }

        [HttpPost, CheckLogin]
        public async Task<ActionResult> GetLiveChat()
        {
            var result = await ConfigService.GetNewsContent("user_block_livechat");

            return this.JsonOK("", result.Data);
        }

        // 公告
        [HttpPost, CheckLogin]
        public async Task<ActionResult> GetNews()
        {
            var result = await NewsService.GetNewsList("notice", 20, 0);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("", result.Data.list);
        }

        // 登录信息
        [CheckLogin, HttpPost]
        public ActionResult GetLoginInfo()
        {
            String userName = LoginHelper.GetUser().UserName;

            return this.JsonOK("", userName);
        }

        #endregion

        #region 会员首页

        public ActionResult Relogin(string redirectUrl)
        {
            return Redirect("/index.html");
        }

        [CheckLogin]
        public async Task<ActionResult> Index()
        {
            User user = LoginHelper.GetUser();

            Int32 userBankCount = await UserService.GetUserBankCount(user.UserName);

            ViewBag.Config = ConfigService.Get();
            ViewBag.BankBindStatus = (userBankCount > 0); // 银行绑定状态，大于0表示已绑定
            ViewBag.User = user;
            return View();
        }

        [CheckLogin]
        public async Task<ActionResult> GetUserExtendInfo()
        {
            //int recordCount;
            //VUser user = LoginHelper.GetUser();

            // 周日开始
            //DateTime lastWeekStartTime = Convert.ToDateTime(DateTime.Now.AddDays(Convert.ToDouble((0 - Convert.ToInt16(DateTime.Now.DayOfWeek))) - 7).ToShortDateString());
            //DateTime lastWeekEndTime =  lastWeekStartTime.AddDays(6);
            //改为周一开始
            DateTime theWeekStartTime = DateTime.Now.AddDays(1 - Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d")));// lastWeekStartTime.AddDays(7);
            DateTime theWeekEndTime = theWeekStartTime.AddDays(6);
            DateTime lastWeekStartTime = theWeekStartTime.AddDays(-7);
            DateTime lastWeekEndTime = theWeekStartTime.AddDays(-1);

            int userId = LoginHelper.GetUser().Id;
            VBet lastGame = await BetService.GetLastBet();

            var resultLast = await BetService.GetTotalBet(userId, lastWeekStartTime, lastWeekEndTime, string.Empty);
            if (resultLast.HasError)
            {
                return this.JsonError(resultLast.ErrorMessage);
            }
            var resultThis = await BetService.GetTotalBet(userId, theWeekStartTime, theWeekEndTime, string.Empty);
            if (resultThis.HasError)
            {
                return this.JsonError(resultThis.ErrorMessage);
            }
            VBet lastWeekSum = resultLast.Data;
            VBet theWeekSum = resultThis.Data;

            String lastWeekRealBet = "0"; // 上周有效投注
            String theWeekRealBet = "0"; // 本周有效投注

            String lastLoginGameName = "---"; // 上次登录游戏名称
            String lastLoginGameTime = "---"; // 上次登录游戏时间
            String lastLoginGamePlatform = "---"; // 上次登录游戏平台
            String lastLoginGameType = "---"; // 上次登录游戏类型
            String lastLoginGameId = "---"; // 上次登录游戏Id

            String lastDepositTime = "---"; // 最后一次存款时间
            String lastWithdrawalTime = "---"; // 最后一次提款时间

            if (lastGame != null && !string.IsNullOrEmpty(lastGame.GamePlatform))
            {
                lastLoginGameName = string.Format("{0}：{1}", lastGame.GamePlatform, lastGame.GamePlatform);
                lastLoginGameTime = lastGame.UpdateTime.ToString("yyyy-MM-dd HH:mm");
                lastLoginGamePlatform = lastGame.GamePlatform;
                lastLoginGameType = UtilHelper.GetBetTypeText_EN(lastGame.Type);
                lastLoginGameId = lastGame.GameName;
            }
            if (lastWeekSum != null)
            {
                lastWeekRealBet = lastWeekSum.RealBet.ToStringN0();
            }
            if (theWeekSum != null)
            {
                theWeekRealBet = theWeekSum.RealBet.ToStringN0();
            }

            #region 最后一次存款和提款时间

            var deposites = await DepositService.GetLastDeposit();
            if (deposites != null)
            {
                lastDepositTime = deposites.CreateTime.ToString("yyyy-MM-dd HH:mm");
            }

            var withdrawales = await WithdrawalService.GetLastWithdrawal();
            if (withdrawales != null)
            {
                lastWithdrawalTime = withdrawales.CreateTime.ToString("yyyy-MM-dd HH:mm");
            }

            #endregion

            var result = new
            {
                LastWeekRealBet = lastWeekRealBet,
                TheWeekRealBet = theWeekRealBet,
                LastLoginGameName = lastLoginGameName,
                LastLoginGameTime = lastLoginGameTime,
                LastLoginGamePlatform = lastLoginGamePlatform,
                LastLoginGameType = lastLoginGameType,
                LastLoginGameId = lastLoginGameId,

                LastDepositTime = lastDepositTime,
                LastWithdrawalTime = lastWithdrawalTime,
            };

            return this.JsonOK("", result);
        }

        #endregion

        #region 获取用户的本地账户金额和游戏账户金额 优惠钱包金额

        [CheckLogin]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> GetUserCash()
        {
            String userName = LoginHelper.GetUser().UserName;
            ApiResult<decimal> result = await UserService.GetUserCash(userName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.JsonOK("",result.Data.ToStringN2());
            }
        }

        // 获取单个游戏余额
        [CheckLogin, HttpPost]
        public async Task<ActionResult> GetGameCash(string game)
        {
            String ip = Request.GetClientIP();
            ApiResult<decimal> result = await UserService.GetGameCash(game,ip);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.JsonOK("", result.Data);
            }
        }

        // 获取游戏余额
        [CheckLogin, HttpPost]
        public async Task<ActionResult> GetAllGameCash()
        {
            var userName = LoginHelper.GetUser().UserName;

            ApiResult<decimal> result = await UserService.GetAllGameCash(userName);

            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.JsonOK("", result.Data);
            }
        }


        // 获取优惠钱包余额
        [CheckLogin, HttpPost]
        public async Task<ActionResult> GetWalletCash()
        {
            String userName = LoginHelper.GetUser().UserName;
            ApiResult<decimal> result = await UserService.GetWalletCash(userName,1);//status=1  申请状态
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.JsonOK("", result.Data);
            }
        }

        #endregion

        #region Email验证

        [CheckLogin]
        public ActionResult BindEmail()
        {
            // 已验证情况下，跳转到提示页
            var user = LoginHelper.GetUser();
            if (user.EmailValidateStatus)
            {
                return Redirect("/user/bindemailsuccess.html?validate=true");
            }

            ViewBag.User = user;

            return View();
        }

        [CheckLogin]
        public ActionResult BindEmailStep2()
        {
            // 已验证情况下，跳转到提示页
            var user = LoginHelper.GetUser();
            if (user.EmailValidateStatus)
            {
                return Redirect("/user/bindemailsuccess.html?validate=true");
            }

            ViewBag.User = user;

            return View();
        }

        //验证Email
        [CheckLogin]
        public async Task<ActionResult> BindEmailSuccess(Int32? id, string email, string emailValidateCode, Boolean? validate)
        {
            String status = "验证成功";
            String details = "恭喜您，邮箱验证成功";

            if (validate.HasValue && validate.Value)
            {
                status = "验证成功";
                details = "您的邮箱已验证";
            }
            else if (string.IsNullOrWhiteSpace(emailValidateCode) || string.IsNullOrWhiteSpace(email) || id.HasValue == false)
            {
                status = "验证失败";
                details = "邮箱验证码错误";
            }
            else
            {
                var result = await UserService.ValidateEmail(id.Value, email, emailValidateCode);
                if (result.HasError)
                {
                    status = "验证失败";
                    details = result.ErrorMessage;
                }
            }

            ViewBag.Status = status;
            ViewBag.Details = details;

            return View();
        }

        /// <summary>
        /// 申请email验证码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [CheckLogin, HttpPost]
        public async Task<ActionResult> SendEmailValidateCode(string email)
        {
            var user = LoginHelper.GetUser();

            String siteUrl = GetSiteUrl();
            string ip = Request.GetClientIP();
            string contactUrl = string.Format("{0}help/contact.html", siteUrl);
            string validateUrl = string.Format("{0}user/bindemailsuccess.html", siteUrl);
            // 发送邮件验证码
            var result = await UserService.SendEmailValidateCode(user.Id, email, validateUrl, contactUrl,ip);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK();
        }

        private string GetSiteUrl()
        {
            HttpRequestWrapper httpRequestWrapper = (new HttpRequestWrapper(System.Web.HttpContext.Current.Request));
            if (httpRequestWrapper.Url != null)
            {
                return string.Format("{0}://{1}/", httpRequestWrapper.Url.Scheme, httpRequestWrapper.GetCurrentFullHost());
            }
            return string.Empty;
        }

        #endregion

        #region 手机验证

        [CheckLogin]
        public ActionResult BindMobile()
        {
            // 已验证情况下，跳转到提示页
            var user = LoginHelper.GetUser();
            if (user.PhoneValidateStatus)
            {
                return Redirect("/user/bindmobilesuccess.html");
            }

            ViewBag.User = user;

            return View();
        }

        [CheckLogin]
        public ActionResult BindMobileStep2()
        {
            // 已验证情况下，跳转到提示页
            var user = LoginHelper.GetUser();
            if (user.PhoneValidateStatus)
            {
                return Redirect("/user/bindmobilesuccess.html");
            }

            ViewBag.User = user;

            return View();
        }

        /// <summary>
        /// 申请phone验证码
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        [HttpPost]
        public async Task<ActionResult> SendMobileValidateCode(string mobile, string authCode)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return this.JsonError("手机号不能为空");
            }

            if (string.IsNullOrWhiteSpace(authCode) == false && authCode.Equals(LoginHelper.GetAuthCode(), StringComparison.InvariantCultureIgnoreCase) == false)
            {
                return this.JsonError("验证码不正确！");
            }
            var user = LoginHelper.GetUser();
            string ip = Request.GetClientIP();
            var result = await UserService.SendMobileValidateCode(user.UserName, mobile, ip);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK();
        }

        [CheckLogin, HttpPost]
        public async Task<ActionResult> DoBindMobile(string mobile, string validateCode)
        {
            if (string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(validateCode))
            {
                return this.JsonError("手机验证码错误");
            }

            Int32 userId = LoginHelper.GetUser().Id;

            var rst = await UserService.ValidatePhone(userId, mobile, validateCode);

            if (rst.HasError)
            {
                return this.JsonError(rst.ErrorMessage);
            }
            return this.JsonOK("");
        }

        [CheckLogin]
        public ActionResult BindMobileSuccess()
        {
            String status = "验证成功";
            String details = "恭喜您，手机验证成功";

            ViewBag.Status = status;
            ViewBag.Details = details;

            return View();
        }

        #endregion

        #region 存款

        [CheckLogin]
        public async Task<ActionResult> DepositOnline(Int32? id)
        {
            // 传入支付平台id
            var user = LoginHelper.GetUser();
            var result = await UserService.GetFirstDepositPromoList(user.UserName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            var piesResult = await UserService.GetUserThirdPay();
            if (piesResult.HasError)
            {
                return this.JsonError(piesResult.ErrorMessage);
            }

            // 支付宝
            var bankResult_AliPay = await UserService.GetUserAdminBank(2);
            if (bankResult_AliPay.HasError)
            {
                return this.JsonError(bankResult_AliPay.ErrorMessage);
            }
            // 微信
            var bankResult_WebChat = await UserService.GetUserAdminBank(0);
            if (bankResult_WebChat.HasError)
            {
                return this.JsonError(bankResult_WebChat.ErrorMessage);
            }
            ViewBag.WebChat = bankResult_WebChat.Data;
            ViewBag.AliPay = bankResult_AliPay.Data;

            ViewBag.CurrThirdPayId = id.GetValueOrDefault(0);
            ViewBag.User = user;
            ViewBag.Pies = piesResult.Data;
            ViewBag.PrefInfos = result.Data;

            return View();
        }

        [CheckLogin]
        public async Task<ActionResult> Deposit()
        {
            var user = LoginHelper.GetUser();
            var result = await UserService.GetFirstDepositPromoList(user.UserName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            var piesResult = await UserService.GetUserThirdPay();
            if (piesResult.HasError)
            {
                return this.JsonError(piesResult.ErrorMessage);
            }

            var bankResult = await UserService.GetUserAdminBank();
            if (bankResult.HasError)
            {
                return this.JsonError(bankResult.ErrorMessage);
            }

            var provinceResult = await ConfigService.GetProvinces();
            if (result.HasError)
            {
                return this.JsonError(provinceResult.ErrorMessage);
            }

            // 支付宝
            var bankResult_AliPay = await UserService.GetUserAdminBank(2);
            if (bankResult_AliPay.HasError)
            {
                return this.JsonError(bankResult_AliPay.ErrorMessage);
            }
            // 微信
            var bankResult_WebChat = await UserService.GetUserAdminBank(0);
            if (bankResult_WebChat.HasError)
            {
                return this.JsonError(bankResult_WebChat.ErrorMessage);
            }
            ViewBag.WebChat = bankResult_WebChat.Data;
            ViewBag.AliPay = bankResult_AliPay.Data;

            ViewBag.User = user;
            ViewBag.Pies = piesResult.Data;
            ViewBag.Bank = bankResult.Data;
            ViewBag.PrefInfos = result.Data;
            ViewBag.Provinces = provinceResult.Data;

            return View();
        }

        [CheckLogin]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> DoDeposit(int? bank, decimal? amount, int? transType, string province, string city, string address, string prefKey, string accountName)
        {
            if (bank.HasValue == false)
            {
                return this.JsonError("请选择收款银行");
            }
            if (amount.HasValue == false)
            {
                return this.JsonError("请选择存款金额");
            }

            var user = LoginHelper.GetUser();
            //string orderNo = IdCreatorHelper.GetOrderNo();
            string ip = Request.GetClientIP();
            string website = Request.GetCurrentFullHost();

            var result = await DepositService.AddDeposit(user.UserName, user.TrueName, bank.Value, amount.Value, transType, (int)UserDepositType.BankTransfer, province, city, address, ip, prefKey, accountName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.JsonOK("提交成功！", result.Data);
            }
        }

        public ActionResult DepositList()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CheckLogin]
        public async Task<ActionResult> GetDepositList(DateTime? beginTime, DateTime? endTime, Int32? status, Int32? type, int? pageIndex, int? pageSize)
        {
            var result = await DepositService.GetDepositList(beginTime, endTime, status, type, pageIndex, pageSize);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            result.Data.extend = new { TotalPageAmount = result.Data.list.Sum(p => p.Amount) };
            return this.Json(result.Data);
            // 小计
            //totalPageAmount = deposites.Sum(p => p.Amount);

            //return this.Json(new PagerFrontViewModel<VUserDeposit>(
            //    recordCount,
            //    new { TotalAmount = totalAmount, TotalPageAmount = totalPageAmount },
            //    deposites));
        }

        public async Task<ActionResult> GetTotalDeposit(DateTime? beginTime, DateTime? endTime, Int32? status, Int32? type)
        {
            var user = LoginHelper.GetUser();

            var result = await DepositService.GetTotalDeposit(user.UserName, beginTime, endTime, status,type);

            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            return this.Json(result.Data);
        }

        [CheckLogin]
        public async Task<ActionResult> DepositAlipay()
        {
            var user = LoginHelper.GetUser();

            var result = await UserService.GetFirstDepositPromoList(user.UserName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            var piesResult = await UserService.GetUserThirdPay();
            if (piesResult.HasError)
            {
                return this.JsonError(piesResult.ErrorMessage);
            }

            // 支付宝
            var bankResult_AliPay = await UserService.GetUserAdminBank(2);
            if (bankResult_AliPay.HasError)
            {
                return this.JsonError(bankResult_AliPay.ErrorMessage);
            }
            // 微信
            var bankResult_WebChat = await UserService.GetUserAdminBank(0);
            if (bankResult_WebChat.HasError)
            {
                return this.JsonError(bankResult_WebChat.ErrorMessage);
            }
            ViewBag.WebChat = bankResult_WebChat.Data;
            ViewBag.AliPay = bankResult_AliPay.Data;

            ViewBag.Pies = piesResult.Data;
            ViewBag.User = user;
            ViewBag.PrefInfos = result.Data;

            return View();
        }

        [CheckLogin]
        public async Task<ActionResult> DepositWechat()
        {
            var user = LoginHelper.GetUser();
            var result = await UserService.GetFirstDepositPromoList(user.UserName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            var piesResult = await UserService.GetUserThirdPay();
            if (piesResult.HasError)
            {
                return this.JsonError(piesResult.ErrorMessage);
            }

            // 支付宝
            var bankResult_AliPay = await UserService.GetUserAdminBank(2);
            if (bankResult_AliPay.HasError)
            {
                return this.JsonError(bankResult_AliPay.ErrorMessage);
            }
            // 微信
            var bankResult_WebChat = await UserService.GetUserAdminBank(0);
            if (bankResult_WebChat.HasError)
            {
                return this.JsonError(bankResult_WebChat.ErrorMessage);
            }
            ViewBag.WebChat = bankResult_WebChat.Data;
            ViewBag.AliPay = bankResult_AliPay.Data;

            ViewBag.Pies = piesResult.Data;
            ViewBag.User = user;
            ViewBag.PrefInfos = result.Data;

            return View();
        }

        #endregion

        #region 提款

        [CheckLogin]
        public async Task<ActionResult> Withdrawal()
        {
            var config = ConfigService.Get();
            var user = LoginHelper.GetUser();
            var result = await UserService.GetUserBankList(user.UserName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            ViewBag.User = user;
            ViewBag.Bank = result.Data;
            ViewBag.Config = ConfigService.Get();
            return View();
        }

        [CheckLogin]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> DoWithdrawal(int? bankAcconutId, int? amount, bool? confirmSave, String withdrawlPwd)
        {
            if (bankAcconutId.HasValue == false)
            {
                return this.JsonError("请选择提款银行卡");
            }
            if (amount.HasValue == false)
            {
                return this.JsonError("请输入提款金额，并且只能为整数");
            }

            Decimal actAmount = Convert.ToDecimal(amount.Value);
            var user = LoginHelper.GetUser();
            string ip = Request.GetClientIP();

            var inspectResult = await WithdrawalService.GetNotInspectes(user.UserName, DateTime.Now);
            if (inspectResult.HasError)
            {
                return this.JsonError(inspectResult.ErrorMessage);
            }
            InspectWithdrawal inspectes = inspectResult.Data;

            //IList<InspectWithdrawal> inspectes;
            //IList<VUserWallet> wallets = null;
            //String msg;
            //Boolean error = false;
            //decimal transferFee = 0;

            //Boolean allowWithdrawal = this._checkInspectWithdrawal(user.UserName, out inspectes, out error, out msg, out transferFee);
            //if (error)
            //{
            //    return this.JsonError(msg);
            //}

            // 如果通过稽核了，则直接提款。还有就是用户确认存款（已弹出提示层的时候点确定）
            decimal mgrFee = inspectes.InspectList.Sum(p => p.NormalityInspectFee);
            decimal prefFee = inspectes.InspectList.Sum(p => p.PrefInspectFee);
            decimal totalFee = mgrFee + prefFee;
            if (totalFee <= 0 || confirmSave.GetValueOrDefault(false))
            {
                // 添加提款
                var withdrawalResult = await WithdrawalService.AddWithdrawal(user.UserName, bankAcconutId.Value, actAmount, "会员提款", ip, mgrFee, prefFee, withdrawlPwd);
                if (withdrawalResult.HasError)
                {
                    return this.JsonError(withdrawalResult.ErrorMessage);
                }
                return this.JsonOK("提交成功，请等待网站审核！");
            }

            // 锁定中的优惠（已申请，未发放或未过期）
            var walletResult = await PromoService.GetWalletLockCount(user.Id);
            if (walletResult.HasError)
            {
                return this.JsonError(walletResult.ErrorMessage);
            }

            return this.JsonOK("needInspect", new
            {
                AllowWithdrawal = inspectes.AllowWithdrawal,
                Inspectes = inspectes.InspectList,
                TransferFee = inspectes.TransFee,
                //Wallets = wallets,
                WalletsCount = walletResult.Data
            });
        }

        //bool _checkInspectWithdrawal(String userName, out IList<InspectWithdrawal> inspectes, out Boolean error, out String msg, out Decimal transferFee)
        //{
        //    // 提款稽核相关
        //    Boolean allowWithdrawal = true; // 稽核不通过，是否允许提款
        //    inspectes = new List<InspectWithdrawal>();
        //    error = false;
        //    transferFee = 0;
        //    DateTime endInspectTime = ConfigTime.Now(); // 提款时，稽核时间为最新时间

        //    try
        //    {
        //        inspectes = _inspectWithdrawalService.GetNotInspectes(userName, endInspectTime,out allowWithdrawal, out msg, out transferFee);
        //    }
        //    catch (CustomServiceException ex)
        //    {
        //        String errorMsg = ex.Message;
        //        error = true;

        //        // 处理友好信息，API的错误，例如：验证信息失败(AVIA)
        //        // 取出游戏平台名称
        //        String platform = "";
        //        String pattern = @"\([A-Z]+\)";
        //        Match mc1 = Regex.Match(errorMsg, pattern, RegexOptions.IgnoreCase);
        //        if (mc1.Success)
        //        {
        //            String t = mc1.Value;
        //            pattern = @"[A-Z]+";
        //            Match mc2 = Regex.Match(t, pattern, RegexOptions.IgnoreCase);
        //            if (mc2.Success)
        //            {
        //                platform = mc2.Value;
        //            }
        //        }

        //        msg = "正在同步{0}游戏数据，请稍候重试或联系客服人员。".FormatWith(platform);

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        error = true;
        //        msg = "抱谦，提款失败，请联系客服人员";
        //        return false;
        //    }

        //    return allowWithdrawal;
        //}

        [CheckLogin]
        public ActionResult WithdrawalPassword()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> GetBanks()
        {
            // 获取对应省份的城市列表
            var result = await ConfigService.GetBankList();
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.JsonOK("", result.Data);
            }
        }

        // 获取省份
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> GetProvinces()
        {
            // 获取对应省份的城市列表
            var result = await ConfigService.GetProvinces();
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.JsonOK("", result.Data);
            }
        }

        // 根据省份获取城市列表
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> GetCities(Int32? provinceId)
        {
            if (provinceId.HasValue == false)
            {
                return this.JsonError("省份ID不能为空");
            }
            // 获取对应省份的城市列表
            var result = await ConfigService.GetCites(provinceId.Value);

            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.JsonOK("", result.Data);
            }
        }

        [CheckLogin]
        public ActionResult WithdrawalList()
        {
            return View();
        }

        [CheckLogin, AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> GetWithdrawalList(DateTime? beginTime, DateTime? endTime, Int32? status, Int32? pageIndex, Int32? pageSize)
        {
            var user = LoginHelper.GetUser();

            var result = await WithdrawalService.GetWithdrawalList(user.UserName, beginTime, endTime,status,pageIndex,pageSize);

            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            result.Data.extend = new { TotalPageAmount = result.Data.list.Sum(p => p.Amount) };
            return this.Json(result.Data);
        }

        public async Task<ActionResult> GetTotalWithdrawal(DateTime? beginTime, DateTime? endTime, Int32? status)
        {
            var user = LoginHelper.GetUser();

            var result = await WithdrawalService.GetTotalWithdrawal(user.UserName, beginTime, endTime, status);

            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            return this.Json(result.Data);
        }

        #endregion

        #region  绑定银行卡

        // 获取会员已绑定的银行卡
        [CheckLogin, HttpPost]
        public async Task<ActionResult> GetBindBanks()
        {
            String userName = LoginHelper.GetUser().UserName;
            var result = await UserService.GetUserBankList(userName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("", result.Data);
        }

        [CheckLogin]
        public async Task<ActionResult> DoSaveBankCard(int bankId, string province, string city, string branchName, string accountNo, string accountName, String withdrawalPwd)
        {
            string userName = LoginHelper.GetUser().UserName;
            var result = await WithdrawalService.AddUserBank(userName, bankId, province, city, branchName, accountName, accountNo,withdrawalPwd);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.JsonOK("银行卡添加成功！");
            }
        }

        #endregion

        #region 转账

        [CheckLogin]
        public async Task<ActionResult> Transfer()
        {
            var result = await GameService.GetAllGameApi();
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            ViewBag.User = LoginHelper.GetUser();
            ViewBag.Apies = result.Data;
            return View();
        }

        [CheckLogin]
        [HttpPost]
        public async Task<ActionResult> DoTransfer(string from, string to, decimal amount, Boolean? tryPlay)
        {
            if (string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(from))
            {
                return this.JsonError("请选择转入或转出平台");
            }

            if (from == to)
            {
                return this.JsonError("同账户不允许互转");
            }
            if (from != "0" && to != "0")
            {
                return this.JsonError("游戏平台账户不允许互转");
            }

            var user = LoginHelper.GetUser();
            string ip = Request.GetClientIP();
            Boolean isDemo = tryPlay.GetValueOrDefault(false); // 是否试玩，默认为正式

            ApiResult<bool> result;
            if (from == "0")
            {
                // 转到游戏平台
                result = await TransferService.ToGame(user.UserName, amount, to, ip, isDemo);
            }
            else
            {
                // 转到本地主账户
                result = await TransferService.FromGame(user.UserName, amount, from, ip, isDemo);
            }
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("提交成功！");
        }

        [CheckLogin]
        public async Task<ActionResult> TransferList()
        {
            // 游戏平台信息
            var result = await GameService.GetAllGameApi();
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            ViewBag.Games = result.Data;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CheckLogin]
        public async Task<ActionResult> GetTransferList(DateTime? beginTime, DateTime? endTime, String gamePlatform, Int32? type, Int32? status, int? pageIndex, int? pageSize)
        {
            var user = LoginHelper.GetUser();

            var result = await TransferService.GetTransferList(user.UserName, beginTime, endTime, type, status, gamePlatform, pageIndex, pageSize);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            result.Data.extend = new { TotalPageAmount = result.Data.list.Sum(p => p.Amount) };
            return this.Json(result.Data);

            //// 小计
            //totalPageAmount = list.Sum(p => p.Amount);

            //return this.Json(new PagerFrontViewModel<VUserTransfer>(
            //    recordCount,
            //    new { TotalAmount = totalAmount, TotalPageAmount = totalPageAmount },
            //    list));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CheckLogin]
        public async Task<ActionResult> GetTotalTransfer(DateTime? beginTime, DateTime? endTime, String gamePlatform, Int32? type, Int32? status, int? pageIndex, int? pageSize)
        {
            var user = LoginHelper.GetUser();

            var result = await TransferService.GetTotalTransfer(user.UserName, beginTime, endTime, type, status, gamePlatform);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            return this.Json(result.Data);
        }

        #endregion

        #region 站内信

        public async Task<ActionResult> MessageList()
        {
            var config = ConfigService.Get();
            DateTime beginTime = ConfigTime.Today().AddDays(-(config.CustomerQueryTime - 1));
            DateTime endTime = ConfigTime.Today();

            Int32 unReadedCount = await UserService.GetUnreadMessageCount(beginTime, endTime);
            Int32 allCount = await UserService.GetMessageCount(beginTime, endTime);
            Int32 readCount = allCount - unReadedCount;

            ViewBag.Config = config;
            ViewBag.UnReadedCount = unReadedCount;
            ViewBag.AllCount = allCount;
            ViewBag.ReadCount = readCount;
            ViewBag.BeginTime = beginTime;
            ViewBag.EndTime = endTime;
            return View();
        }

        public async Task<ActionResult> GetMessageList(DateTime? beginTime, DateTime? endTime, Int32? status, Int32? pageIndex, Int32? pageSize)
        {
            string userName = LoginHelper.GetUser().UserName;
            var result = await UserService.GetMessageList(userName, beginTime, endTime, status, pageIndex, pageSize);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            else
            {
                return this.Json(result.Data);
            }
        }

        public async Task<ActionResult> GetMessagesTop4()
        {
            int userId = LoginHelper.GetUser().Id;
            var result = await UserService.GetTopMessageList(userId, 4);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("", result.Data); ;
        }

        /// <summary>
        /// 删除站内信
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> DoDeleteMessage(string ids)
        {
            var user = LoginHelper.GetUser();
            var result = await UserService.DeleteMessage(ids, user.Id);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK();
        }

        /// <summary>
        /// 将所有未读站内消息设置为已读
        /// </summary>
        /// <returns></returns>
        [CheckLogin]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> DoReadMessage(string ids)
        {
            var user = LoginHelper.GetUser();
            var result = await UserService.ReadMessage(ids, user.Id);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> GetUnreadMsgCount(DateTime? beginTime, DateTime? endTime)
        {
            var count = await UserService.GetUnreadMessageCount(beginTime, endTime);
            return this.JsonOK("", count);
        }

        #endregion

        #region 优惠钱包

        public ActionResult WalletInitList()
        {
            return View();
        }

        public ActionResult WalletApplyList()
        {
            return View();
        }

        public ActionResult WalletCancelList()
        {
            return View();
        }

        public ActionResult WalletInvalidList()
        {
            return View();
        }

        public ActionResult WalletSendList()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CheckLogin]
        public async Task<ActionResult> GetWalletList(UserWalletStatus? status, bool? isInvalid, int? pageIndex, int? pageSize)
        {
            var user = LoginHelper.GetUser();

            var result = await PromoService.GetWalletList(user.Id, isInvalid, status, pageIndex, pageSize);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.Json(result.Data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CheckLogin]
        public async Task<ActionResult> DoWalletApply(int id)
        {
            var user = LoginHelper.GetUser();

            var result = await PromoService.WalletApply(id,user.Id);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("申请成功！");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CheckLogin]
        public async Task<ActionResult> DoWalletCancel(int id)
        {
            var user = LoginHelper.GetUser();

            var result = await PromoService.WalletCancel(id, user.Id);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("取消成功！");
        }

        [HttpPost]
        [CheckLogin]
        public async Task<ActionResult> GetWalletCount()
        {
            Int32 userId = LoginHelper.GetUser().Id;

            var applyCount = await PromoService.GetWalletCount(userId, (int)UserWalletStatus.Apply);
            var cancelCount = await PromoService.GetWalletCount(userId, (int)UserWalletStatus.Cancel);
            var initCount = await PromoService.GetWalletCount(userId, (int)UserWalletStatus.Init);
            var invalidCount = await PromoService.GetWalletInvalidCount(userId);
            var sendCount = await PromoService.GetWalletCount(userId, (int)UserWalletStatus.Send);
            if (applyCount.HasError)
            {
                return this.JsonError(applyCount.ErrorMessage);
            }
            if (cancelCount.HasError)
            {
                return this.JsonError(cancelCount.ErrorMessage);
            }
            if (initCount.HasError)
            {
                return this.JsonError(initCount.ErrorMessage);
            }
            if (invalidCount.HasError)
            {
                return this.JsonError(invalidCount.ErrorMessage);
            }
            if (sendCount.HasError)
            {
                return this.JsonError(sendCount.ErrorMessage);
            }

            var list = new
            {
                ApplyCount = applyCount.Data,
                CancelCount = cancelCount.Data,
                InitCount = initCount.Data,
                InvalidCount = invalidCount.Data,
                SendCount = sendCount.Data,
            };
            return this.JsonOK("", list);
        }

        // 获取可申请的优惠
        [CheckLogin]
        public async Task<ActionResult> GetPromoInfo(Int32? pageIndex)
        {
            int pIndex = pageIndex.GetValueOrDefault(0);
            var result = await PromoInfoService.GetList(null, pIndex, 20);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.Json(result.Data);
        }

        #endregion

        #region 投注记录
        [CheckLogin]
        public async Task<ActionResult> BetList()
        {
            // 游戏平台信息
            var result = await GameService.GetAllGameApi();
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            ViewBag.Games = result.Data;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CheckLogin]
        public async Task<ActionResult> GetBetList(DateTime beginTime, DateTime endTime, String gamePlatform, int? pageIndex, int? pageSize)
        {
            var user = LoginHelper.GetUser();
            var result = await BetService.GetBetList(user.Id, beginTime, endTime, gamePlatform, pageIndex, pageSize);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            decimal totalPageBetAmount = 0, totalPageRealBetAmount = 0, totalPagePayOutAmount = 0, totalPageBetNum = 0;
            if (result.Data.list != null)
            {
                totalPageBetAmount = result.Data.list.Sum(p => p.Bet);
                totalPageRealBetAmount = result.Data.list.Sum(p => p.RealBet);
                totalPagePayOutAmount = result.Data.list.Sum(p => p.PayOut);
                totalPageBetNum = result.Data.list.Sum(p => p.Num);
            }

            result.Data.extend = new { TotalPageBetAmount = totalPageBetAmount, TotalPageRealBetAmount = totalPageRealBetAmount, TotalPagePayOutAmount = totalPagePayOutAmount, TotalPageBetNum = totalPageBetNum };
            return this.Json(result.Data);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CheckLogin]
        public async Task<ActionResult> GetTotalBet(DateTime beginTime, DateTime endTime, String gamePlatform)
        {
            var user = LoginHelper.GetUser();
            var result = await BetService.GetTotalBet(user.Id, beginTime, endTime, gamePlatform);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.Json(result.Data);
        }

        #endregion

        #region 优惠记录

        [CheckLogin]
        public async Task<ActionResult> PromoList()
        {
            var result = await PromoService.GetBonusTypeList();
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            ViewBag.BonusTypes = result.Data;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CheckLogin]
        public async Task<ActionResult> GetPromoList(DateTime? beginTime, DateTime? endTime, Int32? type, int? pageIndex, int? pageSize)
        {
            //int recordCount = 0;
            //int pIndex = pageIndex.GetValueOrDefault(0);
            //int pSize = this.GetPageSize(pageSize);

            //Decimal totalAmount = 0, totalPageAmount = 0;
            var user = LoginHelper.GetUser();

            var result = await PromoService.GetBonusList(user.UserName, beginTime, endTime, type, pageIndex, pageSize);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            result.Data.extend = new { TotalPageAmount = result.Data.list.Sum(p => p.Amount) };
            return this.Json(result.Data);

            //IList<VUserBonus> bonuses = _userBonusService.GetListByUserName(userName, beginTime, endTime, type,
            //    out totalAmount, "CreateTime", "DESC", pIndex, pSize, out recordCount);

            //// 小计
            //totalPageAmount = bonuses.Sum(p => p.Amount);

            //return this.Json(new PagerFrontViewModel<VUserBonus>(
            //    recordCount,
            //    new { TotalAmount = totalAmount, TotalPageAmount = totalPageAmount },
            //    bonuses));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [CheckLogin]
        public async Task<ActionResult> GetTotalPromo(DateTime? beginTime, DateTime? endTime, Int32? type)
        {
            var user = LoginHelper.GetUser();

            var result = await PromoService.GetTotalBonus(user.UserName, beginTime, endTime, type);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            return this.Json(result.Data);
        }

        #endregion

        #region 推荐返利

        [CheckLogin]
        public async Task<ActionResult> RecommendList()
        {
            var user = LoginHelper.GetUser();
            var result = await PromoService.GetRecommendRebateInfo(user.Id);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            ViewBag.RecommendInfo = result.Data;
            return View();
        }

        [CheckLogin, AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> GetRecommendList(DateTime? beginTime, DateTime? endTime, int? pageIndex, int? pageSize)
        {
            int userId = LoginHelper.GetUser().Id;
            var result = await PromoService.GetRecommendList(userId, beginTime, endTime, pageIndex, pageSize);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.Json(result.Data);
        }

        #endregion

        #region 历史记录





        #endregion

        #region 账户设置

        [CheckLogin, HttpPost]
        public async Task<ActionResult> DoSaveLoginPassword(string oldPwd, string newPwd)
        {
            string userName = LoginHelper.GetUser().UserName;
            var result = await UserService.ChangePasswordByUser(userName, oldPwd, newPwd);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("密码修改成功！");
        }

        [CheckLogin]
        public ActionResult LoginPassword()
        {
            return View();
        }

        [CheckLogin]
        public ActionResult Setting()
        {
            ViewBag.User = LoginHelper.GetUser();
            ViewBag.Config = ConfigService.Get(); // 配置
            return View();
        }

        [CheckLogin,HttpPost]
        public async Task<ActionResult> DoSaveWithdrawalPassword(String oldPwd, String newPwd)
        {
            var user = LoginHelper.GetUser();
            var result = await UserService.UpdateWithdrawalPwd(user.Id, oldPwd, newPwd);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK();
        }

        // 保存设置
        [CheckLogin, HttpPost]
        public async Task<ActionResult> DoSaveSetting(string userName, string trueName, string phone, string email, DateTime? birthday, string qq, string province, string city, string address)
        {
            var user = LoginHelper.GetUser();
            if (!string.IsNullOrEmpty(user.Phone) && user.PhoneValidateStatus)
            {
                phone = user.Phone;
            }
            if (!string.IsNullOrEmpty(user.Email) && user.EmailValidateStatus)
            {
                email = user.Email;
            }

            var result = await UserService.UpdateUserInfo(user.UserName, trueName, phone, email, birthday, qq, province, city, address);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("修改成功！");
        }

        #endregion

        #region 获取用户等级

        [HttpPost]
        public async Task<ActionResult> GetLevel()
        {
            var result = await ConfigService.GetFirstLevel(); //.FirstOrDefault();
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("", result.Data);
        }

        #endregion

        public async Task<ActionResult> GetMGGameAccountPwd(string loginPwd)
        {
            var result = await UserService.GetMGGameAccountPwd(loginPwd);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }
            return this.JsonOK("", result.Data);
        }

        #region // 智付微信支付
        [CheckLogin]
        public async Task<ActionResult> DINWXPay()
        {
            User user = LoginHelper.GetUser();
            ViewBag.User = user;

            // 优惠
            var result = await UserService.GetFirstDepositPromoList(user.UserName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            // 支付
            var piesResult = await UserService.GetUserThirdPay();
            if (piesResult.HasError)
            {
                return this.JsonError(piesResult.ErrorMessage);
            }

            ViewBag.Pies = piesResult.Data;
            ViewBag.PrefInfos = result.Data;

            return View();
        }

        #endregion

        #region // 新贝支付

        /// <summary>
        /// 新贝微信支付
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> XBEIWXPay()
        {
            User user = LoginHelper.GetUser();
            ViewBag.User = user;

            // 优惠
            var result = await UserService.GetFirstDepositPromoList(user.UserName);
            if (result.HasError)
            {
                return this.JsonError(result.ErrorMessage);
            }

            // 支付
            var piesResult = await UserService.GetUserThirdPay();
            if (piesResult.HasError)
            {
                return this.JsonError(piesResult.ErrorMessage);
            }

            ViewBag.Pies = piesResult.Data;
            ViewBag.PrefInfos = result.Data;

            return View();
        }

        #endregion


        protected override void Dispose(bool disposing)
        {
            if (_userService != null)
            {
                _userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
