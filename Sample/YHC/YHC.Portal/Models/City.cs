using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHC.Portal.Models
{
    public class City
    {
        /// <summary>
        /// 城市Id
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 省份id
        /// </summary>
        public Int32 ProvinceId { get; set; }
    }
}