using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 新闻或公告
    /// </summary>
    public class News
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public Int32 CategoryId { get; set; }

        public String Key { get; set; }

        public Int32 Sort { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 内容（移除HTML后的内容）
        /// </summary>
        public String Content_RemoveHtml
        {
            get;
            set;
        }

        /// <summary>
        /// 外部链接
        /// </summary>
        public string ExternalLink { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户（对应管理员的ID号）
        /// </summary>
        public String CreateUser { get; set; }

        /// <summary>
        /// 修噶时间
        /// </summary>
        public DateTime OperatTime { get; set; }

        /// <summary>
        /// 修噶创建用户（对应管理员的ID号）
        /// </summary>
        public String Operator { get; set; }

        /// <summary>
        /// 是否广播
        /// </summary>
        public Boolean Broadcasted { get; set; }

        public String BroadcastedText
        {
            get;
            set;
        }
    }
}