using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 资讯分类
    /// </summary>
    public class NewsCategory
    {
        /// <summary>
        /// 分类ID,自增
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 前台网站通过该关键字获取分类，可以为空
        /// </summary>
        public String Key { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public Int32 Sort { get; set; }

        /// <summary>
        /// 父节点ID,根节点为0
        /// </summary>
        public Int32 ParentId { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public Boolean Showed { get; set; }

        public String ShowedText
        {
            get
            {
                return this.Showed ? "是" : "否";
            }
        }
    }
}