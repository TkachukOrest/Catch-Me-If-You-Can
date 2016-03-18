(function () {
    angular
        .module('catchMeApp')
        .controller('TripListController', TripListController);

    TripListController.$inject = ['tripService', 'googleMapService', 'MapPoint'];

    function TripListController(tripService, googleMapService, MapPoint) {
        var tripListVm = this;

        //view model
        tripListVm.trips = []; 
        
        //private fields
        var googleMaps = {};        

        //initializtion
        initialize();

        function initialize() {
            getTrips();            
        }

        function getTrips() {
            tripService.getAllTrips().then(function (response) {
                tripListVm.trips = response.data;
                //initializeGoogleMaps();
            });
        };

        function initializeGoogleMaps() {
            tripListVm.trips.forEach(function(trip) {
                googleMaps[trip.Id] = googleMapService.createMap('trip-map' + trip.Id, new MapPoint(trip.Origin.Latitude, trip.Origin.Longitude));

                googleMapService.displayRoute(googleMaps[trip.Id], trip.Origin, trip.Destination, trip.WayPoints);
            });
        }        
    };
})();