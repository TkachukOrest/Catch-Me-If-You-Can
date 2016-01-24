using System.Web.Optimization;

namespace Blog.WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts bundles
            bundles.Add(new ScriptBundle("~/bundles/common/js").Include(
                        "~/Assets/Scripts/libs/jquery-{version}.js",
                        "~/Assets/Scripts/libs/jquery.validate*",
                        "~/Assets/Scripts/libs/material.js"));            

            bundles.Add(new ScriptBundle("~/bundles/iscroll/js").Include(
                        "~/Assets/Scripts/libs/iscroll.js",
                        "~/Assets/Scripts/iscroll-initialization.js"));

            bundles.Add(new ScriptBundle("~/bundles/google-map/js").Include(
                      "~/Assets/Scripts/libs/google-map.js",
                      "~/Assets/Scripts/google-map-initialization.js"));
            #endregion

            #region Styles bundles
            bundles.Add(new StyleBundle("~/styles/material/css").Include(
                      "~/Assets/Styles/material-design-lite.css",
                      "~/Assets/Styles/material-customized.css",
                      "~/Assets/Styles/material-fonts.css"));
            #endregion

            //BundleTable.EnableOptimizations = true;            
        }
    }
}
