using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 代理物理层级
    /// </summary>
    [Serializable]
    public class VAgentPhysicalLevel
    {
        /// <summary>
        /// 级别（1，2，3，4，5等等）
        /// </summary>
        public int Level{get;set;}

        /// <summary>
        /// 名称
        /// </summary>
        public string Name{get;set;}

        /// <summary>
        /// 存款会员数
        /// </summary>
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
    }
}