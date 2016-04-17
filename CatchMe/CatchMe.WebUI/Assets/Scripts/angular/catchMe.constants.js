(function () {
    angular
        .module('catchMeApp')
        .constant('urlConfigs', {
            getAllTrips: 'api/Trip/GetAllTrips/',
            getTrip: 'api/Trip/GetTripById/',
            saveTrip: 'api/Trip/saveTrip/',
            deleteTrip: 'api/Trip/DeleteTrip/',
            register: 'http://localhost:50144/api/Account/Register/',
            logout: 'http://localhost:50144/api/Account/Logout',
            tokenEndpoint: 'http://localhost:50144//OAuth/Token'
        })
        .constant('googleMapConfigs', {
            styles: [
                //{ "stylers": [{ "saturation": -100 }, { "gamma": 1 }] },
                //{
                //    "featureType": "all",
                //    "elementType": "labels.text.stroke",
                //    "stylers": [{ "visibility": "off" }]
                //}, {
                //    "featureType": "road",
                //    "elementType": "geometry",
                //    "stylers": [{ "visibility": "simplified" }]
                //}, {
                //    "featureType": "road.local",
                //    "elementType": "labels.text",
                //    "stylers": [{ "weight": 0.5 }, { "color": "#333333" }]
                //}, {
                //    "featureType": "transit.station",
                //    "elementType": "labels.icon",
                //    "stylers": [{ "gamma": 1 }, { "saturation": 50 }]
                //}, {
                //    "featureType": "landscape.natural",
                //    "elementType": "all",
                //    "stylers": [
                //        { "hue": "#e0e0e0" },
                //        { "saturation": -100 },
                //        { "lightness": -8 },
                //        { "visibility": "off" }
                //    ]
                //}
            ]
        })
        .constant('maxVehicleUsageTerm', 50);
})();

