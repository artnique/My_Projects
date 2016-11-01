using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    [Serializable]
    public class Config
    {
        public string AgentSiteDomain { get; set; }
        /// <summary>
        /// 站点样式
        /// </summary>
        public string SiteThemes { get; set; }
        /// <summary>
        /// 站点状态（开启，关闭）
        /// </summary>
        public bool SiteStatus { get; set; }
        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int SiteRows { get; set; }
        /// <summary>
        /// 页面列表高度
        /// </summary>
        public int SiteBodyHeight { get; set; }
        /// <summary>
        /// 默认查询时间
        /// </summary>
        public int SiteQueryTime { get; set; }
        /// <summary>
        /// 客户查询时间
        /// </summary>
        public int CustomerQueryTime { get; set; }

        /// <summary>
        /// 启用邮箱绑定
        /// </summary>
        public bool IsBindingEmail { get; set; }

        /// <summary>
        /// 启用手机绑定
        /// </summary>
        public bool IsBindingPhone { get; set; }

        /// <summary>
        /// 时区
        /// </summary>
        public int TimeZone { get; set; }
        /// <summary>
        /// 是否允许绑定不同名银行卡
        /// </summary>
        public bool UserDiffBank { get; set; }
        /// <summary>
        /// 时区显示名称
        /// </summary>
        public string TimeZoneDisplayName { get; set; }
    }
}