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
                        "~/Assets/Scripts/app/catch-me.app.js",
                        "~/Assets/Scripts/app/catch-me.routes.js",
                        "~/Assets/Scripts/app/catch-me.constants.js",
                        "~/Assets/Scripts/app/catch-me.material.js",
                        "~/Assets/Scripts/app/catch-me.configs.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular/catchMe-app/js").Include(
                        "~/Assets/Scripts/app/data_models/map-point.model.js",
                        "~/Assets/Scripts/app/data_models/address-details.model.js",
                        "~/Assets/Scripts/app/services/trip.service.js",
                        "~/Assets/Scripts/app/services/google-map.service.js",
                        "~/Assets/Scripts/app/controllers/trip-list.controller.js",
                        "~/Assets/Scripts/app/controllers/trip.controller.js",
                        "~/Assets/Scripts/app/controllers/partial-drawer-menu.controller.js",                        
                        "~/Assets/Scripts/app/controllers/sign-in.controller.js",
                        "~/Assets/Scripts/app/controllers/sign-up.controller.js",
                        "~/Assets/Scripts/app/controllers/header-menu.controller.js",
                        "~/Assets/Scripts/app/services/loading-dialog.service.js",
                        "~/Assets/Scripts/app/services/trip-details-dialog.service.js",                        
                        "~/Assets/Scripts/app/services/snack-bar-notification.service.js",
                        "~/Assets/Scripts/app/services/authentication.service.js",
                        "~/Assets/Scripts/app/services/exception-logging.service.js",
                        "~/Assets/Scripts/app/components/hover-class.directive.js",
                        "~/Assets/Scripts/app/components/is-logged-in.directive.js",
                        "~/Assets/Scripts/app/components/is-valid.directive.js",
                        "~/Assets/Scripts/app/components/compare-to.directive.js",
                        "~/Assets/Scripts/app/components/header-menu.directive.js",
                        "~/Assets/Scripts/app/components/drawer-menu.directive.js",
                        "~/Assets/Scripts/app/components/header-menu-link.directive.js",
                        "~/Assets/Scripts/app/interceptors/authentication.interceptor.js"));
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
