using YHC.Portal.Models;
using YHC.Portal.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using YHC.Portal.Core;

namespace YHC.Portal.Services
{
    public class WithdrawalService : BaseService
    {
        public async Task<UserWithdrawal> GetLastWithdrawal()
        {
            UserWithdrawal result = null;
            var request = GetRequest("api/Withdrawal/GetLastWithdrawal");
            var response = await Client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                result = JSSerializer.Deserialize<UserWithdrawal>(responseText);
            }
            return result;
        }

        public async Task<ApiResult<PagerFrontViewModel<UserWithdrawal>>> GetWithdrawalList(string userName, DateTime? beginTime, DateTime? endTime, Int32? status,Int32? pageIndex, Int32? pageSize)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("beginTime", beginTime.HasValue ? beginTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("endTime", endTime.HasValue ? endTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("status", status.HasValue ? status.Value.ToString() : string.Empty);
            parm.Add("pageIndex", pageIndex.GetValueOrDefault(0).ToString());
            parm.Add("pageSize", pageSize.GetValueOrDefault(10).ToString());
            var request = GetRequest("api/Withdrawal/GetWithdrawalList", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<UserWithdrawal>>();
        }

        public async Task<ApiResult<decimal>> GetTotalWithdrawal(string userName, DateTime? beginTime, DateTime? endTime, Int32? status)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("beginTime", beginTime.HasValue ? beginTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("endTime", endTime.HasValue ? endTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("status", status.HasValue ? status.Value.ToString() : string.Empty);
            var request = GetRequest("api/Withdrawal/GetTotalWithdrawal", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<decimal>();
        }

        public async Task<ApiResult<bool>> AddUserBank(string userName,int bankId, string province, string city, string branchName,string accountName, string accountNo , String withdrawalPwd)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("bankId", bankId.ToString());
            parm.Add("province", province);
            parm.Add("city", city);
            parm.Add("branchName", branchName);
            parm.Add("accountNo", accountNo);
            parm.Add("accountName", accountName);
            parm.Add("withdrawalPwd", withdrawalPwd);

            var request = PostRequest("api/Withdrawal/AddUserBank", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<string>> AddWithdrawal(String userName, Int32 bankAccountId, Decimal amount, String remark, String ip, Decimal mgrFee, Decimal prefFee, String withdrawalPwd)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("bankAccountId", bankAccountId.ToString());
            parm.Add("amount", amount.ToString());
            parm.Add("remark", remark);
            parm.Add("ip", ip);
            parm.Add("mgrFee", mgrFee.ToString());
            parm.Add("prefFee", prefFee.ToString());
            parm.Add("withdrawalPwd", withdrawalPwd);

            var request = PostRequest("api/Withdrawal/AddWithdrawal", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<string>();
        }

        public async Task<ApiResult<InspectWithdrawal>> GetNotInspectes(string userName, DateTime inspectTime)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("inspectTime", inspectTime.ToString("yyyy-MM-dd HH:mm:ss"));

            var request = GetRequest("api/Withdrawal/GetNotInspectes", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<InspectWithdrawal>();
        }
    }
}