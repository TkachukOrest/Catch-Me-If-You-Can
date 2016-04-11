(function () {
    angular
        .module('catchMeApp')
        .directive('headerMenu', headerMenuDirective);

    function headerMenuDirective() {
        var directive = {
            restrict: 'A',
            templateUrl: '/Assets/Scripts/angular/templates/header.html',
            controller: 'HeaderMenuController',
            controllerAs: 'headerMenuVm'
        };

        return directive;
    };
})();