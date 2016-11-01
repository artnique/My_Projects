using System;
using System.Collections.Generic;

namespace YHC.Portal.Models
{
    [Serializable]
    public class VUserGroup
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 会员数
        /// </summary>
        public int MemberCount { get; set; }

        /// <summary>
        /// 存款账号
        /// </summary>
        public IList<AdminBank> AdminBanks { get; set; }

        /// <summary>
        /// 第三方账号
        /// </summary>
        public IList<ThirdPay> ThirdPays { get; set; }

        public IList<VAutoPayPlatform> AutoPays { get; set; }

        //public String ThirdPayName
        //{
        //    get { return ThirdPays == null ? string.Empty : ThirdPays.PayName; }
        //}

        /// <summary>
        /// 银行存款单笔最大限额
        /// </summary>
        public int MaxDeposit { get; set; }

        /// <summary>
        /// 银行存款单笔最小限额
        /// </summary>
        public int MinDeposit { get; set; }

        /// <summary>
        /// 最大出款
        /// </summary>
        public int MaxWithdraw { get; set; }

        /// <summary>
        /// 最小出款
        /// </summary>
        public int MinWithdraw { get; set; }

        /// <summary>
        /// 单笔最大出款
        /// </summary>
        public int SingleMaxWithdraw { get; set; }

        /// <summary>
        /// 单笔最小出款
        /// </summary>
        public int SingleMinWithdraw { get; set; }

        /// <summary>
        /// 重复提款时数
        /// </summary>
        public int RepeatIntervalWithdrawal { get; set; }

        /// <summary>
        /// 免收手续费次数
        /// </summary>
        public int FreeFeeTimes { get; set; }

        /// <summary>
        /// 提款手续费
        /// </summary>
        public decimal WithdrawalFee { get; set; }

        /// <summary>
        /// 是否系统数据
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 总存款次数
        /// </summary>
        public int DepositCount { get; set; }

        /// <summary>
        /// 总存款金额
        /// </summary>
        public int DepositAmount { get; set; }

        /// <summary>
        /// 总提款次数
        /// </summary>
        public int WithdrawalCount { get; set; }

        /// <summary>
        /// 总提款金额
        /// </summary>
        public int WithdrawalAmount { get; set; }

        /// <summary>
        /// 加入会员组开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 加入会员组结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 第三方支付单笔最大限额
        /// </summary>
        public int ThirdPayDepositSingleMax { get; set; }

        /// <summary>
        /// 第三方支付单笔最小限额
        /// </summary>
        public int ThirdPayDepositSingleMin { get; set; }
    }
}
