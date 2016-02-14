(function () {
    angular
        .module('catchMeApp')
        .controller('TripAddController', TripAddController);

    TripAddController.$inject = ['tripService', 'googleMapService'];

    function TripAddController(tripService, googleMapService) {
        var addTripVm = this;
        addTripVm.trip = {
            WayInfo: {
                Origin: {
                    Address: "",
                    Latitude: 0,
                    Longitude: 0
                },
                Destination: {
                    Address: "",
                    Latitude: 0,
                    Longitude: 0
                },
                WayPoints: []
            },
            TripInfo: {},
            VehicleInfo: {}
        }
        addTripVm.wayPointToAddAddress = "";
        addTripVm.addTrip = addTrip;
        addTripVm.addWayPoint = addWayPoint;

        //fields
        var googleMap;

        //initialization        
        initialize();

        //functions
        function initialize() {
            initializeGoogleMap();
        }

        function initializeGoogleMap() {
            var startPoint = { Latitude: 49.7946898, Longitude: 24.0647954 };
            var endPoint = { Latitude: 49.842582, Longitude: 24.003351 };
            var wayPoints = [{ Latitude: 49.835327, Longitude: 24.0144097 }];

            googleMap = googleMapService.createMap('trip-map', {
                Latitude: 49.7946898,
                Longitude: 24.0647954
            });

            googleMapService.displayRoute(googleMap, startPoint, endPoint, wayPoints);
        }

        function addTrip() {
            tripService.addTrip(addTripVm.trip);
        }

        function addWayPoint() {
            function addGeocodedWayPoint(geocodedPoint) {
                var wayPoint = {
                    Address: addTripVm.wayPointToAddAddress,
                    Latitude: geocodedPoint.Latitude,
                    Longitude: geocodedPoint.Longitude
                }
                addTripVm.trip.WayInfo.WayPoints.push(wayPoint);                

                addTripVm.wayPointToAdd = "";

                googleMapService.displayRoute(googleMap,
                    addTripVm.trip.WayInfo.Origin,
                    addTripVm.trip.WayInfo.Destination,
                    addTripVm.trip.WayInfo.WayPoints);
            }

            googleMapService.decodeAddress(addTripVm.wayPointToAddAddress, addGeocodedWayPoint);            
        }
    };
})();