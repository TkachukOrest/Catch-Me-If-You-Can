(function () {
    angular
        .module('catchMeApp')
        .constant('urlConfigs', {
            GetAllTrips: 'api/Trip/GetAllTrips',
            GetTrip: 'api/Trip/GetTrip',
            AddTrip: 'api/Trip/AddTrip'
        });
})();

