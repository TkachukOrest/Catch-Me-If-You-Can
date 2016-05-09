(function () {
    angular
        .module('catchMeApp')
        .service('tripService', tripService);

    tripService.$inject = ['$http', 'urlConfigs'];

    function tripService($http, urlConfigs) {
        var service = {
            getAllTrips: getAllTrips,
            getTrip: getTrip,
            saveTrip: saveTrip,            
            deleteTrip: deleteTrip
        };

        return service;

        //functions
        function getAllTrips() {
            return $http.get(urlConfigs.getAllTrips);
        }

        function getTrip(id) {
            return $http.get(urlConfigs.getTrip + id);
        }

        function saveTrip(trip, staticMapConfiguration) {
            return $http({
                method: 'POST',
                data: {
                    trip: trip,
                    staticMapConfiguration: staticMapConfiguration
                },
                url: urlConfigs.saveTrip
            });
        }        

        function deleteTrip(tripId) {
            return $http.delete(urlConfigs.deleteTrip + tripId);
        }        
    }
})();