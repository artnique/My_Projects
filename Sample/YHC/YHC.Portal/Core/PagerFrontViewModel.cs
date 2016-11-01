using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Core
{
    /// <summary>
    /// 传给前台的分页对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagerFrontViewModel<T>
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        public Int32 count { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public Object extend { get; set; }

        /// <summary>
        /// 数据列表
        /// </summary>
        public IList<T> list { get; set; }
    }
}