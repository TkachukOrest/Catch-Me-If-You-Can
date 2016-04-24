(function () {
    angular
        .module('catchMeApp')
        .service('authenticationService', authenticationService);

    authenticationService.$inject = ['$http', '$q', 'localStorageService', 'urlConfigs'];

    function authenticationService($http, $q, localStorageService, urlConfigs) {
        //fields        
        var authentication = {
            isAuth: false,
            userName: ""
        };

        //service
        var service = {
            register: register,
            login: login,
            logout: logout,
            authentication: authentication
        };
        return service;

        //public functions    
        function register(registrationData) {
            return $http.post(urlConfigs.register, registrationData);
        };

        function login(loginData) {
            var data = "grant_type=password&username=" + loginData.Email + "&password=" + loginData.Password;

            var deferred = $q.defer();

            $http.post(urlConfigs.tokenEndpoint, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .success(function(response) {
                    authentication.isAuth = true;
                    authentication.userName = loginData.Email;

                    deferred.resolve(response);
                }).error(function(err) {
                    deferred.reject(err);
                });

            return deferred.promise;
        };

        function logout() {
            return $http.post(urlConfigs.logout).success(function () {
                authentication.isAuth = false;
                authentication.userName = "";
            });
        };
    };
})();