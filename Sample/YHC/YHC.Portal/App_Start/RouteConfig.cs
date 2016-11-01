using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YHC.Portal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /// 验证码
            routes.MapRoute("AuthCode_CreateImageCode", "authcode/createimagecode.html", new { controller = "AuthCode", action = "CreateImageCode" });
            routes.MapRoute("AuthCode_CreateLoginImageCode", "authcode/createloginimagecode.html", new { controller = "AuthCode", action = "CreateLoginImageCode" });

            ///Home
            routes.MapRoute("Home_Index", "index.html", new { controller = "Home", action = "Index" });
            routes.MapRoute("Home_Login", "login.html", new { controller = "Home", action = "Login" });
            routes.MapRoute("Home_Regist", "regist.html", new { controller = "Home", action = "Regist" });
            routes.MapRoute("Home_RegistSuccess", "registration_success.html", new { controller = "Home", action = "RegistSuccess" });
            routes.MapRoute("Home_ForgetPwd", "forgetpwd.html", new { controller = "Home", action = "ForgetPwd" });
            routes.MapRoute("Home_DoForgetPwd", "doforgetpwd.html", new { controller = "Home", action = "DoForgetPwd" });
            routes.MapRoute("Home_ForgetPwd2", "forgetpwd2.html", new { controller = "Home", action = "ForgetPwd2" });
            routes.MapRoute("Home_DoGetForgetPwdValidateCode", "dogetforgetpwdvalidatecode.html", new { controller = "Home", action = "DoGetForgetPwdValidateCode" });
            routes.MapRoute("Home_ForgetPwdModifyPwd", "forgetpwdmodifypwd.html", new { controller = "Home", action = "ForgetPwdModifyPwd" });
            routes.MapRoute("Home_DoForgetPwdModifyPwd", "doforgetpwdmodifypwd.html", new { controller = "Home", action = "DoForgetPwdModifyPwd" });

            routes.MapRoute("Home_Download", "download.html", new { controller = "Home", action = "Download" });
            routes.MapRoute("Home_Signal", "signal.html", new { controller = "Home", action = "Signal" });
            routes.MapRoute("Home_Maintenance", "maintenance.html", new { controller = "Home", action = "Maintenance" });
            routes.MapRoute("Home_Mobile", "mobile.html", new { controller = "Home", action = "Mobile" });

            routes.MapRoute("News_Index", "news.html", new { controller = "News", action = "Index" });
            ///Page
            routes.MapRoute("Page_Alternate_URL", "alternate_url.html", new { controller = "Page", action = "AlternateURL" });
            routes.MapRoute("Page_About", "about.html", new { controller = "Page", action = "About" });
            routes.MapRoute("Page_Contact", "contact.html", new { controller = "Page", action = "Contact" });
            routes.MapRoute("Page_Guide", "guide.html", new { controller = "Page", action = "Guide" });
            routes.MapRoute("Page_Provision", "provision.html", new { controller = "Page", action = "Provision" });
            routes.MapRoute("Page_Preferential", "preferential.html", new { controller = "Page", action = "Preferential" });
            routes.MapRoute("Page_Privacy", "privacy.html", new { controller = "Page", action = "Privacy" });
            routes.MapRoute("Page_Help", "help.html", new { controller = "Page", action = "GameHelp" });
            routes.MapRoute("Page_Notice", "notice.html", new { controller = "Page", action = "Notice" });

            ///Error
            routes.MapRoute("Error_Error403", "403.html", new { controller = "Error", action = "Error403" });
            routes.MapRoute("Error_Error404", "404.html", new { controller = "Error", action = "Error404" });
            routes.MapRoute("Error_Error500", "500.html", new { controller = "Error", action = "Error500" });
            routes.MapRoute("Error_Limit", "limit.html", new { controller = "Error", action = "Limit" });
            routes.MapRoute("Error_Maintenance", "maintainance.html", new { controller = "Error", action = "Maintenance" });


            // 智付微信支付
            routes.MapRoute("User_DinWXPay", "user/dinwxpay.html", new { controller = "User", action = "DINWXPay" }); // 智付微信支付

            // 新贝微信支付
            routes.MapRoute("User_XBeiWXPay", "user/xbeiwxpay.html", new { controller = "User", action = "XBEIWXPay" }); // 新贝微信支付

            // 通用，头部，底部，登录信息，公告
            routes.MapRoute("User_GetHeader", "user/getheader.html", new { controller = "User", action = "GetHeader" });
            routes.MapRoute("User_GetFooter", "user/getfooter.html", new { controller = "User", action = "GetFooter" });
            routes.MapRoute("User_GetLiveChat", "user/getlivechat.html", new { controller = "User", action = "GetLiveChat" });
            routes.MapRoute("User_GetNews", "user/getnews.html", new { controller = "User", action = "GetNews" });
            routes.MapRoute("User_GetLoginInfo", "user/getlogininfo.html", new { controller = "User", action = "GetLoginInfo" });

            // 新闻
            routes.MapRoute("News_GetTwoDaysBroadcast", "news/gettwodaysbroadcast.html", new { controller = "News", action = "GetTwoDaysBroadcast" });

            //游戏 
            routes.MapRoute("Game_Sport", "sport.html", new { controller = "Game", action = "Sport" });
            routes.MapRoute("Game_Casino", "casino.html", new { controller = "Game", action = "Casino" });
            routes.MapRoute("Game_Slot", "slot.html", new { controller = "Game", action = "Slot" });
            routes.MapRoute("Game_PTSlot", "ptslot.html", new { controller = "Game", action = "PtSlot" });
            routes.MapRoute("Game_MGSlot", "mgslot.html", new { controller = "Game", action = "MgSlot" });
            routes.MapRoute("Game_AGSlot", "agslot.html", new { controller = "Game", action = "AgSlot" });
            routes.MapRoute("Game_BBINSlot", "bbinslot.html", new { controller = "Game", action = "BbinSlot" });
            routes.MapRoute("Game_Lottery", "lottery.html", new { controller = "Game", action = "Lottery" });

            routes.MapRoute("Game_LaunchSport", "game/launchsport.html", new { controller = "Game", action = "LaunchSport" });
            routes.MapRoute("Game_Play", "game/play.html", new { controller = "Game", action = "Play" });
            routes.MapRoute("Game_TryPlay", "game/tryplay.html", new { controller = "Game", action = "TryPlay" });
            routes.MapRoute("Game_PTLoading", "game/ptloading.html", new { controller = "Game", action = "PTLoading" });
            routes.MapRoute("User_GetGameCash", "user/getgamecash.html", new { controller = "User", action = "GetGameCash" });
            routes.MapRoute("User_GetAllGameCash", "user/getallgamecash.html", new { controller = "User", action = "GetAllGameCash" });
            routes.MapRoute("Game_GetElectrones", "game/getelectrones.html", new { controller = "Game", action = "GetElectrones" });
            routes.MapRoute("Game_GetJackpotsGames", "game/getjackpotsgames.html", new { controller = "Game", action = "GetJackpotsGames" });

            routes.MapRoute("Agent_Index", "agent/index.html", new { controller = "Agent", action = "Index" });
            routes.MapRoute("Agent_Regist", "agent_regist.html", new { controller = "Agent", action = "Regist" });
            routes.MapRoute("Agent_Program", "program.html", new { controller = "Agent", action = "Program" });
            routes.MapRoute("Agent_Protocol", "protocol.html", new { controller = "Agent", action = "Protocol" });
            routes.MapRoute("Agent_Brand", "brand.html", new { controller = "Agent", action = "Brand" });
            routes.MapRoute("Agent_Agreement", "agent_agreement.html", new { controller = "Agent", action = "Agreement" });

            // 帮助
            //routes.MapRoute("Help_Partner", "partner.html", new { controller = "Help", action = "Partner" });
            routes.MapRoute("Help_Index", "help.html", new { controller = "Help", action = "Index" });
            //routes.MapRoute("Help_Index", "help/{key}.html", new { controller = "Help", action = "Index" });

            routes.MapRoute("Help_Licence", "licence.html", new { controller = "Help", action = "Licence" });
            routes.MapRoute("Help_Sitemap", "sitemap.html", new { controller = "Help", action = "SiteMap" });
            routes.MapRoute("Help_PwdForget", "pwdforget.html", new { controller = "Help", action = "PwdForget" });

            /// 优惠
            routes.MapRoute("Promo_Index", "promo.html", new { controller = "Promo", action = "Index" });

            //MG游戏帐号密码
            routes.MapRoute("User_MGGameAccountPwd", "user/getmggamepwd.html", new { controller = "User", action = "GetMGGameAccountPwd" });
            // 市场活动-推荐返利
            routes.MapRoute("User_RecommendList", "user/recommendlist.html", new { controller = "User", action = "RecommendList" });
            routes.MapRoute("User_GetRecommendList", "user/getrecommendlist.html", new { controller = "User", action = "GetRecommendList" });

            //转账
            routes.MapRoute("User_DoTransfer", "user/dotransfer.html", new { controller = "User", action = "DoTransfer" });


            //登陆
            routes.MapRoute("User_Relogin", "user/relogin.html", new { controller = "User", action = "Relogin" });
            routes.MapRoute("User_Logout", "home/logout.html", new { controller = "Home", action = "Logout" });

            // 退出
            routes.MapRoute("Home_LogOut", "logout.html", new { controller = "Home", action = "LogOut" });

            //会员中心
            routes.MapRoute("User_Index", "user/index.html", new { controller = "User", action = "Index" });
            routes.MapRoute("User_GetUserExtendInfo", "user/getuserextendinfo.html", new { controller = "User", action = "GetUserExtendInfo" });
            routes.MapRoute("User_DoSaveLoginPassword", "user/dosaveloginpassword.html", new { controller = "User", action = "DoSaveLoginPassword" });

            //会员存款
            routes.MapRoute("User_Deposit", "user/deposit.html", new { controller = "User", action = "Deposit" });
            routes.MapRoute("User_DepositOnline", "user/DepositOnline.html", new { controller = "User", action = "DepositOnline" });
            routes.MapRoute("User_DoDeposit", "user/dodeposit.html", new { controller = "User", action = "DoDeposit" });
            routes.MapRoute("User_DepositAlipay", "user/DepositAlipay.html", new { controller = "User", action = "DepositAlipay" });
            routes.MapRoute("User_DepositWechat", "user/DepositWechat.html", new { controller = "User", action = "DepositWechat" });

            //会员提款
            routes.MapRoute("User_Withdrawal", "user/withdrawal.html", new { controller = "User", action = "Withdrawal" });
            routes.MapRoute("User_DoWithdrawal", "user/dowithdrawal.html", new { controller = "User", action = "DoWithdrawal" });
            routes.MapRoute("User_GetBanks", "user/getbanks.html", new { controller = "User", action = "GetBanks" });
            routes.MapRoute("User_GetCities", "user/getcities.html", new { controller = "User", action = "GetCities" });
            routes.MapRoute("User_GetProvinces", "user/getprovinces.html", new { controller = "User", action = "GetProvinces" });

            // 绑定银行卡
            routes.MapRoute("User_GetBindBanks", "user/getbindbanks.html", new { controller = "User", action = "GetBindBanks" });
            routes.MapRoute("User_DoSaveBankCard", "user/dosavebankcard.html", new { controller = "User", action = "DoSaveBankCard" });
            routes.MapRoute("User_WithdrawalPassword", "user/withdrawalpassword.html", new { controller = "User", action = "WithdrawalPassword" });
            routes.MapRoute("User_LoginPassword", "user/loginpassword.html", new { controller = "User", action = "LoginPassword" });

            routes.MapRoute("User_Transfer", "user/transfer.html", new { controller = "User", action = "Transfer" });
            routes.MapRoute("User_GetUserCash", "user/getmastercash.html", new { controller = "User", action = "GetUserCash" });
            routes.MapRoute("User_GetWalletCash", "user/getwalletcash.html", new { controller = "User", action = "GetWalletCash" });

            // 绑定手机，邮箱
            routes.MapRoute("User_BindMobile", "user/bindmobile.html", new { controller = "User", action = "BindMobile" });
            routes.MapRoute("User_BindMobileStep2", "user/bindmobilestep2.html", new { controller = "User", action = "BindMobileStep2" });
            routes.MapRoute("User_SendMobileValidateCode", "user/sendmobilevalidatecode.html", new { controller = "User", action = "SendMobileValidateCode" });
            routes.MapRoute("User_DoBindMobile", "user/dobindmobile.html", new { controller = "User", action = "DoBindMobile" });
            routes.MapRoute("User_BindMobileSuccess", "user/bindmobilesuccess.html", new { controller = "User", action = "BindMobileSuccess" });
            routes.MapRoute("User_BindEmail", "user/bindemail.html", new { controller = "User", action = "BindEmail" });
            routes.MapRoute("User_BindEmailStep2", "user/bindemailstep2.html", new { controller = "User", action = "BindEmailStep2" });
            routes.MapRoute("User_SendEmailValidateCode", "user/sendemailvalidatecode.html", new { controller = "User", action = "SendEmailValidateCode" });
            routes.MapRoute("User_BindEmailSuccess", "user/bindemailsuccess.html", new { controller = "User", action = "BindEmailSuccess" });


            // 列表相关
            routes.MapRoute("User_DepositList", "user/depositlist.html", new { controller = "User", action = "DepositList" });
            routes.MapRoute("User_GetDepositList", "user/getdepositlist.html", new { controller = "User", action = "GetDepositList" });
            routes.MapRoute("User_GetTotalDeposit", "user/gettotaldeposit.html", new { controller = "User", action = "GetTotalDeposit" });

            routes.MapRoute("User_WithdrawalList", "user/withdrawallist.html", new { controller = "User", action = "WithdrawalList" });
            routes.MapRoute("User_GetWithdrawalList", "user/getwithdrawallist.html", new { controller = "User", action = "GetWithdrawalList" });
            routes.MapRoute("User_GetTotalWithdrawal", "user/gettotalwithdrawal.html", new { controller = "User", action = "GetTotalWithdrawal" });

            routes.MapRoute("User_TransferList", "user/transferlist.html", new { controller = "User", action = "TransferList" });
            routes.MapRoute("User_GetTransferList", "user/gettransferlist.html", new { controller = "User", action = "GetTransferList" });
            routes.MapRoute("User_GetTotalTransfer", "user/gettotaltransfer.html", new { controller = "User", action = "GetTotalTransfer" });

            routes.MapRoute("User_PromoList", "user/promolist.html", new { controller = "User", action = "PromoList" });
            routes.MapRoute("User_GetPromoList", "user/getpromolist.html", new { controller = "User", action = "GetPromoList" });
            routes.MapRoute("User_GetTotalPromo", "user/gettotalpromo.html", new { controller = "User", action = "GetTotalPromo" });

            routes.MapRoute("User_BetList", "user/betlist.html", new { controller = "User", action = "BetList" });
            routes.MapRoute("User_GetBetList", "user/getbetlist.html", new { controller = "User", action = "GetBetList" });
            routes.MapRoute("User_GetTotalBet", "user/gettotalbet.html", new { controller = "User", action = "GetTotalBet" });

            // 会员资料编辑，密码修改
            routes.MapRoute("User_Setting", "user/setting.html", new { controller = "User", action = "Setting" });
            routes.MapRoute("User_DoSaveSetting", "user/dosavesetting.html", new { controller = "User", action = "DoSaveSetting" });
            routes.MapRoute("User_DoSaveWithdrawalPassword", "user/dosavewithdrawalpassword.html", new { controller = "User", action = "DoSaveWithdrawalPassword" });

            // 站内信
            routes.MapRoute("User_MessageList", "user/messagelist.html", new { controller = "User", action = "MessageList" });
            routes.MapRoute("User_GetMessageList", "user/getmessagelist.html", new { controller = "User", action = "GetMessageList" });
            routes.MapRoute("User_GetMessagesTop4", "user/getmessagestop4.html", new { controller = "User", action = "GetMessagesTop4" });
            routes.MapRoute("User_DoGetAllUnreadQuantity", "user/dogetallunreadquantity.html", new { controller = "User", action = "DoGetAllUnreadQuantity" });
            routes.MapRoute("User_DoReadMessage", "user/doreadmessage.html", new { controller = "User", action = "DoReadMessage" });
            routes.MapRoute("User_DoReadAllMessage", "user/doreadallmessage.html", new { controller = "User", action = "DoReadAllMessage" });
            routes.MapRoute("User_DoDeleteMessage", "user/dodeletemessage.html", new { controller = "User", action = "DoDeleteMessage" });

            //锁定优惠
            routes.MapRoute("User_WalletInitList", "user/walletinitlist.html", new { controller = "User", action = "WalletInitList" });
            routes.MapRoute("User_WalletApplyList", "user/walletapplylist.html", new { controller = "User", action = "WalletApplyList" });
            routes.MapRoute("User_WalletSendList", "user/walletsendlist.html", new { controller = "User", action = "WalletSendList" });
            routes.MapRoute("User_WalletCancelList", "user/walletcancellist.html", new { controller = "User", action = "WalletCancelList" });
            routes.MapRoute("User_WalletInvalidList", "user/walletinvalidlist.html", new { controller = "User", action = "WalletInvalidList" });
            routes.MapRoute("User_GetWalletList", "user/getwalletlist.html", new { controller = "User", action = "GetWalletList" });
            routes.MapRoute("User_DoWalletApply", "user/dowalletapply.html", new { controller = "User", action = "DoWalletApply" });
            routes.MapRoute("User_DoWalletCancel", "user/dowalletcancel.html", new { controller = "User", action = "DoWalletCancel" });
            routes.MapRoute("User_GetWalletCount", "user/getwalletcount.html", new { controller = "User", action = "GetWalletCount" });

            // 支付
            routes.MapRoute("Pay_OnlinePay", "pay/onlinepay.html", new { controller = "Pay", action = "OnlinePay" });
            routes.MapRoute("Pay_DoPay", "pay/dopay.html", new { controller = "Pay", action = "DoPay" });
            routes.MapRoute("Pay_PayConfirm", "pay/payconfirm.html", new { controller = "Pay", action = "PayConfirm" });
            routes.MapRoute("Pay_Success", "pay/success.html", new { controller = "Pay", action = "Success" });

            //优惠信息
            routes.MapRoute("User_GetPromoInfo", "user/getpromoinfo.html", new { controller = "User", action = "GetPromoInfo" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}