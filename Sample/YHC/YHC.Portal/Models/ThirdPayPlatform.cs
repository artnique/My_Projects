using System;
using System.Collections.Generic;

namespace YHC.Portal.Models
{
    /// <summary>
    /// 第三方支付平台
    /// </summary>
    [Serializable]
    public class ThirdPayPlatform
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 第三方支付的代码，写死的
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 银行支付url（部分接口选银行支付时,url地址不一样）
        /// </summary>
        public string UrlOfBank { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string LogoImg { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 第三方支付平台银行集合
        /// </summary>
        public virtual IList<VThirdPayPlatformBank> ThirdPayPlatformBanks { get; set; }
    }


    public enum ThirdPayPlatFormEnum
    {
        /// <summary>
        /// 环迅支付
        /// </summary>
        IPS,

        /// <summary>
        /// 支付卫士-国付宝
        /// </summary>
        GFB,

        /// <summary>
        /// 闪付
        /// </summary>
        LPAY,

        /// <summary>
        /// 支付卫士-双乾
        /// </summary>
        SQ,

        /// <summary>
        /// 支付卫士-环迅
        /// </summary>
        ZFWSIPS,

        /// <summary>
        /// 新生支付
        /// </summary>
        HNAPAY,

        /// <summary>
        /// 智付
        /// </summary>
        DINPAY,

        /// <summary>
        /// 来销卡
        /// </summary>
        LXC,

        /// <summary>
        /// 摩宝支付
        /// </summary>
        MOBAO,

        /// <summary>
        /// 新国付宝
        /// </summary>
        NEWGFB,

        /// <summary>
        /// DADDYPAY
        /// </summary>
        DADDYPAY,

        /// <summary>
        /// 商银信
        /// </summary>
        ALLSCORE,

        /// <summary>
        /// 新贝微信支付
        /// </summary>
        XBEIWXPAY,

        /// <summary>
        /// 新贝支付
        /// </summary>
        XBEIPAY,

        /// <summary>
        /// 新贝点卡,充值卡
        /// </summary>
        XBEICARDPAY,

        /// <summary>
        /// 智付微信支付
        /// </summary>
        DINWXPAY,
    }
}