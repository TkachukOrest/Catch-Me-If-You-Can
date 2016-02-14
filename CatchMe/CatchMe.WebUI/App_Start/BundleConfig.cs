using System.Web.Optimization;

namespace Blog.WebUI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Scripts bundles

            bundles.Add(new ScriptBundle("~/bundles/libs/js").Include(
                        "~/Assets/Scripts/libs/material/material.js",
                        "~/Assets/Scripts/libs/iscroll/iscroll.js",
                        "~/Assets/Scripts/libs/angular/angular.js",
                        "~/Assets/Scripts/libs/angular/angular-animate.js",
                        "~/Assets/Scripts/libs/angular/angular-aria.js",
                        "~/Assets/Scripts/libs/angular/angular-messages.js",
                        "~/Assets/Scripts/libs/angular/angular-material.js",
                        "~/Assets/Scripts/libs/angular/angular-route.js",
                        "~/Assets/Scripts/libs/angular/angular-mocks.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular/catchMe-app/js").Include(
                        "~/Assets/Scripts/angular/catchMe.app.js",
                        "~/Assets/Scripts/angular/catchMe.routes.js",
                        "~/Assets/Scripts/angular/catchMe.material.js",
                        "~/Assets/Scripts/angular/services/trip.service.js",
                        "~/Assets/Scripts/angular/services/googleMap.service.js",
                        "~/Assets/Scripts/angular/controllers/tripList.controller.js",
                        "~/Assets/Scripts/angular/controllers/tripAdd.controller.js",                        
                        "~/Assets/Scripts/angular/controllers/partialDrawerMenu.controller.js",
                        "~/Assets/Scripts/angular/components/hoverClass.directive.js"));

            bundles.Add(new ScriptBundle("~/bundles/common/js").Include(
                        "~/Assets/Scripts/common/namespaces.js"));
            #endregion

            #region Styles bundles
            bundles.Add(new StyleBundle("~/styles/material/css").Include(
                      "~/Assets/Styles/angular-material.css",
                      "~/Assets/Styles/material-design-lite.css",
                      "~/Assets/Styles/material-customized.css",
                      "~/Assets/Styles/material-fonts.css"));
            #endregion

            //BundleTable.EnableOptimizations = true;            
        }
    }
}
