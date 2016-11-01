using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHC.Portal.Models
{
    public class GameApi
    {
        /// <summary>
        /// 游戏名称
        /// </summary>
        public String GameName
        {
            get;
            set;
        }

        /// <summary>
        /// 游戏平台
        /// </summary>
        public String GamePlatform
        {
            get;
            set;
        }

        /// <summary>
        /// 接口地址2，一般为报表接口
        /// </summary>
        public String ApiUrl2
        {
            get;
            set;
        }

        /// <summary>
        /// 登录地址(电子游戏)，视不同游戏，这个可以为空
        /// </summary>
        public String LoginUrl2
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public Boolean Status
        {
            get;
            set;
        }

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
        /// 是否维护，true维护
        /// </summary>
        public Boolean Maintained
        {
            get;
            set;
        }

        /// <summary>
        /// 维护状态文本
        /// </summary>
        public String MaintainedText
        {
            get;
            set;
        }

        /// <summary>
        /// 维护信息
        /// </summary>
        public String MaintainMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 维护开始时间
        /// </summary>
        public DateTime? MaintainStartTime { get; set; }

        /// <summary>
        /// 维护结束时间
        /// </summary>
        public DateTime? MaintainEndTime { get; set; }
    }
}
