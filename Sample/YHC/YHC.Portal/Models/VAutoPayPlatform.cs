using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHC.Portal.Models
{
    public class VAutoPayPlatform
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 平台名称
        /// </summary>
        public string PlatformName { get; set; }

        /// <summary>
        /// 商户Id，写死的
        /// </summary>
        public string CustId { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string CustName { get; set; }

        public string TrustUser { get; set; }

        public string TrustPass { get; set; }

        /// <summary>
        /// 通知接口接口地址
        /// </summary>
        public string NoteUrl { get; set; }

        /// <summary>
        /// 存款地址
        /// </summary>
        public string DepositUrl { get; set; }

        /// <summary>
        /// 存款完成接口地址
        /// </summary>
        public string DepositFinishUrl { get; set; }

        /// <summary>
        /// 提款地址
        /// </summary>
        public string WithdrawalUrl { get; set; }

        /// <summary>
        /// 提款密码(已加密)
        /// </summary>
        public string WithdrawalPassword { get; set; }

        /// <summary>
        /// 银行地址
        /// </summary>
        public string BankUrl { get; set; }

        /// <summary>
        /// 付款方式地址
        /// </summary>
        public string PayWayUrl { get; set; }

        /// <summary>
        /// 城市地址
        /// </summary>
        public string ProvinceUrl { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 存款随机加密字符串
        /// </summary>
        public String DepositHashStr { get; set; }

        /// <summary>
        /// 存款卡号随机加密字符串
        /// </summary>
        public String DepositCardHashStr { get; set; }

        /// <summary>
        /// 存款通知随机加密字符串
        /// </summary>
        public String DepositFinishHashStr { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
