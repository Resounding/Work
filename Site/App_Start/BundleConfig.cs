using System.Web.Optimization;

namespace Site
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/bundle")
                .Include("~/css/bootstrap.css")
                .Include("~/css/bootstrap-responsive.css")
                .Include("~/css/app.css")
            );

            bundles.Add(new ScriptBundle("~/js/bundle")
                .Include("~/js/jquery-1.8.3.js")
                .Include("~/js/bootstrap.js")
            );

            // Code removed for clarity.
            //BundleTable.EnableOptimizations = false;
        }
    }
}