﻿(function () {
    angular.module('catchMeApp', ['ngRoute', 'ngAnimate', 'ngMaterial', 'ngMaterialDatePicker'])
    .run(function ($rootScope, $timeout) {
            $rootScope.$on('$viewContentLoaded', () => {
                $timeout(() => {
                    componentHandler.upgradeAllRegistered();
                });
            });
        });;
})();