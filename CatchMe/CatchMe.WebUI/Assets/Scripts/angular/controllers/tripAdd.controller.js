(function () {
    angular
        .module('catchMeApp')
        .controller('TripAddController', TripAddController);

    TripAddController.$inject = ['$scope', '$mdDialog', 'tripService', 'googleMapService'];

    function TripAddController($scope, $mdDialog, tripService, googleMapService) {
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
                tripService.addTrip(addTripVm.trip);
            }
        }

        function deleteWayPoint(index) {
            addTripVm.trip.WayInfo.WayPoints.splice(index, 1);
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
        }

        function addDestinationPoint() {
            var destinationPoint = getSelectedPoint(destinationPointAutocomplete);

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
        }

        function addWayPoint() {
            var addedWayPoint = getSelectedPoint(wayPointAutocomplete);

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

        function isEmptyPoint(point) {
            return (!point.Address) ||
                (point.Latitude === 0) ||
                (point.longitude === 0);
        }

        function showWayNotFoundAlert() {
            $mdDialog.show(
             $mdDialog.alert()
               .parent(angular.element(document.querySelector('#add-new-trip-container')))
               .clickOutsideToClose(true)
               .title('Warning!')
               .textContent('No route could be found! Please, change your route points.')               
               .ok('Ok')               
           );
        }

        function setMapCenter(point) {
            googleMap.setCenter({ lat: point.Latitude, lng: point.Longitude });
        }
    };
})();