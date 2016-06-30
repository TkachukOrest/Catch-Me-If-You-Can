(function () {
    angular
        .module('catchMeApp')
        .config(interceptorsConfig)
        .config(exceptionHandlerConfig);

    //interceptors configuration
    interceptorsConfig.$inject = ['$httpProvider'];
    function interceptorsConfig($httpProvider) {
        $httpProvider.interceptors.push('authenticationInterceptor');
    };

    //exception handling config
    exceptionHandlerConfig.$inject = ['$provide'];
    function exceptionHandlerConfig($provide) {
        $provide.decorator('$exceptionHandler', extendExceptionHandler);
    }

    //exception handler
    extendExceptionHandler.$inject = ['$delegate', 'exceptionLoggingService', 'snackBarNotification'];
    function extendExceptionHandler($delegate, exceptionLoggingService, snackBarNotification) {
        return function (exception, cause) {
            $delegate(exception, cause);

            snackBarNotification.createIfNotAnyExists('An error has been occured.', 'OK');
            exceptionLoggingService.log(exception);
        };
    }
})();

