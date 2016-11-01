using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class SpareDomain
    {
        /// <summary>
        /// 备用网址
        /// </summary>
        public string DomainUrl { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? Status { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; }
    }
}