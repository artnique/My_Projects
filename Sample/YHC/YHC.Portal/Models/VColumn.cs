namespace YHC.Portal.Models
{
    /// <summary>
    /// 列 视图模型
    /// </summary>
    public class VColumn
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
        /// 是否有权限
        /// </summary>
        public bool HasPermission { get; set; }

        /// <summary>
        /// 是否过滤
        /// </summary>
        public bool IsFilter { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }
    }
}