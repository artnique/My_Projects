using YHC.Portal.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class UserTransfer
    {
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 游戏类型（平台）
        /// </summary>
        public string GameType { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 状态文本
        /// </summary>
        public String StatusText
        {
            get
            {
                String text = "未定义";
                if (this.Status == (int)UserTransferStatus.Success)
                {
                    text = "成功";
                }
                else if (this.Status == (int)UserTransferStatus.IsTransfer)
                {
                    text = "处理中";
                }
                else if (this.Status == (int)UserTransferStatus.NeedProcess || this.Status == (int)UserTransferStatus.NeedProcess_Restore || this.Status == (int)UserTransferStatus.NeedProcess_Success)
                {
                    text = "待处理";
                }
                else
                {
                    text = "失败";
                }
                return text;
            }
        }

        /// <summary>
        /// 额度操作类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 类型文本
        /// </summary>
        public String TypeText
        {
            get
            {
                String text = "";
                if (this.Type == (int)UserTransferType.In)
                {
                    text = "转入";
                }
                else if (this.Type == (int)UserTransferType.Out)
                {
                    text = "转出";
                }
                return text;
            }
        }
    }
}