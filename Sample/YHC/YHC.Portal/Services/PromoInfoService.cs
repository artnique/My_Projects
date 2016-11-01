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
    public class PromoInfoService : BaseService
    {
        public async Task<ApiResult<IList<PromoInfoType>>> GetAllType()
        {
            var request = GetRequest("api/Promo/GetAllType");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<PromoInfoType>>();
        }

        public async Task<ApiResult<IList<PromoInfoType>>> GetAllType(bool? status)
        {
            var request = GetRequest("api/Promo/GetAllType?status=" + status);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<PromoInfoType>>();
        }

        public async Task<ApiResult<PromoInfo>> GetInfo(int id)
        {
            var request = GetRequest("api/Promo/GetInfo?id=" + id);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PromoInfo>();
        }

        public async Task<ApiResult<PagerFrontViewModel<PromoInfo>>> GetList(int? type,int pageIndex, int pageSize)
        {
            var request = GetRequest("api/Promo/GetList?type=" + type + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<PromoInfo>>();
        }
    }
}