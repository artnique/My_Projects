using YHC.Portal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class Recommend
    {
        public string UserName { get; set; }

        public string TrueName { get; set; }

        /// <summary>
        /// 会员注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 累计存款
        /// </summary>
        public decimal TotalDeposit { get; set; }

        /// <summary>
        /// 推荐费
        /// </summary>
        public decimal ReferralFee { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 状态文本
        /// </summary>
        public String StatusText
        {
            get
            {
                RecommendStatus status = (RecommendStatus)this.Status;
                String text = "初始";
                if (status == RecommendStatus.AUDIT)
                {
                    text = "审核";
                }
                else if (status == RecommendStatus.REFUSE)
                {
                    text = "拒绝";
                }
                else if (status == RecommendStatus.COMPLETED)
                {
                    text = "完成";
                }
                else if (status == RecommendStatus.PROCESSING)
                {
                    text = "处理中";
                }
                else if (status == RecommendStatus.INIT)
                {
                    text = "初始";
                }
                return text;
            }
        }

    }
}