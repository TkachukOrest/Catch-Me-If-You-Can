(function () {
    angular
        .module('catchMeApp')
        .directive('drawerMenu', drawerMenuDirective);

    function drawerMenuDirective() {
        var directive = {
            restrict: 'A',
            templateUrl: '/Assets/Scripts/angular/templates/drawer.html'
        };

        return directive;
    };
})();