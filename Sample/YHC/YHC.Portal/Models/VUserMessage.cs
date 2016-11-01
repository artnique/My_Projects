using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class VUserMessage
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// 站内信
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime? ReadTime { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 是否删除文本
        /// </summary>
        public string IsDelText
        {
            get { return IsDel ? "是" : "否"; }
        }
    }
}