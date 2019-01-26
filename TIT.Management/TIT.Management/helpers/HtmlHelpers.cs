﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace TIT.Management.helpers
{
    public static class HtmlHelpers
    {
        public static MvcHtmlString LiActionLink(this HtmlHelper html, string text, string action, string controller)
        {
            var context = html.ViewContext;
            if (context.Controller.ControllerContext.IsChildAction)
                context = html.ViewContext.ParentActionViewContext;
            var routeValues = context.RouteData.Values;
            var currentAction = routeValues["action"].ToString();
            var currentController = routeValues["controller"].ToString();

            var str = String.Format("<li role=\"presentation\"{0}>{1}</li>",
                currentAction.Equals(action, StringComparison.InvariantCulture) &&
                currentController.Equals(controller, StringComparison.InvariantCulture) ?
                " class=\"active\"" :
                String.Empty, html.ActionLink(text, action, controller).ToHtmlString()
            );
            return new MvcHtmlString(str);
        }
    }
}