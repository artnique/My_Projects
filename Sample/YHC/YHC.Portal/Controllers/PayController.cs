using YHC.Portal.Filters;
using YHC.Portal.Helper;
using YHC.Portal.Models;
using YHC.Portal.Services;
using YHC.Portal.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YHC.Portal.Controllers
{
    public class PayController : BaseAsyncController
    {
        private PayService _payService = null;
        public PayService PayService
        {
            get
            {
                if (_payService == null)
                {
                    _payService = new PayService();
                }
                return _payService;
            }
        }

        [CheckLogin]
        public async Task<ActionResult> DoPay(Int32? id, String payPlatform, String payMerCode, String payBankCode, String payBankName,
            Decimal? online_amount, String online_prefKey, String faceCode, String faceNo, String facePassword)
        {
            if (string.IsNullOrWhiteSpace(payPlatform))
            {
                return Content("请选择支付平台");
            }
            if (online_amount.HasValue == false || online_amount.Value <= 0)
            {
                return Content("请输入大于0的存款金额");
            }
            var user = LoginHelper.GetUser();
            String ip = Request.GetClientIP();
            var result = await PayService.DoPay(id, user.UserName, payPlatform, payMerCode, payBankCode, payBankName, online_amount.Value, online_prefKey, ip, faceCode, faceNo, facePassword);
            if (result.HasError)
            {
                return Content(result.ErrorMessage);
            }
            return Content(result.Data);

        }

    }
}
