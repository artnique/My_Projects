using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace YHC.Portal
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/Scriptslib").Include(
                "~/Scripts/lib/jquery-1.8.2.min.js",
                "~/Scripts/lib/main.js",
                "~/Scripts/lib/utility.js",
                "~/Scripts/lib/jquery.cookie.js",
                "~/Scripts/lib/dt.js",
                "~/scripts/lib/sticky/sticky.js"));

            bundles.Add(new ScriptBundle("~/bundles/Scriptspage").Include(
                "~/Scripts/lib/jquery.SuperSlide.2.1.1.js",
                "~/Scripts/page/core.js",
                "~/Scripts/page/portal.js",
                "~/Scripts/page/a.js"));

            bundles.Add(new ScriptBundle("~/bundles/Scriptsslot").Include(
                "~/Scripts/page/game/jPages.js",
                "~/Scripts/page/game/ptslot.js"));

            bundles.Add(new ScriptBundle("~/bundles/Scriptspagination").Include(
               "~/scripts/lib/pagination/jquery.pagination.js"));

            bundles.Add(new ScriptBundle("~/bundles/Scriptssport").Include(
                "~/Scripts/lib/slideshow/jquery.SuperSlide.2.1.1.js",
                "~/Scripts/lib/pluging/jquery.anythingslider1.9.2.js",
                "~/Scripts/page/game/sport.js"));

            bundles.Add(new ScriptBundle("~/bundles/Scriptspromo").Include(
                "~/Scripts/page/promo/index.js"));

            bundles.Add(new StyleBundle("~/bundles/Stylesslot").Include(
                "~/Content/style/slot.css"));

            bundles.Add(new ScriptBundle("~/bundles/Scriptsforgetpwd").Include(
                "~/Scripts/page/home/forgetpwd.js"));

            bundles.Add(new ScriptBundle("~/bundles/Scriptsalert").Include(
               "~/Scripts/lib/alert/jquery.alerts.js"));

            // 样式绑定
            bundles.Add(new StyleBundle("~/bundles/Stylescss").Include(
                "~/Content/style/main.css"));

            bundles.Add(new StyleBundle("~/bundles/Stylesalert").Include(
                "~/Scripts/lib/alert/jquery.alerts.css"));
        }
    }
}