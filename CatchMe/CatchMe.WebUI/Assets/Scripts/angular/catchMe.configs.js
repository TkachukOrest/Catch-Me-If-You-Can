(function () {
    angular
        .module('catchMeApp')
        .config(function ($httpProvider) {
            $httpProvider.interceptors.push('authenticationInterceptor');
        });
})();

