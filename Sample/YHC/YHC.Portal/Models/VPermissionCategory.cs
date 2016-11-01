namespace YHC.Portal.Models
{
    /// <summary>
    /// 权限分类 视图模型
    /// </summary>
    public class VPermissionCategory
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 上级分类
        /// </summary>
        public VPermissionCategory Parent { get; set; }

        /// <summary>
        /// 上级分类
        /// </summary>
        public string _parentId
        {
            get
            {
                if (Parent != null)
                {
                    return Parent.Id;
                }
                return null;
            } 
        }

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
        /// 排序号
        /// </summary>
        public int SortNo { get; set; }
    }
}