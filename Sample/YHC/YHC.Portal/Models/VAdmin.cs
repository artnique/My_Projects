using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 管理员
    /// </summary>
    [Serializable]
    public class VAdmin
    {
        /// <summary>
        /// 主键
        /// </summary>
        public String Id { get; set; }

        /// <summary>
        /// 管理员登录名
        /// </summary>
        public String AdminName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// 密码修改时间
        /// </summary>
        public DateTime PasswordUpdateTime { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual VAdminRole Role { get; set; }

        /// <summary>
        /// 状态,true为启用，false为禁用
        /// </summary>
        public Boolean Status { get; set; }

        public String StatusText
        {
            get
            {
                return this.Status ? "启用" : "禁用";
            }
        }

        /// <summary>
        /// 登录次数
        /// </summary>
        public Int32 LoginCount { get; set; }

        /// <summary>
        /// 界面主题
        /// </summary>
        public String Theme { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool IsSuperAdmin { get; set; }

        public String IsSuperAdminText
        {
            get
            {
                return this.IsSuperAdmin ? "是" : "否";
            }
        }

        /// <summary>
        /// 登录状态
        /// </summary>
        public bool LoginStatus { get; set; }

        public String LoginStatusText
        {
            get
            {
                return this.LoginStatus ? "是" : "否";
            }
        }

        /// <summary>
        /// SessionId
        /// </summary>
        public string SessionId { get; set; }
    }
}