﻿(function () {
    angular
        .module('catchMeApp')
        .controller('TripListController', TripListController);

    TripListController.$inject = ['tripService'];

    function TripListController(tripService) {
        var tripListVm = this;

        //view model
        tripListVm.trips = [];
        tripListVm.deleteTrip = deleteTrip;
        tripListVm.editTrip = editTrip;

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

        //public function
        function deleteTrip(index, tripId) {            
            tripService.deleteTrip(tripId).then(function() {
                tripListVm.trips.splice(index, 1);
            });
        }

        function editTrip(tripId) {
            
        };
    };
})();