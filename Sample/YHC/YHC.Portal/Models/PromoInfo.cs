using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class PromoInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int32 Id { get; set; }

        ///// <summary>
        ///// 优惠码
        ///// </summary>
        public String PreferentialKey { get; set; }

        /// <summary>
        /// 优惠类型
        /// </summary>
        public int? InformationTypeId { get; set; }
        /// <summary>
        /// 优惠类型
        /// </summary>
        public string InformationTypeName { get; set; }

        /// <summary>
        /// 是否可以申请
        /// </summary>
        public bool CanApply
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// 概要描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// 图片，展示是前台的优惠图片
        /// </summary>
        public String Img { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string Thumbnail { get; set; }

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
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}