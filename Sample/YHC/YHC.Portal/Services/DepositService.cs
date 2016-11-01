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
    public class DepositService : BaseService
    {
        public async Task<UserDeposit> GetLastDeposit()
        {
            UserDeposit result = null;
            var request = GetRequest("api/Deposit/GetLastDeposit");
            var response = await Client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                result = JSSerializer.Deserialize<UserDeposit>(responseText);
            }
            return result;
        }

        public async Task<ApiResult<PagerFrontViewModel<UserDeposit>>> GetDepositList(DateTime? beginTime, DateTime? endTime, Int32? status, Int32? type, Int32? pageIndex, Int32? pageSize)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("beginTime", beginTime.HasValue ? beginTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("endTime", endTime.HasValue ? endTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("status", status.HasValue ? status.Value.ToString() : string.Empty);
            parm.Add("type", type.HasValue ? type.Value.ToString() : string.Empty);
            parm.Add("pageIndex", pageIndex.GetValueOrDefault(0).ToString());
            parm.Add("pageSize", pageSize.GetValueOrDefault(10).ToString());
            var request = GetRequest("api/Deposit/GetDepositList", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync <PagerFrontViewModel<UserDeposit>>();
        }

        public async Task<ApiResult<decimal>> GetTotalDeposit(string userName, DateTime? beginTime, DateTime? endTime, Int32? status, Int32? type)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("beginTime", beginTime.HasValue ? beginTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("endTime", endTime.HasValue ? endTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("status", status.HasValue ? status.Value.ToString() : string.Empty);
            parm.Add("type", type.HasValue ? type.Value.ToString() : string.Empty);
            var request = GetRequest("api/Deposit/GetTotalDeposit", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<decimal>();
        }

        public async Task<ApiResult<string>> AddDeposit(string userName, string trueName, int bankId, decimal amount, int? transType, int depositType, string province, string city, string address, string ip, string prefKey, string accountName)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("trueName", trueName);
            parm.Add("bankId", bankId.ToString());
            parm.Add("amount", amount.ToString());
            parm.Add("province", province);
            parm.Add("city", city);
            parm.Add("transType", transType.HasValue ? transType.Value.ToString() : string.Empty);
            parm.Add("depositType", depositType.ToString());
            parm.Add("address", address);
            parm.Add("ip", ip);
            parm.Add("prefKey", prefKey);
            parm.Add("accountName", accountName);
            var request = PostRequest("api/Deposit/AddDeposit", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<string>();
        }
    }
}