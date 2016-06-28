using System.Web.Mvc;
using MsAspMvc = System.Web.Mvc.Html;
using System.Web.Routing;

namespace CSTruter.Web.Mvc.Html
{
    public static class FormExtensions
    {
        public static MsAspMvc.MvcForm BeginAngularForm(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method, object htmlAttributes)
        {
            RouteValueDictionary routeValues = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (!routeValues.ContainsKey("name"))
                routeValues.Add("name", "form");

            htmlHelper.ViewData.Add("formName", routeValues["name"]);
            return MsAspMvc.FormExtensions.BeginForm(htmlHelper, actionName, controllerName, method, routeValues);
        }
    }
}