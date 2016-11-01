using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Extensions
{
    public static class HttpRequestExtension
    {
        /// <summary>
        /// 获得当前页面客户端的IP地址，使用ServerVariables获取。
        /// </summary>
        /// <param name="request">当前请求</param>
        /// <returns>客户端IP地址</returns>
        public static string GetClientIP(this HttpRequestBase request)
        {
            string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrWhiteSpace(result))
            {
                // cdn，代理：211.148.213.229, 122.10.88.116, 
                result = request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrWhiteSpace(result))
            {
                result = request.UserHostAddress;
            }
            if (result.Contains("::"))
            {
                result = "127.0.0.1";
            }

            // 处理加代理，CDN后ip获取了多个的问题
            if (string.IsNullOrWhiteSpace(result) == false)
            {
                String[] ips = result.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (ips.Length >= 1)
                {
                    result = ips[0].Trim();
                }
            }

            // 判断是否带端口号
            Int32 pos = result.IndexOf(":");
            if (pos > -1)
            {
                result = result.Substring(0, pos);
            }

            return result;
        }

        /// <summary>
        /// 返回完整的主机头
        /// </summary>
        /// <param name="request">当前请求</param>
        /// <returns>完整的主机头。如果不是默认端口，则返回带端口的主机头</returns>
        public static string GetCurrentFullHost(this HttpRequestBase request)
        {
            if (!request.Url.IsDefaultPort)
            {
                return String.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            return request.Url.Host;
        }
    }
}