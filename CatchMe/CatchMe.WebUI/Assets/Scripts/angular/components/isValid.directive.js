(function () {
    angular
     .module('catchMeApp')
     .directive('isValid', isValidDirective);

    function isValidDirective() {
        var directive = {
            restrict: 'A',
            require: 'ngModel',
            scope: {
                isValid: '='
            },
            link: link
        };

        return directive;

        function link(scope, element, attr, ngModelCtrl) {
            scope.$watch(function () {
                return scope.isValid;
            }, function (newValue, oldValue) {
                if (newValue || oldValue) {
                    ngModelCtrl.$setValidity("is-valid", newValue);
                }                 
            });            
        }
    };
})();
