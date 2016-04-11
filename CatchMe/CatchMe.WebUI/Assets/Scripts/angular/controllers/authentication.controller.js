(function () {
    angular
        .module('catchMeApp')
        .controller('AuthenticationController', AuthenticationController);

    AuthenticationController.$inject = ['$location', 'authenticationService'];

    function AuthenticationController($location, authenticationService) {
        //view model
        var authenticationCtrlVm = this;

        authenticationCtrlVm.authentication = authenticationService.authentication;                
    };
})();