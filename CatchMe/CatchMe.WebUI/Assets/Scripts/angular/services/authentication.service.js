(function () {
    angular
        .module('catchMeApp')
        .service('authenticationService', authenticationService);

    authenticationService.$inject = ['$http', '$q', 'localStorageService', 'urlConfigs', 'localStorageKeys'];

    function authenticationService($http, $q, localStorageService, urlConfigs, localStorageKeys) {
        //fields        
        var currentUser = getUser();

        //service
        var service = {
            register: register,
            login: login,
            logout: logout,
            isLoggedIn: isLoggedIn,
            user: currentUser
        };
        return service;

        //public functions    
        function register(registrationData) {
            return $http.post(urlConfigs.register, registrationData);
        };

        function login(loginData) {
            var data = "grant_type=password&username=" + loginData.Email + "&password=" + loginData.Password;

            return $http.post(urlConfigs.tokenEndpoint, data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .success(function (response) {
                    localStorageService.set(localStorageKeys.authorizationData, {
                        token: response.access_token,
                        user: { userName: loginData.Email }
                    });
                    changeUser({ userName: loginData.Email });;
                });
        };

        function logout() {
            localStorageService.remove(localStorageKeys.authorizationData);
            changeUser({ userName: '' });
        };

        function isLoggedIn(user) {
            if (user === undefined || user === null) {
                user = currentUser;
            }

            return user.userName !== "" && user.userName === currentUser.userName;
        }

        //private helpers
        function changeUser(user) {
            angular.extend(currentUser, user);
        }

        function getUser() {
            var authData = localStorageService.get(localStorageKeys.authorizationData);

            return authData ? authData.user : { userName: "" };
            //return authData ? { userName: authData.userName } : { userName: "" };
        }
    };
})();