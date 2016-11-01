using YHC.Portal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class UserWallet
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 优惠Id
        /// </summary>
        public String SourceId { get; set; }

        /// <summary>
        /// 优惠名称
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// 钱包来源
        /// </summary>
        public int Type { get; set; }

        public String TypeText
        {
            get
            {
                var type = (UserWalletType)this.Type;
                switch (type)
                {
                    case UserWalletType.Preferential:
                        return "优惠";
                    case UserWalletType.Lucky:
                        return "抽奖";
                    case UserWalletType.Perfect:
                        return "完善资料";
                    case UserWalletType.Recommend:
                        return "推荐返利";
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        public String StatusText
        {
            get
            {
                var status = (UserWalletStatus)this.Status;
                switch (status)
                {
                    case UserWalletStatus.Init:
                        return "待领取";
                    case UserWalletStatus.Apply:
                        return "锁定中";
                    case UserWalletStatus.Send:
                        return "已领取";
                    case UserWalletStatus.Cancel:
                        return "已取消";
                    default:
                        return string.Empty;
                }
            }
        }

        /// <summary>
        /// 过期日期
        /// </summary>
        public virtual DateTime? ValidTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 优惠时间
        /// </summary>
        public virtual DateTime OperatTime { get; set; }

        /// <summary>
        /// 申请截止日期
        /// </summary>
        public DateTime ApplyDueTime { get; set; }

        /// <summary>
        /// 是否过期，无效
        /// </summary>
        public bool IsInvalid { get; set; }

        /// <summary>
        /// 锁定天数
        /// </summary>
        public int LockDays { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 发放时间
        /// </summary>
        public DateTime? SendTime { get; set; }

        public DateTime? CancelTime { get; set; }

        /// <summary>
        /// 申请截止天数
        /// </summary>
        public decimal ApplyDueDays { get; set; }

        /// <summary>
        /// 需要达到的稽核流水
        /// </summary>
        public decimal InspectRealBet { get; set; }

        /// <summary>
        /// 更新区间内的有效投注
        /// </summary>
        public decimal RangeRealBet { get; set; }

        public decimal SurplusBet { get { return InspectRealBet > RangeRealBet ? InspectRealBet - RangeRealBet : 0; } }
    }
}