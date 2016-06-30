(function () {
    angular
     .module('catchMeApp')
     .directive('compareTo', compareToDirective);

    function compareToDirective() {
        var directive = {
            restrict: 'A',
            require: 'ngModel',
            scope: {
                compareToModel: '=compareTo'
            },
            link: link
        };

        return directive;

        function link(scope, element, attributes, ngModel) {
            ngModel.$validators.compareTo = function (modelValue) {
                return modelValue == scope.compareToModel;
            };

            scope.$watch("compareToModel", function () {
                ngModel.$validate();
            });
        }
    };
})();
