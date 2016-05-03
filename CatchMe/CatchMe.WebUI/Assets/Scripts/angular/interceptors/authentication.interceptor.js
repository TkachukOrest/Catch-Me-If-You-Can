(function () {
    angular
        .module('catchMeApp')
        .factory('authenticationInterceptor', authenticationInterceptor);

    authenticationInterceptor.$inject = ['$q', '$injector', '$location', 'localStorageService', 'localStorageKeys'];

    function authenticationInterceptor($q, $injector, $location, localStorageService, localStorageKeys) {
        //public functions
        function request(config) {
            config.headers = config.headers || {};

            var authData = localStorageService.get(localStorageKeys.authorizationData);

            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        }

        function responseError(response) {
            if (response.status === 401 || response.status === 403) {
                var authService = $injector.get('authenticationService');
                authService.logout();

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