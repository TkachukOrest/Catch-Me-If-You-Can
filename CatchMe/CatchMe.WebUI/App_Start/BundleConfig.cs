using System.Web.Optimization;

namespace Blog.WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts bundles

            bundles.Add(new ScriptBundle("~/bundles/common-libs/js").Include(
                        "~/Assets/Scripts/libs/jquery-{version}.js",
                        "~/Assets/Scripts/libs/jquery.validate*",
                        "~/Assets/Scripts/libs/material.js",
                        "~/Assets/Scripts/libs/iscroll.js"));           

            bundles.Add(new ScriptBundle("~/bundles/angular/js").Include(
                        "~/Assets/Scripts/libs/angular.js",
                        "~/Assets/Scripts/libs/angular-route.js",
                        "~/Assets/Scripts/libs/angular-mocks.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular/catchMe-app/js").Include(
                        "~/Assets/Scripts/angular/app/catchMeApp.js",
                        "~/Assets/Scripts/angular/configs/catchMe.routes.js",
                        "~/Assets/Scripts/angular/controllers/TripListController.js",
                        "~/Assets/Scripts/angular/controllers/AddNewTripController.js"));

            bundles.Add(new ScriptBundle("~/bundles/common/js").Include(
                        "~/Assets/Scripts/namespaces.js",
                        "~/Assets/Scripts/common/services/GoogleMapsService.js"));
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
