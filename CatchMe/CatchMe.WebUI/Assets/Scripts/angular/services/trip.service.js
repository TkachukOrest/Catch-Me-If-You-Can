(function () {
    angular
        .module('catchMeApp')
        .service('tripService', tripService);

    tripService.$inject = ['$http', 'urlConfigs'];

    function tripService($http, urlConfigs) {
        var service = {
            getAllTrips: getAllTrips,
            getTrip: getTrip,
            addTrip: addTrip
        };

        return service;

        //functions
        function getAllTrips() {
            return $http({
                method: 'GET',
                url: urlConfigs.GetAllTrips
            });
        }

        function getTrip(id) {
            return $http({
                method: 'GET',
                data: id,
                url: urlConfigs.GetTrip
            });
        }

        function addTrip(trip, staticMapConfiguration) {
            return $http({
                method: 'POST',
                data: {
                    trip: trip,
                    staticMapConfiguration: staticMapConfiguration
                },
                url: urlConfigs.AddTrip
            });
        }
    }
})();