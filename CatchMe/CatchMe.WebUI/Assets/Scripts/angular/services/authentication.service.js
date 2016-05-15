(function () {
    angular
        .module('catchMeApp')
        .service('authenticationService', authenticationService);

    authenticationService.$inject = ['$http', '$q', 'localStorageService', 'localStorageKeys', 'urlConfigs'];

    function authenticationService($http, $q, localStorageService, localStorageKeys, urlConfigs) {
        //fields                
        var currentUser = { userName: '' };

        //initialization        
        activate();

        function activate() {
            getUser();
        }

        //service
        var service = {
            register: register,
            login: login,
            logout: logout,
            isLoggedIn: isLoggedIn,
            user: currentUser,
            getAuthData: getAuthData
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
                    setAuthData({
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

        function getAuthData() {
            return localStorageService.get(localStorageKeys.authorizationData);
        }

        //private helpers
        function setAuthData(authData) {
            localStorageService.set(localStorageKeys.authorizationData, authData);
        }

        function changeUser(user) {
            angular.extend(currentUser, user);
        }

        function getUser() {
            var authData = getAuthData();

            if (authData && authData.user && authData.user.userName) {
                return $http.get(urlConfigs.verifyUserName, { params: {userName: authData.user.userName}})
                    .then(function (response) {
                        if (!response.data) {
                            logout();
                            return;
                        }

                        changeUser(authData.user);
                    }, function () {
                        logout();
                        return;
                    });
            }

            changeUser({ userName: '' });
        }
    };
})();