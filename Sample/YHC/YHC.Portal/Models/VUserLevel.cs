using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHC.Portal.Models
{
    public class VUserLevel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 级别名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 会员级别
        /// </summary>
        public int UserLevel { get; set; }
    }
}
