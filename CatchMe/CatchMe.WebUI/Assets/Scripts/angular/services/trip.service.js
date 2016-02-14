(function () {
    angular
        .module('catchMeApp')
        .service('tripService', tripService);

    tripService.$inject = ['$http'];

    function tripService($http) {
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
                url: CatchMe.Urls.GetAllTrips
            });
        }

        function getTrip(id) {
            return $http({
                method: 'GET',
                data: id,
                url: CatchMe.Urls.GetTrip
            });
        }

        function addTrip(trip) {
            return $http({
                method: 'POST',
                data: trip,
                url: CatchMe.Urls.AddTrip
            });
        }
    }
})();