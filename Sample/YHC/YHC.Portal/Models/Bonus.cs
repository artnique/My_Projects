using YHC.Portal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class Bonus
    {
        public string BonusName { get; set; }

        public decimal Amount { get; set; }

        public int Status { get;set; }

        /// <summary>
        /// 状态文本
        /// </summary>
        public string StatusText
        {
            get
            {
                String text = "处理中";
                //if (status == UserBonusStatus.Audit)
                //{
                //    text = "审核";
                //}
                if (this.Status == (int)UserBonusStatus.Refuse)
                {
                    text = "拒绝";
                }
                else if (this.Status == (int)UserBonusStatus.Success)
                {
                    text = "完成";
                }
                else if (this.Status == (int)UserBonusStatus.Processing)
                {
                    text = "处理中";
                }
                else if (this.Status == (int)UserBonusStatus.Cancel)
                {
                    text = "取消";
                }
                return text;
            }
        }

        public DateTime CreateTime { get; set; }
    }
}