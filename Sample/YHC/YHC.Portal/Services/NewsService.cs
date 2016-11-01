using YHC.Portal.Core;
using YHC.Portal.Extensions;
using YHC.Portal.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace YHC.Portal.Services
{
    public class NewsService : BaseService
    {
        public async Task<ApiResult<IList<NewsCategory>>> GetSubcategories(string category)
        {
            var request = GetRequest("api/News/GetSubcategories?category=" + category);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<NewsCategory>>();
        }
        public async Task<ApiResult<PagerFrontViewModel<News>>> GetAllNotices()
        {
            var request = GetRequest("api/News/GetNotices?pageIndex=&pageSize=&broadcasted=");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<News>>();
        }

        public async Task<ApiResult<PagerFrontViewModel<News>>> GetNotices(Int32? pageIndex, Int32? pageSize, Boolean? broadcasted)
        {
            var request = GetRequest("api/News/GetNotices?pageIndex=" + pageIndex + "&pageSize=" + pageSize + "&broadcasted=" + broadcasted);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<News>>();
        }

        public async Task<ApiResult<News>> GetPageFooter()
        {
            var request = GetRequest("api/News/GetPageFooter");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<News>();
        }

        public async Task<ApiResult<PagerFrontViewModel<News>>> GetNewsList(string category, int? pageSize, int? pageIndex)
        {
            var request = GetRequest("api/News/GetNews?category=" + category + "&pageSize=" + pageSize + "&pageIndex=" + pageIndex);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<News>>();
        }

        public async Task<ApiResult<News>> GetNews(string category)
        {
            var request = GetRequest("api/News/GetNewsByCategory?category=" + category);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<News>();
        }

        public async Task<ApiResult<IList<News>>> GetBroadcast(int count, DateTime? sendTime)
        {
            var request = GetRequest("api/News/GetBroadcast?count=" + count + "&sendTime=" + sendTime);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<News>>();
        }

        public async Task<ApiResult<News>> GetNewsByKey(string key)
        {
            var request = GetRequest("api/News/GetNewsByKey?key=" + key);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<News>();
        }

        public async Task<ApiResult<PagerFrontViewModel<Ad>>> GetAds(string type, int? pageSize, int? pageIndex)
        {
            var request = GetRequest("api/News/GetAds?type=" + type + "&pageSize=" + pageSize + "&pageIndex=" + pageIndex);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<Ad>>();
        }

        public async Task<ApiResult<SEO>> GetSEO(string pageNo)
        {
            var request = GetRequest("api/News/GetSEO?pageNo=" + pageNo);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<SEO>();
        }
    }
}