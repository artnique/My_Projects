using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YHC.Portal.Extensions;

namespace YHC.Portal.Models
{
    public class User
    {
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string TrueName { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public int UserLevel { get; set; }

        /// <summary>
        /// 现金
        /// </summary>
        public decimal Cash { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Email验证状态
        /// </summary>
        public bool EmailValidateStatus { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 电话验证状态
        /// </summary>
        public bool PhoneValidateStatus { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 银行存款单笔最大限额
        /// </summary>
        public Int32 MaxDeposit { get; set; }

        /// <summary>
        /// 银行存款单笔最小限额
        /// </summary>
        public Int32 MinDeposit { get; set; }

        /// <summary>
        /// 最大出款
        /// </summary>
        public Int32 MaxWithdraw { get; set; }

        /// <summary>
        /// 最小出款
        /// </summary>
        public Int32 MinWithdraw { get; set; }

        /// <summary>
        /// 单笔最大出款
        /// </summary>
        public Int32 SingleMaxWithdraw { get; set; }

        /// <summary>
        /// 单笔最小出款
        /// </summary>
        public Int32 SingleMinWithdraw { get; set; }

        //
        // 摘要:
        //     第三方支付(存款)单笔最大限额
        public int ThirdPayDepositSingleMax { get; set; }
        //
        // 摘要:
        //     第三方支付(存款)单笔最小限额
        public int ThirdPayDepositSingleMin { get; set; }

        /// <summary>
        /// 是否存在提款密码
        /// </summary>
        public bool HasWithdrawalPassword { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        #region 加*号显示的字段，只get

        /// <summary>
        /// 加*号的会员名
        /// </summary>
        public String MaskUserName
        {
            get
            {
                return this.UserName.MaskStart(1);
            }
        }

        /// <summary>
        /// 加*号的邮件
        /// </summary>
        public String MaskEmail
        {
            get
            {
                return this.Email.MaskStart(2);
            }
        }

        /// <summary>
        /// 加*号的电话
        /// </summary>
        public String MaskPhone
        {
            get
            {
                return this.Phone.MobileMask();
            }
        }

        /// <summary>
        /// 生日
        /// </summary>
        public string BirthdayText
        {
            get
            {
                string birthdayText = string.Empty;
                if (Birthday.HasValue)
                {
                    birthdayText = Birthday.Value.ToString("yyyy-MM-dd");
                }
                return birthdayText;
            }
        }

        #endregion

        /// <summary>
        /// 支付宝最大限额
        /// </summary>
        public Decimal AliPayMaxAmount { get; set; }

        /// <summary>
        /// 支付宝最大限额
        /// </summary>
        public Decimal AliPayMinAmount { get; set; }

        /// <summary>
        /// 微信最大限额
        /// </summary>
        public Decimal WXMaxAmount { get; set; }

        /// <summary>
        /// 微信最大限额
        /// </summary>
        public Decimal WXMinAmount { get; set; }
    }
}