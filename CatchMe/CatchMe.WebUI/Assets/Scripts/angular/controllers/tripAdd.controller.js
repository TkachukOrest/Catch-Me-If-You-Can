(function () {
    angular
        .module('catchMeApp')
        .controller('TripAddController', TripAddController);

    TripAddController.$inject = ['$scope', '$mdDialog', '$location', 'tripService', 'googleMapService'];

    function TripAddController($scope, $mdDialog, $location, tripService, googleMapService) {
        var addTripVm = this;

        //view model
        addTripVm.trip = {
            WayInfo: {
                Origin: {
                    Address: "",
                    Latitude: 0,
                    Longitude: 0,
                    IsValid: false
                },
                Destination: {
                    Address: "",
                    Latitude: 0,
                    Longitude: 0,
                    IsValid: false
                },
                WayPoints: []
            },
            TripInfo: {},
            VehicleInfo: {}
        }
        addTripVm.datePickerOpts = {}
        addTripVm.addTrip = addTrip;
        addTripVm.deleteWayPoint = deleteWayPoint;
        addTripVm.isOriginAndDestinationValid = isOriginAndDestinationValid;

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
        function addTrip(addNewTripForm) {
            if (!addNewTripForm.$invalid) {
                tripService.addTrip(addTripVm.trip).then(function () {
                    $location.path('/Trips');
                });
            }
        }

        function deleteWayPoint(index) {
            addTripVm.trip.WayInfo.WayPoints.splice(index, 1);
            renderMap();
        }

        function isOriginAndDestinationValid() {
            return !isEmptyPoint(addTripVm.trip.WayInfo.Origin) &&
                   !isEmptyPoint(addTripVm.trip.WayInfo.Destination) &&
                    addTripVm.trip.WayInfo.Origin.IsValid &&
                    addTripVm.trip.WayInfo.Destination.IsValid;
        }

        //private helpers
        function addOriginPoint() {
            var originPoint = getSelectedPoint(originPointAutocomplete);

            if (!wayContainsPoint(originPoint)) {
                if (isEmptyPoint(addTripVm.trip.WayInfo.Destination)) {
                    addTripVm.trip.WayInfo.Origin = originPoint;
                    addTripVm.trip.WayInfo.Origin.IsValid = true;
                    setMapCenter(originPoint);
                } else {
                    function onWayDirectionExists() {
                        addTripVm.trip.WayInfo.Origin = originPoint;
                        addTripVm.trip.WayInfo.Origin.IsValid = true;
                        renderMap();
                    }

                    function onWayDirectionNotExists() {
                        addTripVm.trip.WayInfo.Origin.IsValid = false;
                        showWayNotFoundAlert();
                    }

                    googleMapService.isWayDirectionValid(originPoint, addTripVm.trip.WayInfo.Destination, addTripVm.trip.WayInfo.WayPoints)
                        .then(onWayDirectionExists, onWayDirectionNotExists);
                }
            } else {
                addTripVm.trip.WayInfo.Origin.IsValid = false;
                showWayAlreadyContainsPointAlert();
            }
        }

        function addDestinationPoint() {
            var destinationPoint = getSelectedPoint(destinationPointAutocomplete);

            if (!wayContainsPoint(destinationPoint)) {
                if (isEmptyPoint(addTripVm.trip.WayInfo.Origin)) {
                    addTripVm.trip.WayInfo.Destination = destinationPoint;
                    addTripVm.trip.WayInfo.Destination.IsValid = true;
                    setMapCenter(destinationPoint);

                } else {
                    function onWayDirectionExists() {
                        addTripVm.trip.WayInfo.Destination = destinationPoint;
                        addTripVm.trip.WayInfo.Destination.IsValid = true;
                        renderMap();
                    }

                    function onWayDirectionNotExists() {
                        addTripVm.trip.WayInfo.Destination.IsValid = false;
                        showWayNotFoundAlert();
                    }

                    googleMapService.isWayDirectionValid(addTripVm.trip.WayInfo.Origin, destinationPoint, addTripVm.trip.WayInfo.WayPoints)
                        .then(onWayDirectionExists, onWayDirectionNotExists);
                }
            } else {
                addTripVm.trip.WayInfo.Destination.IsValid = false;
                showWayAlreadyContainsPointAlert();
            }
        }

        function addWayPoint() {
            var addedWayPoint = getSelectedPoint(wayPointAutocomplete);

            if (!isEmptyPoint(addTripVm.trip.WayInfo.Origin) &&
                !isEmptyPoint(addTripVm.trip.WayInfo.Destination)) {
                if (!wayContainsPoint(addedWayPoint)) {
                    function onWayDirectionExists() {
                        addTripVm.trip.WayInfo.WayPoints.push(addedWayPoint);
                        renderMap();
                    }

                    var wayPoints = addTripVm.trip.WayInfo.WayPoints.concat(addedWayPoint);
                    googleMapService.isWayDirectionValid(addTripVm.trip.WayInfo.Origin, addTripVm.trip.WayInfo.Destination, wayPoints)
                        .then(onWayDirectionExists, showWayNotFoundAlert);
                } else {
                    showWayAlreadyContainsPointAlert();
                }
            }

            addTripVm.trip.WayInfo.WayPointToAdd = "";
        }

        function getSelectedPoint(autocomplete) {
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

        function showWayNotFoundAlert() {
            showWarningBox('No route could be found! Please, change your route points.');
        }

        function showWayAlreadyContainsPointAlert() {
            showWarningBox('Route already contains this point! Please, change your route.');
        }

        function showWarningBox(message) {
            $mdDialog.show(
               $mdDialog.alert()
                 .parent(angular.element(document.querySelector('#add-new-trip-container')))
                 .clickOutsideToClose(true)
                 .title('Warning!')
                 .textContent(message)
                 .ok('Ok')
             );
        }

        function setMapCenter(point) {
            googleMap.setCenter({ lat: point.Latitude, lng: point.Longitude });
        }

        function isEmptyPoint(point) {
            return (!point.Address) ||
                (point.Latitude === 0) ||
                (point.longitude === 0);
        }

        function wayContainsPoint(point) {
            return isEqualPoints(addTripVm.trip.WayInfo.Origin, point) ||
                isEqualPoints(addTripVm.trip.WayInfo.Destination, point) ||
                wayPointsContains(point);
        }

        function wayPointsContains(point) {
            var result = false;

            addTripVm.trip.WayInfo.WayPoints.forEach(function (element) {
                result = result || isEqualPoints(element, point);
            });

            return result;
        }

        function isEqualPoints(left, right) {
            return left.Address === right.Address &&
                left.Latitude === right.Latitude &&
                left.Longitude === right.Longitude;
        }

    };
})();