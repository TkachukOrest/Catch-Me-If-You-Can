(function () {
    angular
        .module('catchMeApp')
        .controller('SignUpController', SignUpController);

    SignUpController.$inject = ['$location', 'authenticationService', 'snackBarNotification', 'loadingDialogService'];

    function SignUpController($location, authenticationService, snackBarNotification, loadingDialogService) {
        var signUpVm = this;

        //view model
        signUpVm.registerData = {
            FirstName: "",
            LastName: "",
            Email: "",
            Password: "",
            ConfirmPassword: ""
        };
        signUpVm.register = register;

        //private functions
        function register() {
            loadingDialogService.show();

            authenticationService.register(signUpVm.registerData).then(function () {
                $location.path('/SignIn');                
                snackBarNotification.create('Your account has been successfully created.', 'OK');
            }, function (response) {
                snackBarNotification.create("Failed to register user due to: " + parseErrors(response), 'OK');
            }).finally(function () {
                loadingDialogService.hide();
            });
        };

        //helpers
        function parseErrors(response) {
            var errors = [];
            for (var key in response.data.ModelState) {
                for (var i = 0; i < response.data.ModelState[key].length; i++) {
                    errors.push(response.data.ModelState[key][i]);
                }
            }
            return errors.join(', ');            
        }

    };
})();