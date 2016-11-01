using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 新闻或公告
    /// </summary>
    public class Footer
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

    }
}