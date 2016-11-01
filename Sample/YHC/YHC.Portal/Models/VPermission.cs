using System.Collections.Generic;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 权限 视图模型
    /// </summary>
    public class VPermission
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 权限分类
        /// </summary>
        public VPermissionCategory PermissionCategory { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// （可设置是否显示的）列集合
        /// </summary>
        public IList<VColumn> Columns { get; set; }

        /// <summary>
        /// 是否有权限(设置角色权限时使用)
        /// </summary>
        public bool HasPermission { get; set; }

        /// <summary>
        /// 是否限制查询(设置角色权限时使用)
        /// </summary>
        public bool IsLimitQuery { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }

        /// <summary>
        /// 可设置是否限制查询
        /// </summary>
        public bool SetIsLimitQuery { get; set; }
    }
}