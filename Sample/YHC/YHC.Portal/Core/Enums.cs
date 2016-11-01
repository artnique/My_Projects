using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Core
{
    /// <summary>
    /// 登录状态
    /// </summary>
    public enum EnumLoginStatus
    {
        /// <summary>
        /// 已登录
        /// </summary>
        Logined,

        /// <summary>
        /// 未登录或者缓存过期
        /// </summary>
        NotLogin,

        /// <summary>
        /// 会话已过期，Cookie过期
        /// </summary>
        Expired,
    }

    /// <summary>
    /// 备注类型
    /// </summary>
    public enum RemarkType
    {
        /// <summary>
        /// 普通类
        /// </summary>
        General = 10,
        /// <summary>
        /// 财务类
        /// </summary>
        Finance = 20,
        /// <summary>
        /// 危险类
        /// </summary>
        Hazard = 30
    }

    /// <summary>
    /// 代理审核状态
    /// </summary>
    public enum AppStatus
    {
        /// <summary>
        /// 0未审核
        /// </summary>
        App = 0,

        /// <summary>
        /// 1审核通过
        /// </summary>
        AppPass = 1,

        /// <summary>
        /// 2审核不通过
        /// </summary>
        AppUnPass = 2
    }

    public enum BetType
    {
        /// <summary>
        /// 1真人投注
        /// </summary>
        Trueman = 1,

        /// <summary>
        /// 2彩票投注
        /// </summary>
        Lottery = 2,

        /// <summary>
        /// 3体育投注
        /// </summary>
        Sport = 3,

        /// <summary>
        /// 4电子投注
        /// </summary>
        Electron = 4
    }

    public enum JackpotsInfoType
    {
        // 摘要:
        //     1.Game Based – displays jackpot information for a specific game in a specific
        //     casino.
        GAMEBASED = 1,
        //
        // 摘要:
        //     2.Casino Based – displays jackpot information for a specific casino. The
        //     mandatory parameter is casino.
        CASINOBASED = 2,
        //
        // 摘要:
        //     3.Games base XML - ALL info about games
        GAMETOTAL = 3,
        //
        // 摘要:
        //     4.Casinos Total – displays the total jackpot of all casinos. No mandatory
        //     parameters.
        CASINOSTOTAL = 4,
        //
        // 摘要:
        //     5.Game Group Total – displays the total jackpot for a game group. The mandatory
        //     parameter is group.
        GAMEGROUPTOTAL = 5,
    }

    public enum InspectType
    {
        /// <summary>
        /// 存款
        /// </summary>
        DEPOSIT = 0,

        /// <summary>
        /// 资金修正
        /// </summary>
        REPAIR = 1,

        /// <summary>
        /// 优惠
        /// </summary>
        PREFERENTIAL = 2,

        /// <summary>
        /// 手工优惠
        /// </summary>
        BOUNS = 3
    }

    public enum UserWalletStatus
    {
        /// <summary>
        /// 0初始状态
        /// </summary>
        Init = 0,

        /// <summary>
        /// 1申请状态
        /// </summary>
        Apply = 1,

        /// <summary>
        /// 2已发送状态
        /// </summary>
        Send = 2,

        /// <summary>
        /// 3取消状态
        /// </summary>
        Cancel = 3,

        /// <summary>
        /// 4拒绝状态
        /// </summary>
        Refust = 4,


    }

    /// <summary>
    /// 赔付线数
    /// </summary>
    public enum LineNumber
    {
        /// <summary>
        /// 10线以下
        /// </summary>
        LessThan10 = 5,
        /// <summary>
        /// 10线
        /// </summary>
        Ten = 10,
        /// <summary>
        /// 15线
        /// </summary>
        Fiften = 15,
        /// <summary>
        /// 20线
        /// </summary>
        Twenty = 20,
        /// <summary>
        /// 25线以上
        /// </summary>
        GreaterTen25 = 25,
    }

    public enum TransferType
    {
        // 摘要:
        //     网银
        OnlineBank = 1,
        //
        // 摘要:
        //     ATM自动柜员机
        ATMTrans = 2,
        //
        // 摘要:
        //     ATM现金存款
        ATMDeposit = 3,
        //
        // 摘要:
        //     银行柜台
        BankCounter = 4,
        //
        // 摘要:
        //     手机银行
        MobileBank = 5,
        /// <summary>
        /// 支付宝
        /// </summary>
        Alipay = 6,
        /// <summary>
        /// 微信
        /// </summary>
        Wechat = 7,
    }

    // 摘要:
    //     存款类型
    public enum UserDepositType
    {
        // 摘要:
        //     0银行转账
        BankTransfer = 0,
        //
        // 摘要:
        //     1第三方支付
        OnlinePay = 1,
        //
        // 摘要:
        //     2代理转账
        AgentTransfer = 2,
        //
        // 摘要:
        //     亚联支付
        AsiaPay = 3,
    }

    // 摘要:
    //     提款状态
    public enum UserWithdrawalStatus
    {
        // 摘要:
        //     处理中
        Processing = 1,
        //
        // 摘要:
        //     初审通过
        AuditPass = 2,
        //
        // 摘要:
        //     初审拒绝
        AuditRefuse = 3,
        //
        // 摘要:
        //     复审通过
        AuditReviewPass = 4,
        //
        // 摘要:
        //     复审拒绝
        AuditReviewRefuse = 5,
    }

    // 摘要:
    //     用户转账类型
    public enum UserTransferType
    {
        // 摘要:
        //     0转出（从主账号转钱到游戏平台）
        Out = 0,
        //
        // 摘要:
        //     1转入（从游戏平台转钱到主账号）
        In = 1,
    }

    /// <summary>
    /// 游戏转账状态
    /// </summary>
    public enum UserTransferStatus
    {
        /// <summary>
        /// 0失败（游戏接口告知失败）
        /// </summary>
        Failed = 0,

        /// <summary>
        /// 1成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 2待处理
        /// </summary>
        NeedProcess = 2,

        /// <summary>
        /// 3待处理（直接恢复金额)
        /// </summary>
        NeedProcess_Restore = 3,

        /// <summary>
        /// 4待处理（操作成功）
        /// </summary>
        NeedProcess_Success = 4,

        /// <summary>
        ///正在转账
        /// </summary>
        IsTransfer = 5,
    }

    public enum UserBonusStatus
    {
        /// <summary>
        /// 1处理中
        /// </summary>
        Processing = 1,

        /// <summary>
        /// 2审核中
        /// </summary>
        //Audit = 2,

        /// <summary>
        /// 3拒绝
        /// </summary>
        Refuse = 3,

        /// <summary>
        /// 4成功
        /// </summary>
        Success = 4,

        /// <summary>
        /// 5取消
        /// </summary>
        Cancel = 5
    }

    public enum UserDepositStatus
    {
        /// <summary>
        /// 0未支付
        /// </summary>
        NotPay = 0,

        /// <summary>
        /// 1处理中
        /// </summary>
        Processing = 1,

        /// <summary>
        /// 2审核中
        /// </summary>
        Audit = 2,

        /// <summary>
        /// 3拒绝
        /// </summary>
        Refuse = 3,

        /// <summary>
        /// 4成功
        /// </summary>
        Success = 4,

        /// <summary>
        /// 5取消
        /// </summary>
        Cancel = 5
    }

    /// <summary>
    /// 推荐状态
    /// </summary>
    public enum RecommendStatus
    {
        /// <summary>
        /// 0初始
        /// </summary>
        INIT = 0,

        /// <summary>
        /// 1处理中
        /// </summary>
        PROCESSING = 1,

        /// <summary>
        /// 2审核通过
        /// </summary>
        AUDIT = 2,

        /// <summary>
        /// 3拒绝
        /// </summary>
        REFUSE = 3,

        /// <summary>
        /// 4完成
        /// </summary>
        COMPLETED = 4
    }

    public enum MessageStatus
    {
        Unread = 0,
        Read = 1,
    }

    public enum UserWalletType
    {
        /// <summary>
        /// 优惠
        /// </summary>
        Preferential = 0,

        /// <summary>
        /// 抽奖
        /// </summary>
        Lucky = 1,

        /// <summary>
        /// 完善资料
        /// </summary>
        Perfect = 2,

        /// <summary>
        /// 推荐
        /// </summary>
        Recommend = 3,

        /// <summary>
        /// 其他类型
        /// </summary>
        Other = 4,
    }
}