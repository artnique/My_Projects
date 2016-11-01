using YHC.Portal.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace YHC.Portal.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderCssFile(this UrlHelper url, String path)
        {
            StringBuilder str = new StringBuilder();
            if (!string.IsNullOrEmpty(path))
            {
                str.Append(string.Format("<link type=\"text/css\" rel=\"stylesheet\" href=\"{0}\"/>"
                                         , url.Content(path + "?v=" + ConfigHelper.StaticResourceVersion)));
            }
            return new MvcHtmlString(str.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderScriptFile(this UrlHelper url, String path)
        {
            StringBuilder str = new StringBuilder();
            if (!string.IsNullOrEmpty(path))
            {
                str.Append(string.Format("<script type=\"text/javascript\"  src=\"{0}\" ></script>"
                                         , url.Content(path + "?v=" + ConfigHelper.StaticResourceVersion)));
            }
            return new MvcHtmlString(str.ToString());
        }
    }
}
