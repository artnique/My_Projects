using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 银行
    /// </summary>
    [Serializable]
    public class Bank
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public String BankName { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public String LogoImg { get; set; }

        public String Url { get; set; }
    }
}