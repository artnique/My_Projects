using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 用户游戏账号
    /// </summary>
    public class UserGameAccount
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 类型类型
        /// </summary>
        public string GameType { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 系统中当前的余额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 最后更新平台投注数据的时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 平台上的真实余额
        /// </summary>
        public decimal RealAmount { get; set; }

        /// <summary>
        /// 查询余额的时间
        /// </summary>
        public DateTime? QueryTime { get; set; }

        /// <summary>
        /// 查询完余额后记录当时的更新投注时间
        /// </summary>
        public DateTime OriUpdateTime { get; set; }

        /// <summary>
        /// 取余额是否成功
        /// </summary>
        public bool? QueryResult { get; set; }

        /// <summary>
        /// 校验数据的结果
        /// </summary>
        public bool CheckResult { get; set; }

        /// <summary>
        /// 检验数据的时间
        /// </summary>
        public DateTime CheckTime { get; set; }

        /// <summary>
        /// 等待补单
        /// </summary>
        public bool IsWaitting { get; set; }

        /// <summary>
        /// 未结算的金额
        /// </summary>
        public decimal NoSettAmount { get; set; }
    }
}
