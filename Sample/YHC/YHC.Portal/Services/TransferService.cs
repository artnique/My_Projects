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
    public class TransferService : BaseService
    {
        public async Task<ApiResult<PagerFrontViewModel<UserTransfer>>> GetTransferList(string userName, DateTime? beginTime, DateTime? endTime,int? type, Int32? status,string gamePlatform, Int32? pageIndex, Int32? pageSize)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("beginTime", beginTime.HasValue ? beginTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("endTime", endTime.HasValue ? endTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("type", type.HasValue ? type.Value.ToString() : string.Empty);
            parm.Add("status", status.HasValue ? status.Value.ToString() : string.Empty);
            parm.Add("gamePlatform", gamePlatform);
            parm.Add("pageIndex", pageIndex.GetValueOrDefault(0).ToString());
            parm.Add("pageSize", pageSize.GetValueOrDefault(10).ToString());
            var request = GetRequest("api/Transfer/GetTransferList", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<UserTransfer>>();
        }

        public async Task<ApiResult<decimal>> GetTotalTransfer(string userName, DateTime? beginTime, DateTime? endTime, int? type, Int32? status, string gamePlatform)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("beginTime", beginTime.HasValue ? beginTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("endTime", endTime.HasValue ? endTime.Value.ToString("yyy-MM-dd mm:HH:ss") : string.Empty);
            parm.Add("type", type.HasValue ? type.Value.ToString() : string.Empty);
            parm.Add("status", status.HasValue ? status.Value.ToString() : string.Empty);
            parm.Add("gamePlatform", gamePlatform);

            var request = GetRequest("api/Transfer/GetTotalTransfer", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<decimal>();
        }

        public async Task<ApiResult<bool>> ToGame(string userName,decimal amount,string gamePlatform,string ip, bool isDemo)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("amount", amount.ToString());
            parm.Add("gamePlatform", gamePlatform);
            parm.Add("ip", ip);
            parm.Add("isDemo", isDemo.ToString());

            var request = PostRequest("api/Transfer/DoTransferToGame", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }

        public async Task<ApiResult<bool>> FromGame(string userName, decimal amount, string gamePlatform, string ip, bool isDemo)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("userName", userName);
            parm.Add("amount", amount.ToString());
            parm.Add("gamePlatform", gamePlatform);
            parm.Add("ip", ip);
            parm.Add("isDemo", isDemo.ToString());

            var request = PostRequest("api/Transfer/DoTransferFromGame", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<bool>();
        }
    }
}