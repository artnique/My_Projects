using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using YHC.Portal.Core;

namespace YHC.Portal.Models
{
    public class Game
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public GameCategory GameLocalCategory { get; set; }

        /// <summary>
        /// 最新游戏
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// 最新游戏文本
        /// </summary>
        public string IsNewText
        {
            get
            {
                if (IsNew)
                {
                    return "是";
                }
                return "否";
            }
        }

        /// <summary>
        /// 热门游戏
        /// </summary>
        public bool IsHot { get; set; }


        /// <summary>
        /// 热门游戏文本
        /// </summary>
        public string IsHotText
        {
            get
            {
                if (IsHot)
                {
                    return "是";
                }
                return "否";
            }
        }

        /// <summary>
        /// 游戏平台
        /// </summary>
        public GameApi Api { get; set; }

        /// <summary>
        /// 游戏类型
        /// </summary>
        public BetType GameType { get; set; }

        /// <summary>
        /// 赔付线数
        /// </summary>
        public LineNumber? PaymentLineNumber { get; set; }


        public string GameTypeText_EN
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 英文标题
        /// </summary>
        public string EnTitle { get; set; }

        /// <summary>
        /// 游戏标识
        /// </summary>
        public string GameIdentify { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 推荐指数
        /// </summary>
        public int RecommendNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 状态文本
        /// </summary>
        public string StatusText
        {
            get
            {
                if (Status)
                {
                    return "启用";
                }
                return "禁用";
            }
        }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }

        /// <summary>
        /// PT试玩
        /// </summary>
        public bool IsTry { get; set; }

        public string IsTryText
        {
            get
            {
                if (IsTry)
                {
                    return "试玩";
                }
                return "正式";
            }
        }

        /// <summary>
        /// 是否显示奖金池
        /// </summary>
        public bool ShowJackpots { get; set; }

        /// <summary>
        /// 是否显示奖金池
        /// </summary>
        public string ShowJackpotsText
        {
            get
            {
                if (ShowJackpots)
                {
                    return "显示";
                }
                return "不显示";
            }
        }

        /// <summary>
        /// 奖金类型 1.
        /// </summary>
        public JackpotsInfoType JackpotsInfo { get; set; }


        /// <summary>
        /// 奖金池参数
        /// </summary>
        public string JackpotsParams { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 游戏平台对应的ID号，用作游戏中文名称映射
        /// </summary>
        public string GameNameId { get; set; }
    }
}