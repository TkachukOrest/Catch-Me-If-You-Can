(function () {
    angular
        .module('catchMeApp')
        .directive('hoverClass', hoverClassDirective);    

    function hoverClassDirective() {
        var directive = {
            restrict: 'A',                      
            scope: {
                hoverClass: '@'
            },
            link: link
        };

        return directive;

        function link(scope, element) {
            element.on('mouseenter', function () {                                
                element.addClass(scope.hoverClass);
            });
            element.on('mouseleave', function () {                
                element.removeClass(scope.hoverClass);
            });
        }
    };
})();