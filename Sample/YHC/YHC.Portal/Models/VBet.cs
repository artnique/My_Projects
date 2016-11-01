using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class VBet
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public String UserName { get; set; }

        /// <summary>
        /// 投注次数
        /// </summary>
        public Int32 Num
        {
            get;
            set;
        }

        /// <summary>
        /// 总投注额
        /// </summary>
        public Decimal Bet
        {
            get;
            set;
        }

        /// <summary>
        /// 有效投注额
        /// </summary>
        public Decimal RealBet
        {
            get;
            set;
        }

        /// <summary>
        /// 派彩
        /// </summary>
        public Decimal PayOut
        {
            get;
            set;
        }

        /// <summary>
        /// 彩金
        /// </summary>
        public Decimal Jackpot { get; set; }

        /// <summary>
        /// 局号
        /// </summary>
        public String Stage { get; set; }

        /// <summary>
        /// 投注类型 1：真人投注额 2：彩票投注额 3：体育投注额 4：电子投注额
        /// </summary>
        public int Type
        {
            get;
            set;
        }

        /// <summary>
        /// 1输   2赢    3和   4无效
        /// </summary>
        public int ResultType { get; set; }

        /// <summary>
        /// 投注时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 派彩时间
        /// </summary>
        public DateTime UpdateTime
        {
            get;
            set;
        }

        public string GameName { get; set; }

        /// <summary>
        /// 游戏平台
        /// </summary>
        public string GamePlatform { get; set; }
    }
}