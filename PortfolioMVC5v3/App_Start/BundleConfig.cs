using System.Web.Optimization;

namespace PortfolioMVC5v3
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/adminCss").Include(
                "~/Content/admin/style.min.css",
                "~/Content/fontawesome.css",
                "~/Content/regular.css",
                "~/Content/solid.css",
                "~/Content/brands.css",
                "~/Content/admin/kendo.common.min.css",
                "~/Content/admin/kendo.moonlight.mobile.min.css",
                "~/Content/admin/kendo.moonlight.min.css"
                ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css",
                "~/Content/fontawesome.css",
                "~/Content/regular.css",
                "~/Content/solid.css",
                "~/Content/brands.css"
            ));

            bundles.Add(new StyleBundle("~/Content/projectCardCss").Include(
                "~/Content/vertical.css",
                "~/Content/lightbox.css"
            ));

            bundles.Add(new ScriptBundle("~/bundles/projectCardJs").Include(
                "~/Scripts/plugins.js",
                "~/Scripts/sly.min.js",
                "~/Scripts/vertical.js",
                "~/Scripts/lightbox.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/adminLayout").Include(
              "~/Scripts/jquery-3.3.1.min.js",
              "~/Scripts/umd/popper.min.js",
              "~/Scripts/perfect-scrollbar/dist/perfect-scrollbar.jquery.min.js",
              "~/Scripts/bootstrap.min.js",
              "~/Scripts/waves.js",
              "~/Scripts/sidebarmenu.js",
              "~/Scripts/custom.js",
              "~/Scripts/Kendo/kendo.all.min.js",
              "~/Scripts/Kendo/kendo.culture.pl.min.js",
              "~/Scripts/Kendo/kendo.messages.pl-PL.min.js",
              "~/Scripts/sweetalert2@8.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
