(function () {
    angular
        .module('catchMeApp')
        .controller('TripListController', TripListController);

    function TripListController() {
        var tripListVm = this;

        //view model                
        
        //initializtion
        initialize();

        //methods 
        function initialize() {
            initializeGoogleMaps();
        }

        function initializeGoogleMaps() {           
        }
    };
})();