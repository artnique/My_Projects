namespace YHC.Portal.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using System.Web.Routing;

    public static class HtmlHelperExtensions
    {
        #region HeadLink

        public static string HeadLink(this HtmlHelper htmlHelper, string rel, string href, string type, string title)
        {
            return htmlHelper.HeadLink(rel, href, type, title, null);
        }

        public static string HeadLink(this HtmlHelper htmlHelper, string rel, string href, string type, string title, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("link");

            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            if (!string.IsNullOrEmpty(rel))
            {
                tagBuilder.MergeAttribute("rel", rel);
            }
            if (!string.IsNullOrEmpty(href))
            {
                // 增加版本号
                href += "?v=" + ConfigHelper.StaticResourceVersion;
                tagBuilder.MergeAttribute("href", href);
            }
            if (!string.IsNullOrEmpty(type))
            {
                tagBuilder.MergeAttribute("type", type);
            }
            if (!string.IsNullOrEmpty(title))
            {
                tagBuilder.MergeAttribute("title", title);
            }

            return tagBuilder.ToString(TagRenderMode.SelfClosing);
        }

        #endregion

        #region RenderCssFile

        public static void RenderCssFile(this HtmlHelper htmlHelper, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("css file path not null");
            }

            htmlHelper.ViewContext.HttpContext.Response.Write(htmlHelper.HeadLink("stylesheet", path, "text/css", ""));
        }

        public static void RenderCssFile(this HtmlHelper htmlHelper, string path, object htmlAttribute)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("css file path not null");
            }

            htmlHelper.ViewContext.HttpContext.Response.Write(htmlHelper.HeadLink("stylesheet", path, "text/css", "", htmlAttribute));
        }

        #endregion

        #region RenderScriptTag

        public static void RenderScriptFile(this HtmlHelper htmlHelper, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("script file path not null");
            }

            htmlHelper.ViewContext.HttpContext.Response.Write(htmlHelper.ScriptBlock("text/javascript", path));
        }

        public static void RenderScriptFile(this HtmlHelper htmlHelper, string path, object htmlAttribute)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("script file path not null");
            }
            if (htmlAttribute == null)
            {
                throw new ArgumentNullException("htmlAttribute not null");
            }


            htmlHelper.ViewContext.HttpContext.Response.Write(htmlHelper.ScriptBlock("text/javascript", path, htmlAttribute));
        }

        #endregion



        #region ScriptBlock

        public static string ScriptBlock(this HtmlHelper htmlHelper, string type, string src)
        {
            return htmlHelper.ScriptBlock(type, src, null);
        }

        public static string ScriptBlock(this HtmlHelper htmlHelper, string type, string src, object htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("script");

            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            if (!string.IsNullOrEmpty(type))
            {
                tagBuilder.MergeAttribute("type", type);
            }
            if (!string.IsNullOrEmpty(src))
            {
                src += "?v=" + ConfigHelper.StaticResourceVersion;
                tagBuilder.MergeAttribute("src", src);
            }

            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        #endregion


        #region Json扩展方法（允许Get请求获取json）
        /// <summary>
        /// 允许Get请求获取json
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static JsonResult XJson(this Controller controller, Object data)
        {
            JsonResult result = new JsonResult();
            result.Data = data;
            result.MaxJsonLength = Int32.MaxValue;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return result;
        }

        /// <summary>
        /// 返回自定义提示消息json格式
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static JsonResult JsonError(this Controller controller, String msg, Object data = null)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (data == null)
            {
                result.Data = new { success = false, msg = msg };
            }
            else
            {
                result.Data = new { success = false, msg = msg, responseText = data };
            }

            return result;
        }

        /// <summary>
        /// 格式：{ success = true, msg = "" }
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="msg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static JsonResult JsonOK(this Controller controller, String msg = "", Object data = null)
        {
            JsonResult result = new JsonResult();
            result.MaxJsonLength = int.MaxValue;
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (data == null)
            {
                result.Data = new { success = true, msg = msg };
            }
            else
            {
                result.Data = new { success = true, msg = msg, responseText = data };
            }

            return result;
        }

        /// <summary>
        ///  格式：{ success = true/false }
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="successed"></param>
        /// <returns></returns>
        public static JsonResult JsonResult(this Controller controller, Boolean successed, String msg = "")
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (string.IsNullOrWhiteSpace(msg))
            {
                result.Data = new { success = successed };
            }
            else
            {
                result.Data = new { success = successed, msg = msg };
            }

            return result;
        }

        #endregion
    }
}
