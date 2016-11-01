using YHC.Portal.Helper;
using YHC.Portal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace YHC.Portal.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public async static Task<ApiResult<T>> ParseTResultAsync<T>(this HttpResponseMessage response)
        {
            SetCookie(response);

            ApiResult<T> result = new ApiResult<T>();
            string responseText = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    if (responseText.Contains("StatusCode") && responseText.Contains("Message") && responseText.Contains("Data"))
                    {
                        var obj = new { Message = "", StatusCode = 0 };
                        obj = JsonConvert.DeserializeAnonymousType(responseText, obj);
                        result.ErrorMessage = obj.Message;
                        result.ErrorCode = obj.StatusCode;
                    }
                    else
                    {
                        result.Data = JsonConvert.DeserializeObject<T>(responseText);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Logger.Info(responseText);

                    result.ErrorMessage = "网络错误(" + response.StatusCode + ")，请稍后重试";
                }
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                try
                {
                    var obj = new { message = "", exceptionMessage = "", exceptionType = "" };
                    obj = JsonConvert.DeserializeAnonymousType(responseText, obj);
                    result.ErrorMessage = obj.exceptionMessage;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Logger.Info(responseText);

                    result.ErrorMessage = "网络错误(" + response.StatusCode + ")，请稍后重试";
                }
            }
            else
            {
                Logger.Error(responseText);
                result.ErrorMessage = "网络错误，请稍后重试";
            }
            return result;
        }

        public async static Task<ApiResult<T>> ParseTResultAsync<T>(this HttpResponseMessage response, bool checkStatusAndMessage)
        {
            SetCookie(response);

            ApiResult<T> result = new ApiResult<T>();
            string responseText = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    if (responseText.Contains("StatusCode") && responseText.Contains("Message") && responseText.Contains("Data"))
                    {
                        if ((responseText.Contains("\"StatusCode\":0") && responseText.Contains("\"Message\":\"\"")))
                        {
                            string newDAta = responseText.Replace("{\"StatusCode\":0,\"Message\":\"\",\"Data\":", "").Replace("}]}", "}]");
                            result.Data = JsonConvert.DeserializeObject<T>(newDAta);
                        }
                        else
                        {
                            var obj = new { Message = "", StatusCode = 0 };
                            obj = JsonConvert.DeserializeAnonymousType(responseText, obj);
                            result.ErrorMessage = obj.Message;
                            result.ErrorCode = obj.StatusCode;
                        }

                    }
                    else
                    {
                        result.Data = JsonConvert.DeserializeObject<T>(responseText);
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Logger.Info(responseText);

                    result.ErrorMessage = "网络错误(" + response.StatusCode + ")，请稍后重试";
                }
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                try
                {
                    var obj = new { message = "", exceptionMessage = "", exceptionType = "" };
                    obj = JsonConvert.DeserializeAnonymousType(responseText, obj);
                    result.ErrorMessage = obj.exceptionMessage;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Logger.Info(responseText);

                    result.ErrorMessage = "网络错误(" + response.StatusCode + ")，请稍后重试";
                }
            }
            else
            {
                Logger.Error(responseText);
                result.ErrorMessage = "网络错误，请稍后重试";
            }
            return result;
        }

        public async static Task<ApiResult<string>> ParseStringAsync(this HttpResponseMessage response)
        {
            SetCookie(response);

            ApiResult<string> result = new ApiResult<string>();
            string responseText = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    if (responseText.Contains("ErrorCode") && responseText.Contains("ErrorMessage"))
                    {
                        var obj = new { ErrorMessage = "", ErrorCode = 0 };
                        obj = JsonConvert.DeserializeAnonymousType(responseText, obj);
                        result.ErrorMessage = obj.ErrorMessage;
                        result.ErrorCode = obj.ErrorCode;
                    }
                    else
                    {
                        result.Data = responseText;
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Logger.Info(responseText);

                    result.ErrorMessage = "网络错误(" + response.StatusCode + ")，请稍后重试";
                }
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                try
                {
                    var obj = new { message = "", exceptionMessage = "", exceptionType = "" };
                    obj = JsonConvert.DeserializeAnonymousType(responseText, obj);
                    result.ErrorMessage = obj.exceptionMessage;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    Logger.Info(responseText);

                    result.ErrorMessage = "网络错误(" + response.StatusCode + ")，请稍后重试";
                }
            }
            else
            {
                Logger.Info(responseText);

                result.ErrorMessage = "网络错误，请稍后重试";
            }
            return result;
        }

        private static void SetCookie(HttpResponseMessage response)
        {
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

        /// <summary>
        /// 把Api写的Cookie转成UI Cookie
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        private static HttpCookie GetHttpCookie(string cookie)
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
    }

    public class ApiResult<T>
    {
        public bool HasError { get; set; }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                if (!HasError)
                    HasError = true;
            }
        }

        private int _errorCode;
        public int ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
                if (!HasError)
                    HasError = true;
            }
        }

        public T Data { get; set; }
    }
}