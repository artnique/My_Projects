using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using YHC.Portal.Core;

namespace YHC.Portal.Models
{
    public class UserDeposit
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 会员ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名，从EUser实体获取
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 存款顺序
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        public int Type { get; set; }

        /// <summary>
        /// 支付类型文本
        /// </summary>
        public String TypeText
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态文本
        /// </summary>
        public String StatusText
        {
            get
            {
                String text = string.Empty;
                if (this.Status == (int)UserDepositStatus.NotPay)
                {
                    text = "未支付";
                }
                else if (this.Status == (int)UserDepositStatus.Audit)
                {
                    text = "审核";
                }
                else if (this.Status == (int)UserDepositStatus.Refuse)
                {
                    text = "拒绝";
                }
                else if (this.Status == (int)UserDepositStatus.Success)
                {
                    text = "完成";
                }
                else if (this.Status == (int)UserDepositStatus.Processing)
                {
                    text = "处理中";
                }
                else if (this.Status == (int)UserDepositStatus.Cancel)
                {
                    text = "取消";
                }
                return text;
            }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
