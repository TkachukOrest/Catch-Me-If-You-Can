(function () {
    angular
        .module('catchMeApp')
        .service('tripService', tripService);

    tripService.$inject = ['$http', 'urlConfigs'];

    function tripService($http, urlConfigs) {
        var service = {
            getAllTrips: getAllTrips,
            getTrip: getTrip,
            addTrip: addTrip,
            deleteTrip: deleteTrip
        };

        return service;

        //functions
        function getAllTrips() {
            return $http({
                method: 'GET',
                url: urlConfigs.getAllTrips
            });
        }

        function getTrip(id) {
            return $http({
                method: 'GET',
                data: id,
                url: urlConfigs.getTrip
            });
        }

        function addTrip(trip, staticMapConfiguration) {
            return $http({
                method: 'POST',
                data: {
                    trip: trip,
                    staticMapConfiguration: staticMapConfiguration
                },
                url: urlConfigs.addTrip
            });
        }

        function deleteTrip(tripId) {
            return $http({
                method: 'DELETE',                
                url: urlConfigs.deleteTrip + tripId
            });
        }
    }
})();