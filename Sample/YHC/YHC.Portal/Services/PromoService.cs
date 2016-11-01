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
    public class PromoService : BaseService
    {
        public async Task<ApiResult<PagerFrontViewModel<Bonus>>> GetBonusList(string userName, DateTime? beginTime, DateTime? endTime, int? type,Int32? pageIndex, Int32? pageSize)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("beginTime", beginTime.HasValue ? beginTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("endTime", endTime.HasValue ? endTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("type", type.HasValue ? type.Value.ToString() : string.Empty);
            parm.Add("pageIndex", pageIndex.GetValueOrDefault(0).ToString());
            parm.Add("pageSize", pageSize.GetValueOrDefault(10).ToString());
            var request = GetRequest("api/Promo/GetBonusList", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<Bonus>>();
        }

        public async Task<ApiResult<decimal>> GetTotalBonus(string userName, DateTime? beginTime, DateTime? endTime, int? type)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("beginTime", beginTime.HasValue ? beginTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("endTime", endTime.HasValue ? endTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("type", type.HasValue ? type.Value.ToString() : string.Empty);
            var request = GetRequest("api/Promo/GetTotalBonus", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<decimal>();
        }

        public async Task<ApiResult<IList<BonusType>>> GetBonusTypeList()
        {
            var request = GetRequest("api/Promo/GetBonusTypeList");
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<IList<BonusType>>(); 
        }

        public async Task<ApiResult<PagerFrontViewModel<UserWallet>>> GetWalletList(int userId, bool? isInvalid, UserWalletStatus? status, Int32? pageIndex, Int32? pageSize)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userId", userId.ToString());
            parm.Add("status", status.HasValue ? ((int)status.Value).ToString() : string.Empty);
            //parm.Add("type", type.HasValue ? type.Value.ToString() : string.Empty);
            parm.Add("isInvalid", isInvalid.HasValue ? isInvalid.Value.ToString() : string.Empty);
            parm.Add("pageIndex", pageIndex.GetValueOrDefault(0).ToString());
            parm.Add("pageSize", pageSize.GetValueOrDefault(10).ToString());
            var request = GetRequest("api/Promo/GetWalletList", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<UserWallet>>();
        }

        public async Task<ApiResult<int>> GetWalletLockCount(int userId)
        {
            var request = GetRequest("api/Promo/GetWalletLockCount?userId=" + userId);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<int>();
        }

        public async Task<ApiResult<int>> GetWalletInvalidCount(int userId)
        {
            var request = GetRequest("api/Promo/GetWalletInvalidCount?userId=" + userId);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<int>();
        }

        public async Task<ApiResult<int>> GetWalletCount(int userId,int status)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userId", userId.ToString());
            parm.Add("status", status.ToString());

            var request = GetRequest("api/Promo/GetWalletCount",parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<int>();
        }

        public async Task<ApiResult<bool>> WalletApply(int id, int userId)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("id", id.ToString());
            parm.Add("userId", userId.ToString());

            var request = PostRequest("api/Promo/WalletApply", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<bool>> WalletCancel(int id, int userId)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("id", id.ToString());
            parm.Add("userId", userId.ToString());

            var request = PostRequest("api/Promo/WalletCancel", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<RecommendRebateInfo>> GetRecommendRebateInfo(int userId)
        {
            var request = GetRequest("api/Promo/GetRecommendRebateInfo?userId=" + userId);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<RecommendRebateInfo>();
        }

        public async Task<ApiResult<PagerFrontViewModel<Recommend>>> GetRecommendList(int userId,DateTime? beginTime, DateTime? endTime, Int32? pageIndex, Int32? pageSize)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userId", userId.ToString());
            parm.Add("beginTime", beginTime.HasValue ? beginTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("endTime", endTime.HasValue ? endTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("pageIndex", pageIndex.GetValueOrDefault(0).ToString());
            parm.Add("pageSize", pageSize.GetValueOrDefault(10).ToString());
            var request = GetRequest("api/Promo/GetRecommendList", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<Recommend>>();
        }
    }
}