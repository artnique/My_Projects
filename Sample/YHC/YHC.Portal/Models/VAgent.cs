using YHC.Portal.Core;
using YHC.Portal.Helper;
using System;
using System.Collections.Generic;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 代理
    /// </summary>
    [Serializable]
    public class VAgent
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 提款密码
        /// </summary>
        public string WithdrawalPassword { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 类型（系统，用户）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 类型文本
        /// </summary>
        public String TypeText
        {
            get
            {
                return this.Type == 0 ? "系统" : "用户";
            }
        }

        /// <summary>
        /// 状态（禁用，启用）
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 状态文本
        /// </summary>
        public String StatusText
        {
            get
            {
                return this.Status ? "启用" : "禁用";
            }
        }

        /// <summary>
        /// 代理号
        /// </summary>
        public string AgentCode { get; set; }

        /// <summary>
        /// 上级代理
        /// </summary>
        public VAgent Parent { get; set; }

        /// <summary>
        /// 总代理
        /// </summary>
        public int RootId { get; set; }

        /// <summary>
        /// 代理域名
        /// </summary>
        public string Domain { get; set; }

        ///// <summary>
        ///// 第三方账号
        ///// </summary>
        //public VThirdPay ThirdPay { get; set; }

        ///// <summary>
        ///// 存款账号
        ///// </summary>
        //public VAdminBank AdminBank { get; set; }

        /// <summary>
        /// 最大存款限额
        /// </summary>
        public int MaxDeposit { get; set; }

        /// <summary>
        /// 最小存款限额
        /// </summary>
        public int MinDeposit { get; set; }

        /// <summary>
        /// 最大提款限额
        /// </summary>
        public int MaxWithdraw { get; set; }

        /// <summary>
        /// 最小提款限额
        /// </summary>
        public int MinWithdraw { get; set; }

        /// <summary>
        /// 单笔提款最大限额
        /// </summary>
        public int SingleMaxWithdraw { get; set; }

        /// <summary>
        /// 单笔提款最小限额
        /// </summary>
        public int SingleMinWithdraw { get; set; }

        /// <summary>
        /// 会员数
        /// </summary>
        public int MemberCount { get; set; }

        /// <summary>
        /// 代理数
        /// </summary>
        public int AgentCount { get; set; }

        /// <summary>
        /// 代理组
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 层次关系
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 代理层级
        /// </summary>
        public VAgentPhysicalLevel AgentPhysicalLevel { get; set; }

        /// <summary>
        /// 叶子结点(没有下层代理)
        /// </summary>
        public bool IsLeaf { get; set; }

        ///// <summary>
        ///// 代理层级名称
        ///// </summary>
        //public string AgentLevelName { get; set; }

        /// <summary>
        /// 亏欠派彩
        /// </summary>
        public decimal DebtPayOut { get; set; }

        /// <summary>
        /// 代理账户
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 审批状态
        /// </summary>
        public AppStatus AppStatus { get; set; }

        /// <summary>
        /// 审批状态文本
        /// </summary>
        public string AppStatusText
        {
            get
            {
                return UtilHelper.GetAgentAuditStatusText(this.AppStatus);
            }
        }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string OperatDes { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        public DateTime? OperatTime { get; set; }

        /// <summary>
        /// 推广码
        /// </summary>
        public string ExtendCode { get; set; }

        /// <summary>
        /// 推广说明
        /// </summary>
        public string ExtendDes { get; set; }


        /// <summary>
        /// 注册ip
        /// </summary>
        public String RegIP { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录ip
        /// </summary>
        public String LastLoginIP { get; set; }

        /// <summary>
        /// 前台显示的佣金方案
        /// </summary>
        public String CommissionSchemeText { get; set; }
        ///// <summary>
        ///// 派彩是否累加
        ///// </summary>
        //public bool IsPayoutCumulative { get; set; }

        ///// <summary>
        ///// 是否允许下一级设置佣金分成比例
        ///// </summary>
        //public bool IsAllowNexSetRatio { get; set; }

        ///// <summary>
        ///// 存款会员数是否有效
        ///// </summary>
        //public bool EnableActiveUserNumber { get; set; }

        ///// <summary>
        /////有效投注金额否有效
        ///// </summary>
        //public bool EnableEffectiveBet { get; set; }

        ///// <summary>
        ///// 派彩金额否有效
        ///// </summary>
        //public bool EnablePayOut { get; set; }

        /// <summary>
        /// 代理分层比例配制
        /// </summary>
        //public IList<VAgentCommissionRatioSetting> commissionList { get; set; }

        ///// <summary>
        ///// 存款会员数
        ///// </summary>
        //public int ActiveUserNumber { get; set; }

        ///// <summary>
        ///// 有效投注金额
        ///// </summary>
        //public decimal EffectiveBet { get; set; }

        ///// <summary>
        ///// 派彩金额
        ///// </summary>
        //public decimal PayOut { get; set; }

        ///// <summary>
        ///// 有效投注佣金比例
        ///// </summary>
        //public decimal EffectiveBetRatio { get; set; }

        ///// <summary>
        ///// 派彩佣金比例
        ///// </summary>
        //public decimal PayOutRatio { get; set; }

        /// <summary>
        /// 代理佣金分成比例设置列表
        /// </summary>
        //public IList<VAgentCommissionRatioSetting> AgentCommissionRatioSettings { get; set; }

        ///// <summary>
        ///// 代理佣金方案
        ///// </summary>
        //public AgentComissionScheme ComissionScheme { get; set; }

        /// <summary>
        /// 代理佣金区间段设置
        /// </summary>
        //public VAgentCommissionRangeSetting AgentCommissionRangeSetting { get; set; }

        /// <summary>
        /// 全局分成比例
        /// </summary>
        //public IList<VAgentCommissionRangeRatioGlobalSetting> CommissionRangeRatioGlobalSetting { get; set; }
        /// <summary>
        /// 上级代理ID
        /// </summary>

        public int? _parentId
        {
            get
            {
                if (Parent != null)
                {
                    return this.Parent.Id;
                }
                return null;
            }
        }

    }
}