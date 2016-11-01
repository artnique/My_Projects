using System;
using System.Collections.Generic;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 管理员组
    /// </summary>
    [Serializable]
    public class VAdminRole
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public IList<VPermission> Permissions { get; set; }
    }
}