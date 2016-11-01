using YHC.Portal.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace YHC.Portal.Services
{
    public class PayService : BaseService
    {
        public async Task<ApiResult<string>> DoPay(Int32? id,String userName, String payPlatform, String payMerCode, String payBankCode, String payBankName,
            Decimal amount, String prefKey,string ip, String faceCode, String faceNo, String facePassword)
        {
            NameValueCollection parm = new NameValueCollection();
            parm.Add("Id", id.HasValue ? id.Value.ToString() : string.Empty);
            parm.Add("userName", userName);
            parm.Add("payPlatform", payPlatform);
            parm.Add("payMerCode", payMerCode);
            parm.Add("payBankCode", payBankCode);
            parm.Add("payBankName", payBankName);
            parm.Add("amount", amount.ToString());
            parm.Add("faceCode", faceCode);
            parm.Add("faceNo", faceNo);
            parm.Add("facePassword", facePassword);
            parm.Add("ip", ip);
            parm.Add("prefKey", prefKey);

            var request = PostRequest("api/Pay/DoPay", parm);
            var response = await Client.SendAsync(request);
            return await response.ParseTResultAsync<string>();
        }
    }
}