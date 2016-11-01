using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class RecommendRebateInfo
    {
        /// <summary>
        /// 被邀请人数(打开推荐链接人数)
        /// </summary>
        public int RecommendUrlQuantity { get; set; }

        /// <summary>
        /// 注册人数
        /// </summary>
        public int RegisterQuantity { get; set; }

        /// <summary>
        /// 成功推荐人数
        /// </summary>
        public int RecommendSuccessQuantity { get; set; }

        /// <summary>
        /// 总推荐优惠
        /// </summary>
        public decimal RecommendTotalBouns { get; set; }

        /// <summary>
        /// 推荐码
        /// </summary>
        public string RecommendCode { get; set; }
    }
}