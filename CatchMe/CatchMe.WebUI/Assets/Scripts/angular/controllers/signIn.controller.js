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
                snackBarNotification.create(error.error_description, 'OK');
             }).finally(function() {
                loadingDialogService.hide();
            });
        };
    };
})();