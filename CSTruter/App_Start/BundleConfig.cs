using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CSTruter
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(
                new StyleBundle("~/bundles/css")
                    .Include(
                        "~/Content/bootstrap.css",
                        "~/Content/ui-bootstrap-csp.css",
                        "~/Content/app.css"
                    )
            );

            bundles.Add(
                new ScriptBundle("~/bundles/js")
                    .Include( // Required dependencies
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-messages.js",
                        "~/Scripts/angular-animate.js",
                        "~/Scripts/angular-touch.js",
                        "~/Scripts/angular-ui/ui-bootstrap.js"
                    )
                    .Include( // Reusable modules
                        "~/Scripts/cstruter/cstruter.validate.unobtrusive.js"
                    )
                    .Include( // Application Specific
                        "~/app/app.module.js",
                        "~/app/people/person.module.js",
                        "~/app/people/person.controller.js"
                    )
            );
        }
    }
}