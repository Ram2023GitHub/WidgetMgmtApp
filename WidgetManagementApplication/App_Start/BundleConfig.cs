using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace WidgetManagementApplication
{
    public class BundleConfig
    {
        // For more information on Bundling, visit https://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/MicrosoftAjax.js",
                    "~/Scripts/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/MicrosoftAjaxTimer.js",
                    "~/Scripts/MicrosoftAjaxWebForms.js"));

            bundles.Add(new ScriptBundle("~/bundles").Include(
                    "~/Scripts/jquery-3.4.1.js",
                    "~/Scripts/jquery-3.4.1.slim.min.js"));

        }
    }
}