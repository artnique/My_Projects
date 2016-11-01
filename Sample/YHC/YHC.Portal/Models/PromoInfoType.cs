using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class PromoInfoType
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String TypeName { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public Int32 Sort { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusText
        {
            get;
            set;
        }
    }
}