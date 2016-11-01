using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class InspectWithdrawalItem
    {
        /// <summary>
        /// 常态性稽核扣款费用
        /// </summary>
        public Decimal NormalityInspectFee { get; set; }

        /// <summary>
        /// 优惠扣款（如果当前不符合条件，则扣除所有优惠）
        /// </summary>
        public Decimal PrefInspectFee { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime
        {
            get;
            set;
        }

        /// <summary>
        /// 结束时间（如果没有下一条记录，则为当前时间，否则为下一条记录的创建时间）
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// 区间派彩总额
        /// </summary>
        public Decimal TotalPayOut { get; set; }

        /// <summary>
        /// 各游戏派彩列表（程序中遍历用户所该接口获取），
        /// 格式：AVIA:100,PT:200.34,MG:123.12。
        /// ViewModel分把它变成keyvalue
        /// </summary>
        public String GamePayOutAmounts
        {
            get;
            set;
        }

        /// <summary>
        /// 有效投注额-调整后，根据多条记录来调整的
        /// </summary>
        public Decimal TotalBetAmountOfRevise { get; set; }

        #region 优惠稽核相关

        /// <summary>
        /// 优惠有效投注额（根据设置计算，即要求优惠的投注流水要多少，不需要稽核时为横线）
        /// </summary>
        public String PrefInspectNeedBetAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 存款金额（除存款发优惠外，其它发优惠时，这里均为0）
        /// </summary>
        public Decimal DepositAmount { get; set; }

        /// <summary>
        /// 优惠金额（存款不放优惠时为0）
        /// </summary>
        public Decimal PrefAmount { get; set; }

        /// <summary>
        /// 优惠活动名称
        /// </summary>
        public String PrefName { get; set; }

        /// <summary>
        /// 稽核来源类型
        /// </summary>
        public int Type { get; set; }

        #endregion

        #region 常态性稽核

        /// <summary>
        /// 常态性稽核有效投注额（即需要达到这个投注额，就不需要扣款）
        /// </summary>
        public String NormalityInspectNeedBetAmount { get; set; }

        #endregion

    }
}