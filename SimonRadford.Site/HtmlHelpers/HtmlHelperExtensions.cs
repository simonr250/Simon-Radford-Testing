using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using MvcContrib.Pagination;

namespace SimonRadford.Site.HtmlHelpers
{
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public static class HtmlHelperExtensions
    {
        // Methods
        public static string ActionImage(this HtmlHelper html, string controller, string action, object routeValues, string imageRelativeUrl, string alt, object linkAttributes, object imageAttributes)
        {
            var u = new UrlHelper(html.ViewContext.RequestContext);
            var img = new TagBuilder("img");
            img.GenerateId(alt + "Img");
            img.MergeAttribute("src", imageRelativeUrl);
            img.MergeAttribute("alt", alt);
            img.MergeAttribute("title", alt);
            img.MergeAttribute("class", "actionLink");
            img.MergeAttributes(new RouteValueDictionary(imageAttributes));
            var t = new TagBuilder("a");
            t.MergeAttribute("href", u.Action(action, controller, new RouteValueDictionary(routeValues)));
            t.InnerHtml = img.ToString(TagRenderMode.SelfClosing);
            t.MergeAttributes(new RouteValueDictionary(linkAttributes));
            return t.ToString(TagRenderMode.Normal);
        }

        public static string ImageActionLink(this AjaxHelper helper, string imageUrl, string altText, string actionName, object routeValues, AjaxOptions ajaxOptions)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("title", altText);
            builder.MergeAttribute("class", "actionLink");
            var link = helper.ActionLink("[replaceme]", actionName, routeValues, ajaxOptions);
            return link.ToString().Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing));
        }

        public static string PageDropDown(this HtmlHelper html, IPagination pagedata)
        {
            HttpRequestBase _request = html.ViewContext.HttpContext.Request;
            StringBuilder builder = new StringBuilder();
            builder.Append("<div class='pagedropdown'>");
            builder.Append("Page <select>");
            for (int i = 1; i <= pagedata.TotalPages; i++)
            {
                builder.AppendFormat("<option value=\"{0}\"{1}>{0}</option>", i, (i == pagedata.PageNumber) ? " selected=\"true\"" : "");
            }
            builder.AppendFormat("</select> of {0}", pagedata.TotalPages);
            builder.Append("</div>");
            return builder.ToString();

        }

        public static string SubmitImage(this HtmlHelper htmlHelper, string imageLink, string value, string altText)
        {
            return string.Format("<input type='image' src='{0}' value='{1}' alt='{2}' />", imageLink, value, altText);
        }
    }

 

}
