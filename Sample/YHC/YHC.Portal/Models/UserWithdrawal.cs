using YHC.Portal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 会员提款
    /// </summary>
    public class UserWithdrawal
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 会员信息
        /// </summary>
        public int UserId { get; set; }

        public String UserName { get; set; }

        /// <summary>
        /// 提款金额
        /// </summary>
        public Decimal Amount { get; set; }

        private Decimal _actualAmount;
        /// <summary>
        /// 实际扣款
        /// </summary>
        public Decimal ActualAmount
        {
            get
            {
                return this.Amount - this.MgrFee - this.PrefFee - this.TransferFee;
            }
            set { _actualAmount = value; }

        }

        /// <summary>
        /// 行政扣款
        /// </summary>
        public Decimal MgrFee { get; set; }

        /// <summary>
        /// 优惠扣款
        /// </summary>
        public Decimal PrefFee { get; set; }

        /// <summary>
        /// 转账扣款
        /// </summary>
        public Decimal TransferFee { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

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
                string text = string.Empty;
                switch (this.Status)
                {
                    case (int)UserWithdrawalStatus.Processing:
                        text = "处理中";
                        break;
                    case (int)UserWithdrawalStatus.AuditPass:
                        text = "初审通过";
                        break;
                    case (int)UserWithdrawalStatus.AuditRefuse:
                        text = "初审拒绝";
                        break;
                    case (int)UserWithdrawalStatus.AuditReviewPass:
                        text = "复审通过";
                        break;
                    case (int)UserWithdrawalStatus.AuditReviewRefuse:
                        text = "复审拒绝";
                        break;
                }

                return text;
            }
        }
    }
}
