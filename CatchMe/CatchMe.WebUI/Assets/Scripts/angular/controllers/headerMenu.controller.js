(function () {
    angular
        .module('catchMeApp')
        .controller('HeaderMenuController', HeaderMenuController);

    HeaderMenuController.$inject = ['$location', 'authenticationService', 'loadingDialogService', 'snackBarNotification'];

    function HeaderMenuController($location, authenticationService, loadingDialogService, snackBarNotification) {
        //view model
        var headerMenuVm = this;
        
        headerMenuVm.logout = logout;
        headerMenuVm.signIn = signIn;
        headerMenuVm.isSignIn = isSignIn;

        //public methods
        function isSignIn() {
            return authenticationService.isLoggedIn();
        }

        function logout() {            
            authenticationService.logout();

            snackBarNotification.create('You have been successfully logged out.', 'OK');                        
        };

        function signIn() {
            $location.path('/SignIn');
        }
    };
})();