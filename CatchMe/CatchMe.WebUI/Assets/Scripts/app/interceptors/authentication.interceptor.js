(function () {
    angular
        .module('catchMeApp')
        .factory('authenticationInterceptor', authenticationInterceptor);

    authenticationInterceptor.$inject = ['$q', '$location', '$injector'];

    function authenticationInterceptor($q, $location, $injector) {
        //public functions
        function request(config) {
            config.headers = config.headers || {};

            var authenticationService = $injector.get('authenticationService');
            var authData = authenticationService.getAuthData();

            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        }

        function responseError(response) {
            if (response.status === 401 || response.status === 403) {
                var authenticationService = $injector.get('authenticationService');
                authenticationService.logout();
                $location.path('/SignIn');
            }

            return $q.reject(response);
        }

        //interceptor
        var authInterceptorServiceFactory = {};

        authInterceptorServiceFactory.request = request;
        authInterceptorServiceFactory.responseError = responseError;        

        return authInterceptorServiceFactory;
    };
})();