using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class Promo
    {
        /// <summary>
        /// Id
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 优惠码
        /// </summary>
        public String PreferentialKey { get; set; }

        /// <summary>
        /// 优惠名称
        /// </summary>
        public String PreferentialTitle { get; set; }

        /// <summary>
        /// 优惠类型
        /// </summary>
        public int PreferentialType { get; set; }

        /// <summary>
        /// 投注额要求
        /// </summary>
        public String Description { get; set; }
    }
}