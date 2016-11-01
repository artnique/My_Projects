using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class Ad
    {
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 广告标志
        /// </summary>
        public string AdNo
        {
            get;
            set;
        }

        /// <summary>
        ///广告描述
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 连接地址
        /// </summary>
        public string Link
        {
            get;
            set;
        }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName
        {
            get;
            set;
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath
        {
            get;
            set;
        }

        public string ImgUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 图片高度
        /// </summary>
        public int? ImgHeight
        {
            get;
            set;
        }

        /// <summary>
        /// 图片宽度
        /// </summary>
        public int? ImgWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }

        /// <summary>
        /// 链接打开方式
        /// </summary>
        public String LinkTarget
        {
            get;
            set;
        }

        /// <summary>
        /// 链接打开方式文本
        /// </summary>
        public String LinkTargetText
        {
            get;
            set;
        }
    }
}