(function () {
    angular
        .module('catchMeApp')
        .service('exceptionLoggingService', exceptionLoggingService);               

    exceptionLoggingService.$inject = ['$window', '$injector', '$log', 'localStorageKeys', 'urlConfigs'];
    function exceptionLoggingService($window, $injector, $log, localStorageKeys, urlConfigs) {
        var service = {
            log: logException
        }

        return service;

        //public functions
        function logException(exception) {
            try {
                var $http = $injector.get('$http');
                var localStorageService = $injector.get('localStorageService');
                var authenticationService = $injector.get('authenticationService');
                var authData = authenticationService.getAuthData();

                var errorData = {
                    User: (authData && authData.user ? authData.user.userName : 'Anonim'),
                    Url: ($window.location.href || ""),
                    Message: (exception.cause || ""),
                    StackTrace: (exception.stack || ""),
                    Time: moment()
                };
                              
                var exceptionsData = localStorageService.get(localStorageKeys.exceptionsData) || [];
                exceptionsData.push(errorData);

                if (exceptionsData.length > 5) {
                    $http.post(urlConfigs.logError, exceptionsData).success(function () {
                        localStorageService.remove(localStorageKeys.exceptionsData);
                    });               
                    return;
                }

                var expiredExceptions = getExpiredExceptions(exceptionsData, 1);
                if (expiredExceptions.length > 0) {
                    $http.post(urlConfigs.logError, expiredExceptions).success(function () {
                        var updatedExceptionsData = removeExpiredExceptions(exceptionsData, expiredExceptions);
                        localStorageService.set(localStorageKeys.exceptionsData, updatedExceptionsData);
                    });                    
                    return;
                }

                localStorageService.set(localStorageKeys.exceptionsData, exceptionsData);
            } catch (loggingError) {
                $log.warn("An error has been occured in exception handler");
                $log.log(loggingError);
            }
        }

        //private functions
        function getExpiredExceptions(exceptionsData, expirationTimeInMinutes) {
            var expiredExceptions = [];
            var currentTime = moment();

            angular.forEach(exceptionsData, function (exception) {
                var exceptionExpirationTime = moment(exception.Time).add(expirationTimeInMinutes, 'minutes');
                if (currentTime.isAfter(exceptionExpirationTime)) {
                    expiredExceptions.push(exception);
                }
            });

            return expiredExceptions;
        }

        function removeExpiredExceptions(exceptionsData, expiredExceptions) {
            return exceptionsData.filter(function (value) {
                return expiredExceptions.indexOf(value) == -1;
            });
        }      
    };
})();