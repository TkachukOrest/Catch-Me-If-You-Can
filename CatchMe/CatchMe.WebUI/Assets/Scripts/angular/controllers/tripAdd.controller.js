(function () {
    angular
        .module('catchMeApp')
        .controller('TripAddController', TripAddController);

    TripAddController.$inject = ['$scope', 'tripService', 'googleMapService'];

    function TripAddController($scope, tripService, googleMapService) {
        var addTripVm = this;

        //view model
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
        addTripVm.datePickerOpts = {}
        addTripVm.addTrip = addTrip;
        addTripVm.deleteWayPoint = deleteWayPoint;

        //private fields
        var googleMap;
        var originPointAutocomplete;
        var destinationPointAutocomplete;
        var wayPointAutocomplete;

        //initialization        
        activate();

        function activate() {
            initializeMap();
            initializeGoogleAutocompleteInputs();
            initializeDatePickerAttributes();
        }

        function initializeMap() {
            googleMap = googleMapService.createMap('trip-map', {
                Latitude: 50.4501,
                Longitude: 30.5234
            });

            googleMapService.getCurrentPosition().then(function (position) {
                googleMap.setCenter(position);
            });
        }

        function initializeGoogleAutocompleteInputs() {
            originPointAutocomplete = googleMapService.initAutocomplete('origin-point', addOriginPoint);
            destinationPointAutocomplete = googleMapService.initAutocomplete('destination-point', addDestinationPoint);
            wayPointAutocomplete = googleMapService.initAutocomplete('way-point', addWayPoint);
        }

        function initializeDatePickerAttributes() {
            addTripVm.datePickerOpts.minDate = moment();
        }

        //public functions              
        function addTrip() {
            tripService.addTrip(addTripVm.trip);
        }

        function deleteWayPoint(index) {
            addTripVm.trip.WayInfo.WayPoints.splice(index, 1);
        }

        //private helpers
        function addOriginPoint() {
            var originPoint = getAutocompletedPoint(originPointAutocomplete);

            if (isEmptyPoint(addTripVm.trip.WayInfo.Destination)) {
                addTripVm.trip.WayInfo.Origin = originPoint;
                setMapCenter(originPoint);
            } else {
                function onWayDirectionExists() {
                    addTripVm.trip.WayInfo.Origin = originPoint;
                    renderMap();
                }

                googleMapService.isWayDirectionValid(originPoint, addTripVm.trip.WayInfo.Destination, addTripVm.trip.WayInfo.WayPoints)
                   .then(onWayDirectionExists, showWayNotFoundAlert);
            }
        }

        function addDestinationPoint() {
            var destinationPoint = getAutocompletedPoint(destinationPointAutocomplete);

            if (isEmptyPoint(addTripVm.trip.WayInfo.Origin)) {
                addTripVm.trip.WayInfo.Destination = destinationPoint;
                setMapCenter(destinationPoint);

            } else {
                function onWayDirectionExists() {
                    addTripVm.trip.WayInfo.Destination = destinationPoint;
                    renderMap();
                }

                googleMapService.isWayDirectionValid(addTripVm.trip.WayInfo.Origin, destinationPoint, addTripVm.trip.WayInfo.WayPoints)
                    .then(onWayDirectionExists, showWayNotFoundAlert);
            }
        }

        function addWayPoint() {
            var addedWayPoint = getAutocompletedPoint(wayPointAutocomplete);

            if (!isEmptyPoint(addTripVm.trip.WayInfo.Origin) &&
                !isEmptyPoint(addTripVm.trip.WayInfo.Destination)) {
                function onWayDirectionExists() {
                    addTripVm.trip.WayInfo.WayPoints.push(addedWayPoint);
                    renderMap();
                }

                var wayPoints = addTripVm.trip.WayInfo.WayPoints.concat(addedWayPoint);
                googleMapService.isWayDirectionValid(addTripVm.trip.WayInfo.Origin, addTripVm.trip.WayInfo.Destination, wayPoints)
                    .then(onWayDirectionExists, showWayNotFoundAlert);
            }
        }

        function getAutocompletedPoint(autocomplete) {
            var place = autocomplete.getPlace();

            return {
                Address: place.formatted_address,
                Latitude: place.geometry.location.lat(),
                Longitude: place.geometry.location.lng()
            };
        }

        function renderMap() {
            if (!isEmptyPoint(addTripVm.trip.WayInfo.Origin) &&
                !isEmptyPoint(addTripVm.trip.WayInfo.Destination)) {
                googleMapService.displayRoute(googleMap,
                    addTripVm.trip.WayInfo.Origin,
                    addTripVm.trip.WayInfo.Destination,
                    addTripVm.trip.WayInfo.WayPoints);
            }
        }

        function isEmptyPoint(point) {
            return (!point.Address) ||
                (point.Latitude === 0) ||
                (point.longitude === 0);
        }

        function showWayNotFoundAlert() {
            alert("No route could be found between");
        }

        function setMapCenter(point) {
            googleMap.setCenter({ lat: point.Latitude, lng: point.Longitude });
        }
    };
})();