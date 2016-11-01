using YHC.Portal.Helper;
using YHC.Portal.Models;
using YHC.Portal.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace YHC.Portal.Services
{
    public class AccountService : BaseService
    {
        /// <summary>
        /// 注册账户，注册后成功会自动登录
        /// </summary>
        /// <param name="extendCode"></param>
        /// <param name="userName"></param>
        /// <param name="trueName"></param>
        /// <param name="password"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="source"></param>
        /// <param name="qq"></param>
        /// <param name="birthday"></param>
        /// <param name="isReceiveSMS"></param>
        /// <param name="isReceiveEmail"></param>
        /// <param name="authCode"></param>
        /// <param name="withdrawalPassword"></param>
        /// <param name="ip"></param>
        /// <param name="regWebSite"></param>
        /// <returns></returns>
        public async Task<ApiResult<LoginInfo>> Regist(string extendCode, string userName, string trueName, string password, string phone, string email, string source, string qq, DateTime? birthday, 
            bool isReceiveSMS, bool isReceiveEmail, string authCode, string withdrawalPassword, string ip, string regWebSite)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("extendCode", extendCode);
            nvc.Add("userName", userName);
            nvc.Add("trueName", trueName);
            nvc.Add("password", password);
            nvc.Add("phone", phone);
            nvc.Add("email", email);
            nvc.Add("source", source);
            nvc.Add("qq", qq);
            nvc.Add("birthday", birthday.ToString());
            nvc.Add("isReceiveSMS", isReceiveSMS.ToString());
            nvc.Add("isReceiveEmail", isReceiveEmail.ToString());
            nvc.Add("authCode", authCode);
            nvc.Add("withdrawalPassword", withdrawalPassword);
            nvc.Add("IP", ip);
            nvc.Add("RegWebSite", regWebSite);
            var request = CreateRequest("api/Account/Regist", HttpMethod.Post, nvc);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<LoginInfo>();
        }

        public async Task<ApiResult<int>> GetLoginStatus()
        {
            NameValueCollection nvc = new NameValueCollection();
            var request = GetRequest("api/Account/GetLoginStatus", nvc);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<int>();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<ApiResult<LoginInfo>> Login(string userName, string password, string ip, string webSite)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("grant_type", "password");
            nvc.Add("userName", userName);
            nvc.Add("password", password);
            nvc.Add("IP", ip);
            nvc.Add("LoginWebSite", webSite);
            var request = CreateRequest("api/Account/Login", HttpMethod.Post, nvc);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<LoginInfo>();
        }

        /// <summary>
        /// 完成登录
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<ApiResult<LoginInfo>> LoginCompleted(HttpResponseMessage response)
        {
            ApiResult<LoginInfo> result = await response.ParseTResultAsync<LoginInfo>();
            if (!result.HasError && result.Data != null)
            {
                // 把Api写的Cookie，写到本站点
                if (response.Headers.Contains("set-cookie"))
                {
                    var cookies = response.Headers.GetValues("set-cookie");
                    foreach (var item in cookies)
                    {
                        HttpCookie cookie = GetHttpCookie(item);
                        if (cookie != null)
                        {
                            HttpContext.Current.Response.Cookies.Add(cookie);
                        }
                    }
                    //下面的方法无效
                    //HttpContext.Current.Response.Headers.Set("set-cookie", string.Join(", ", cookieValue));
                }
            }
            return result;
        }

        /// <summary>
        /// 把Api写的Cookie转成UI Cookie
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        private HttpCookie GetHttpCookie(string cookie)
        {
            HttpCookie result = null;
            string[] items = cookie.Split(';');
            for (int i = 0; i < items.Length; i++)
            {
                string[] tmp = items[i].Split('=');
                if (tmp.Length == 2)
                {
                    if (result == null)
                    {
                        result = new HttpCookie(tmp[0].Trim(), tmp[1].Trim());
                    }
                    else
                    {
                        switch (tmp[0].ToLower().Trim())
                        {
                            case "expires":
                                result.Expires = DateTime.Parse(tmp[1].Trim());
                                break;
                            case "path":
                                result.Path = tmp[1].Trim();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 获取注册设置
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<RegistSetting>> GetRegistSetting()
        {
            var request = CreateRequest("api/Account/GetRegistSetting", HttpMethod.Get);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<RegistSetting>();
        }

        public async Task<ApiResult<bool>> Logout()
        {
            var request = GetRequest("api/Account/Logout");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<int>> RegistAgent(string userName, string trueName, string password, string phone, string email, string qq, DateTime? birthday, string domain, string extendDes, string regIp)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("userName", userName);
            nvc.Add("trueName", trueName);
            nvc.Add("password", password);
            nvc.Add("phone", phone);
            nvc.Add("email", email);
            nvc.Add("qq", qq);
            nvc.Add("birthday", birthday.HasValue ? birthday.ToString() : "");
            nvc.Add("domain", domain);
            nvc.Add("extendDesc", extendDes);
            nvc.Add("ip", regIp);
            var request = CreateRequest("api/Account/RegistAgent", HttpMethod.Post, nvc);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<int>();
        }

        public async Task<ApiResult<bool>> AgentExtension(string code)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("", code);
            var request = PostRequest("api/Agent/Extend", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }
    }
}