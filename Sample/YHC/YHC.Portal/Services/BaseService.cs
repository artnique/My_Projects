using YHC.Portal.Helper;
using YHC.Portal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace YHC.Portal.Services
{
    public class BaseService : IDisposable
    {
        private HttpClientHandler _clientHandler;
        private HttpClient _client;

        protected HttpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _clientHandler = new HttpClientHandler { UseCookies = false };
                    _client = new HttpClient(_clientHandler) { BaseAddress = ConfigHelper.ApiBaseAddress };
                }
                return _client;
            }
        }

        private JavaScriptSerializer _jsSerializer;

        protected JavaScriptSerializer JSSerializer
        {
            get
            {
                if (_jsSerializer == null)
                {
                    _jsSerializer = new JavaScriptSerializer();
                }
                return _jsSerializer;
            }
        }

        public void Dispose()
        {
            if (_client != null)
            {
                _client.Dispose();
                _client = null;
            }
            if (_clientHandler != null)
            {
                _clientHandler.Dispose();
                _clientHandler = null;
            }
            if (_jsSerializer != null)
            {
                _jsSerializer = null;
            }
        }

        protected HttpRequestMessage CreateRequest(string url, HttpMethod httpMethod, NameValueCollection parameters = null)
        {
            FormUrlEncodedContent content = null;
            if (parameters != null && parameters.Count > 0)
            {
                if (httpMethod == HttpMethod.Get)
                {
                    string[] tmp = new string[parameters.Count];
                    for (int i = 0; i < parameters.Keys.Count; i++)
                    {
                        string key = parameters.Keys[i];
                        tmp[i] = string.Format("{0}={1}", key, parameters[key]);
                    }
                    if (url.IndexOf('?') != -1)
                    {
                        url += "&" + string.Join("&", tmp);
                    }
                    else
                    {
                        url += "?" + string.Join("&", tmp);
                    }
                }
                else
                {
                    List<KeyValuePair<string, string>> List = new List<KeyValuePair<string, string>>();
                    foreach (string key in parameters.Keys)
                    {
                        List.Add(new KeyValuePair<string, string>(key, parameters[key]));
                    }
                    content = new FormUrlEncodedContent(List);
                }
            }
            var request = new HttpRequestMessage(httpMethod, url);
            var cookies = HttpContext.Current.Request.Headers.Get("Cookie");
            request.Headers.Add("Cookie", cookies);
            if (content != null)
            {
                request.Content = content;
            }
            //LogHelper.Info(typeof(BaseService), Client.BaseAddress + request.RequestUri.ToString());
            return request;
        }

        protected HttpRequestMessage GetRequest(string url, NameValueCollection parameters = null)
        {
            return CreateRequest(url, HttpMethod.Get, parameters);
        }

        protected HttpRequestMessage PostRequest(string url, NameValueCollection parameters = null)
        {
            return CreateRequest(url, HttpMethod.Post, parameters);
        }
    }
}