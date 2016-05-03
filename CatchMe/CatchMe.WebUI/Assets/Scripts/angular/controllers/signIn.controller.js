(function () {
    angular
        .module('catchMeApp')
        .controller('SignInController', SignInController);

    SignInController.$inject = ['$location', 'authenticationService', 'snackBarNotification', 'loadingDialogService'];

    function SignInController($location, authenticationService, snackBarNotification, loadingDialogService) {
        var signInVm = this;

        //view model
        signInVm.loginData = {
            Email: "",
            Password: ""
        };        
        signInVm.login = login;

        //private functions
        function login() {
            loadingDialogService.show();

            authenticationService.login(signInVm.loginData).then(function () {
                $location.path('/Trips');
                snackBarNotification.create(signInVm.loginData.Email + ', you have been successfully logged in.', 'OK');
            }, function (error) {
                if (error.error_description) {
                    snackBarNotification.create(error.error_description, 'OK');
                } else {
                    snackBarNotification.create("An error has been occured", 'OK');
                }
             }).finally(function() {
                loadingDialogService.hide();
            });
        };
    };
})();