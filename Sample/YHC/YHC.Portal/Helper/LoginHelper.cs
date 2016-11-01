using YHC.Portal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace YHC.Portal.Helper
{
    public class LoginHelper
    {
        public const string COOKIE_AUTHCODE = "authcode";

        #region 验证码操作

        public static void SetAuthCode(String code)
        {
            HttpCookie cookie = new HttpCookie(COOKIE_AUTHCODE);
            cookie.Name = COOKIE_AUTHCODE;
            cookie.Value = code;

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static string GetAuthCode()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[COOKIE_AUTHCODE];
            if (cookie != null)
            {
                return cookie.Value;
            }
            return "";
        }

        public static void RemoveAuthCode()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[COOKIE_AUTHCODE];
            if (cookie != null)
            {
                cookie.Value = "";
                cookie.Expires = ConfigTime.Now().AddDays(-10);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        #endregion

        public static User GetUser()
        {
            User user = null;
            using (var handler = new HttpClientHandler { UseCookies = false })
            using (var client = new HttpClient(handler) { BaseAddress = ConfigHelper.ApiBaseAddress })
            {
                var message = new HttpRequestMessage(HttpMethod.Get, "api/account/getloginuser");
                var cookies = HttpContext.Current.Request.Headers.Get("Cookie");
                message.Headers.Add("Cookie", cookies);
                var response = client.SendAsync(message).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseText = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<User>(responseText);
                }
            }
            return user;
        }

        /// <summary>
        /// 返回登录状态 0:已登录、1:未登录、2:登录已过期
        /// </summary>
        /// <returns></returns>
        public static int CheckLogin()
        {
            using (var handler = new HttpClientHandler { UseCookies = false })
            using (var client = new HttpClient(handler) { BaseAddress = ConfigHelper.ApiBaseAddress })
            {
                var message = new HttpRequestMessage(HttpMethod.Get, "api/account/GetLoginStatus");
                var cookies = HttpContext.Current.Request.Headers.Get("Cookie");
                message.Headers.Add("Cookie", cookies);
                var response = client.SendAsync(message).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseText = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<int>(responseText);
                    return result;
                }
            }
            return 1;
        }
    }
}