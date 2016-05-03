(function () {
    angular
     .module('catchMeApp')
     .directive('isLoggedIn', isLoggedIn);

    isLoggedIn.$inject = ['authenticationService'];

    function isLoggedIn(authenticationService) {
        var directive = {
            restrict: 'A',
            link: link
        };

        return directive;

        function link(scope, element) {
            var prevDisp = element.css('display');

            scope.user = authenticationService.user;
            scope.$watch('user', function (user) {
                if (!authenticationService.isLoggedIn(user))
                    element.css('display', 'none');
                else
                    element.css('display', prevDisp);
            }, true);
        }
    };
})();
