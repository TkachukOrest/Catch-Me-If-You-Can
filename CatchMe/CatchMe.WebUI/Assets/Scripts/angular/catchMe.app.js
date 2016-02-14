(function () {
    angular.module('catchMeApp', ['ngRoute', 'ngAnimate', 'ngMaterial'])
    .run(function ($rootScope, $timeout) {
            $rootScope.$on('$viewContentLoaded', () => {
                $timeout(() => {
                    componentHandler.upgradeAllRegistered();
                });
            });
        });;
})();