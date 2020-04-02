using System.Web;
using System.Web.Mvc;

namespace SchoolSystem.HtmlHelper
{
    public static class SCHelper
    {
        public static IHtmlString File(this System.Web.Mvc.HtmlHelper helper, string id)
        {
            TagBuilder tb = new TagBuilder("input");
             tb.Attributes.Add("type","file");
             tb.Attributes.Add("id",id);
            return new MvcHtmlString(tb.ToString());
        }

        public static IHtmlString SSLabel(this System.Web.Mvc.HtmlHelper helper, string content)
        {
            string LabelStr =
                $"<Label Style=\"background-color: blue;color:red; font-size:24px\"{content}</Label>";

            return new HtmlString(LabelStr);
        }

        public static IHtmlString File(string id)
        {
            TagBuilder tb = new TagBuilder("input");
            tb.Attributes.Add("type", "file");
            tb.Attributes.Add("id", id);
            return new MvcHtmlString(tb.ToString());
        }

        public static IHtmlString Image(this System.Web.Mvc.HtmlHelper helper, string src, string alt)
        {
            TagBuilder tb = new TagBuilder("img");
            tb.Attributes.Add("src",VirtualPathUtility.ToAbsolute(src));
            tb.Attributes.Add("alt",alt);

            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }
        /// <summary>
        /// This is to make a capitalize the first letter of a string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string SCCapitalizeFirstLetter(string str)
        {
            if (str != null)
            {
                return (char.ToUpper(str[0]) + str.Substring(1).ToLower()).ToString();
            }
            // Mule - how can i use a customer exception class str is null 
            return null;
        }
    }
}