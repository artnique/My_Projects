using System;
using System.Collections.Generic;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 第三方支付平台银行
    /// </summary>
    [Serializable]
    public class VThirdPayPlatformBank
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 关联的第三方支付
        /// </summary>
        public ThirdPayPlatform ThirdPayPlatform { get; set; }

        /// <summary>
        /// 第三方支付中的银行编辑
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Css名称
        /// </summary>
        public string CssName { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否启用,TRUE启用
        /// </summary>
        public bool Enabled { get; set; }

        public String EnabledText
        {
            get
            {
                return this.Enabled ? "启用" : "禁用";
            }
        }

        /// <summary>
        /// 创建人
        /// </summary>
        public VAdmin Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}