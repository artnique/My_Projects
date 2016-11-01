using YHC.Portal.Extensions;
using YHC.Portal.Helper;
using YHC.Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace YHC.Portal.Filters
{
    /// <summary>
    /// 检查登录（用于web网站）
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        private Boolean _redirectToIndex = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="redirectToIndex">未登录，是否转向首页，默认不转向</param>
        public CheckLoginAttribute(Boolean redirectToIndex = false)
        {
            _redirectToIndex = redirectToIndex;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Boolean result = false;
            int status = LoginHelper.CheckLogin();

            if (status == 0)
            {
                result = true;
            }

            // 未登录
            if (result == false)
            {
                String msg = "您还没有登录";
                if (status == 1)
                {
                    msg = "您还没有登录"; // "您的账号已在其它地方登录";
                }
                else if (status == 2)
                {
                    msg = "您的登录已过期，请重新登录";
                }

                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    // 如果是ajax提交，则返回json格式
                    JsonResult json = new JsonResult();
                    json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                    json.Data = new { success = false, msg = msg };

                    filterContext.Result = json;
                }
                else
                {
                    // 重新登录提示页
                    String redirectUrl = filterContext.RequestContext.HttpContext.Request.Url.ToString().UrlEncode();
                    String url = "/user/relogin.html?msg={0}&redirectUrl={1}".FormatWith(msg.UrlEncode(), redirectUrl);
                    if (_redirectToIndex)
                    {
                        url = "/index.html?redirectUrl=" + redirectUrl;
                    }
                    filterContext.Result = new RedirectResult(url);
                }
            }

            base.OnActionExecuting(filterContext);
        }



    }
}