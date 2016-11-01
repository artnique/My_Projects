using YHC.Portal.Core;
using YHC.Portal.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace YHC.Portal.Helper
{
    public class UtilHelper
    {
        /// <summary>
        /// 获取备注类型
        /// </summary>
        /// <param name="remarkType"></param>
        /// <returns></returns>
        public static string GetRemarkTypeText(RemarkType remarkType)
        {
            switch (remarkType)
            {
                case RemarkType.General:
                    return "普通类";
                case RemarkType.Finance:
                    return "财务类";
                case RemarkType.Hazard:
                    return "危险类";
            }
            return string.Empty;
        }

        #region 获取代理审核状态文本

        /// <summary>
        /// 获取代理审核状态文本
        /// </summary>
        /// <param name="auditStatus"></param>
        /// <returns></returns>
        public static String GetAgentAuditStatusText(AppStatus auditStatus)
        {
            switch (auditStatus)
            {
                case AppStatus.AppUnPass:
                    return "审核不通过";
                case AppStatus.AppPass:
                    return "审核通过";
                default:
                    return "待审核";
            }
        }

        #endregion

        public static string ConvertNameValueString(NameValueCollection nc, String sperator = "&")
        {
            if (null == nc || nc.Count == 0)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            foreach (string key in nc.Keys)
            {
                if (null != key)
                {
                    sb.AppendFormat("{0}={1}" + sperator, key, nc[key]);
                }
            }
            return sb.ToString(0, sb.Length - 1);
        }

        public static String GetBetTypeText_EN(int type)
        {
            if (type == 1)
            {
                return "casino";
            }
            else if (type == 2)
            {
                return "lottery";
            }
            else if (type == 3)
            {
                return "sport";
            }
            else
            {
                return "slot";
            }
        }

        /// <summary>
        /// 公共请求方法
        /// </summary>
        /// <param name="url">要请求的url地址</param>
        /// <param name="reqdata">请求参数,"id=xx&key=xx"</param>
        /// <param name="method">GET,POST</param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static string SendHttpRequest(string url, string reqdata, string method, string userAgent, bool https = false)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url", "不能为空");
            }
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentNullException("method", "不能为空");
            }

            string html = "";
            HttpWebRequest request = null;
            HttpRequestCachePolicy policy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
            if (https)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                ServicePointManager.ServerCertificateValidationCallback =
                new System.Net.Security.RemoteCertificateValidationCallback(HttpsCheckValidationResult);
            }

            //请求
            if (method.ToLower() == "get")
            {
                request = (HttpWebRequest)WebRequest.Create(new Uri(url + "?" + reqdata));
                request.Method = method;
                request.CachePolicy = policy;
                if (string.IsNullOrEmpty(userAgent) == false)
                {
                    request.UserAgent = userAgent;
                }
                request.ContentType = "text/html;charset=UTF-8";
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                request.Method = method;
                if (string.IsNullOrEmpty(userAgent) == false)
                {
                    request.UserAgent = userAgent;
                }
                request.CachePolicy = policy;
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] buf = System.Text.Encoding.GetEncoding("utf-8").GetBytes(reqdata);
                using (System.IO.Stream s = request.GetRequestStream())
                {
                    s.Write(buf, 0, buf.Length);
                    s.Close();
                }
            }

            //返回响应
            HttpWebResponse res = request.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.UTF8);
            html = sr.ReadToEnd();
            sr.Close();
            res.Close();

            return html;
        }

        #region 获取推荐人推荐链接

        /// <summary>
        /// 根据当前域名获取推荐链接，例如：http://localhost:9800/?u=123456
        /// </summary>
        /// <param name="recommendCode">推荐码</param>
        /// <returns></returns>
        public static String GetRecommendUrl(String recommendCode)
        {
            String currentDomain = (new HttpContextWrapper(HttpContext.Current)).Request.GetCurrentFullHost();
            String recommandUrl = "http://{0}/?u={1}".FormatWith(currentDomain, recommendCode);

            return recommandUrl;
        }

        #endregion

        /// <summary>
        /// 解决HTTPS请求。
        /// 基础连接已经关闭: 未能为 SSL/TLS 安全通道建立信任关系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static bool HttpsCheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 总是接受  
            return true;
        }
    }
}