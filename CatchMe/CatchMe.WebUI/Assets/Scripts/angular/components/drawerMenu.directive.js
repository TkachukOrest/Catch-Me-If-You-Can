(function () {
    angular
        .module('catchMeApp')
        .directive('drawerMenu', drawerMenuDirective);

    function drawerMenuDirective() {
        var directive = {
            restrict: 'E',
            templateUrl: '/Assets/Scripts/angular/templates/drawer.html',
            replace: true
        };

        return directive;
    };
})();