using System.Web.Optimization;

namespace CatchMe.WebUI
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
                        "~/Assets/Scripts/libs/angular/angular-mocks.js",
                        "~/Assets/Scripts/libs/angular/angular-touch.js",
                        "~/Assets/Scripts/libs/moment/moment.js",
                        "~/Assets/Scripts/libs/material/material-datetimepicker.js",
                        "~/Assets/Scripts/libs/input-masks/input-masks.js",
                        "~/Assets/Scripts/libs/angularLocalStorage/angular-local-storage.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular/catchMe-module/js").Include(
                        "~/Assets/Scripts/angular/catchMe.app.js",
                        "~/Assets/Scripts/angular/catchMe.routes.js",
                        "~/Assets/Scripts/angular/catchMe.constants.js",
                        "~/Assets/Scripts/angular/catchMe.material.js",
                        "~/Assets/Scripts/angular/catchMe.configs.js"));                        

            bundles.Add(new ScriptBundle("~/bundles/angular/catchMe-app/js").Include(
                        "~/Assets/Scripts/angular/data_models/mapPoint.model.js",
                        "~/Assets/Scripts/angular/services/trip.service.js",
                        "~/Assets/Scripts/angular/services/googleMap.service.js",
                        "~/Assets/Scripts/angular/controllers/tripList.controller.js",
                        "~/Assets/Scripts/angular/controllers/trip.controller.js",
                        "~/Assets/Scripts/angular/controllers/partialDrawerMenu.controller.js",
                        "~/Assets/Scripts/angular/controllers/dialog.controller.js",
                        "~/Assets/Scripts/angular/controllers/signIn.controller.js",
                        "~/Assets/Scripts/angular/controllers/signUp.controller.js",
                        "~/Assets/Scripts/angular/controllers/headerMenu.controller.js",
                        "~/Assets/Scripts/angular/services/loadingDialog.service.js",
                        "~/Assets/Scripts/angular/services/snackBarNotification.service.js",
                        "~/Assets/Scripts/angular/services/authentication.service.js",
                        "~/Assets/Scripts/angular/services/exceptionLogging.service.js",
                        "~/Assets/Scripts/angular/components/hoverClass.directive.js",
                        "~/Assets/Scripts/angular/components/isLoggedIn.directive.js",
                        "~/Assets/Scripts/angular/components/isValid.directive.js",
                        "~/Assets/Scripts/angular/components/headerMenu.directive.js",
                        "~/Assets/Scripts/angular/components/drawerMenu.directive.js",                        
                        "~/Assets/Scripts/angular/components/headerMenuLink.directive.js",
                        "~/Assets/Scripts/angular/interceptors/authentication.interceptor.js"));
            #endregion

            #region Styles bundles
            bundles.Add(new StyleBundle("~/styles/material/css").Include(
                      "~/Assets/Styles/angular-material.css",
                      "~/Assets/Styles/material-design-lite.css",
                      "~/Assets/Styles/material-customized.css",
                      "~/Assets/Styles/material-fonts.css",
                      "~/Assets/Styles/material-datetimepicker.css",
                      "~/Assets/Styles/paper-snackbar.css"));
            #endregion

            BundleTable.EnableOptimizations = false;            
        }
    }
}
