(function () {
    angular
        .module('catchMeApp')
        .service('tripService', tripService);

    tripService.$inject = ['$http', 'urlConfigs', 'snackBarNotification'];

    function tripService($http, urlConfigs, snackBarNotification) {
        var service = {
            getAllTrips: getAllTrips,
            getTrip: getTrip,
            saveTrip: saveTrip,            
            deleteTrip: deleteTrip
        };

        return service;

        //functions
        function getAllTrips() {
            return $http.get(urlConfigs.getAllTrips)
                   .error(errorCallback);
        }

        function getTrip(id) {
            return $http.get(urlConfigs.getTrip + id)
                   .error(errorCallback);
        }

        function saveTrip(trip, staticMapConfiguration) {
            return $http({
                method: 'POST',                
                data: {
                    trip: trip,
                    staticMapConfiguration: staticMapConfiguration
                },
                url: urlConfigs.saveTrip
            }).error(errorCallback);
        }        

        function deleteTrip(tripId) {
            return $http.delete(urlConfigs.deleteTrip + tripId)
                        .error(errorCallback);
        }

        function errorCallback() {
            snackBarNotification.create('An error has been occured.', 'OK');
        }
    }
})();