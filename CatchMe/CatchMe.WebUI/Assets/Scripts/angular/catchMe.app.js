(function () {
    angular.module('catchMeApp', ['ngRoute', 'ngAnimate', 'ngMaterial', 'ngMaterialDatePicker'])
        .run(['$rootScope', '$timeout', function ($rootScope, $timeout) {
            $rootScope.$on('$viewContentLoaded', () => {
                $timeout(() => {
                    componentHandler.upgradeAllRegistered();
                });
            });
        }]);
})();