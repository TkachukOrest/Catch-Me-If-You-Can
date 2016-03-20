(function () {
    angular
        .module('catchMeApp')
        .controller('TripListController', TripListController);

    TripListController.$inject = ['tripService', 'googleMapService', 'MapPoint'];

    function TripListController(tripService) {
        var tripListVm = this;

        //view model
        tripListVm.trips = [];         

        //initializtion
        initialize();

        function initialize() {
            getTrips();            
        }

        function getTrips() {
            tripService.getAllTrips().then(function (response) {
                tripListVm.trips = response.data;                
            });
        };        
    };
})();