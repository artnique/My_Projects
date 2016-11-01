using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 第三方支付(商户)
    /// </summary>
    [Serializable]
    public class ThirdPay
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public String MerName { get; set; }

        public string MerCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Boolean Status { get; set; }

        /// <summary>
        /// 状态文本
        /// </summary>
        public String StatusText
        {
            get
            {
                return this.Status ? "启用" : "禁用";
            }
        }

        /// <summary>
        /// 是否显示
        /// </summary>
        public Boolean Show { get; set; }

        /// <summary>
        /// 商城url转向域名，格式：http://www.test.com
        /// </summary>
        public String MallDomain { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public Int32 Sort { get; set; }

        /// <summary>
        /// 第三方支付平台银行
        /// </summary>
        public IList<ThridPayBank> BankList { get; set; }

        /// <summary>
        /// 第三方支付平台 Code
        /// </summary>
        public string ThirdPayCode { get; set; }

        public string ThirdPayLogoImg { get; set; }
    }
}
        
        