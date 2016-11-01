using YHC.Portal.Core;
using YHC.Portal.Extensions;
using YHC.Portal.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace YHC.Portal.Services
{
    public class CommonService : BaseService
    {

        public Footer GetFooter(string key)
        {
            var request = GetRequest("api/News/GetNewsByKey?key=" + key);
            var response = Client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseText = response.Content.ReadAsStringAsync().Result;
                return JSSerializer.Deserialize<Footer>(responseText);
            }
            return null;
        }

        public async Task<ApiResult<Footer>> GetFooterByKey(string key)
        {
            var request = GetRequest("api/News/GetNewsByKey?key=" + key);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<Footer>();
        }

        public PagerFrontViewModel<Ad> GetFooterImages()
        {
            var request = GetRequest("api/News/GetAds?type=yc_footer_image&pageSize=20&pageIndex=0");
            var response = Client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseText = response.Content.ReadAsStringAsync().Result;
                return JSSerializer.Deserialize<PagerFrontViewModel<Ad>>(responseText);
            }
            return null;
        }
    }
}