using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Collections.Specialized;
using YHC.Portal.Helper;
using System.Net.Http;
using System.Net;
using YHC.Portal.Models;
using YHC.Portal.Core;
using YHC.Portal.Extensions;

namespace YHC.Portal.Services
{
    public class BetService : BaseService
    {
        public async Task<ApiResult<PagerFrontViewModel<VBet>>> GetBetList(int userId, DateTime startTime, DateTime endTime, string gamePlatform, int? pageIndex, int? pageSize)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("startTime", startTime.ToString("yyyy-MM-dd 00:00:00"));
            parm.Add("endTime", endTime.ToString("yyyy-MM-dd 00:00:00"));
            parm.Add("gamePlatform", gamePlatform);
            parm.Add("pageIndex", pageIndex.GetValueOrDefault().ToString());
            parm.Add("pageSize", pageSize.GetValueOrDefault(10).ToString());

            var request = GetRequest("api/bet/GetBetList", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<PagerFrontViewModel<VBet>>();
        }

        public async Task<ApiResult<VBet>> GetTotalBet(int userId, DateTime startTime, DateTime endTime, string gamePlatform)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("startTime", startTime.ToString("yyyy-MM-dd 00:00:00"));
            parm.Add("endTime", endTime.ToString("yyyy-MM-dd 00:00:00"));
            parm.Add("gamePlatform", gamePlatform);

            var request = GetRequest("api/bet/GetTotalBet", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<VBet>();
        }

        public async Task<VBet> GetLastBet()
        {
            VBet result = null;
            var request = GetRequest("api/bet/GetLastBet=");
            var response = await Client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                result = JSSerializer.Deserialize<VBet>(responseText);
            }
            return result;
        }
    }
}