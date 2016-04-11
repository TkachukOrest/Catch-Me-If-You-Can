(function () {
    angular
        .module('catchMeApp')
        .controller('HeaderMenuController', HeaderMenuController);

    HeaderMenuController.$inject = ['$location', 'authenticationService', 'loadingDialogService', 'snackBarNotification'];

    function HeaderMenuController($location, authenticationService, loadingDialogService, snackBarNotification) {
        //view model
        var headerMenuVm = this;

        headerMenuVm.authentication = authenticationService.authentication;
        headerMenuVm.logout = logout;
        headerMenuVm.signIn = signIn;

        //public methods
        function logout() {
            loadingDialogService.show();

            authenticationService.logout().then(function() {
                snackBarNotification.create('You have been successfully logged out.', 'OK');
            }).finally(function() {
                loadingDialogService.hide();                
            });
        };

        function signIn() {
            $location.path('/SignIn');
        }
    };
})();