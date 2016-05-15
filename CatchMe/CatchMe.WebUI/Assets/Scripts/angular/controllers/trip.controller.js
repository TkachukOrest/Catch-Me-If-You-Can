(function () {
    angular
        .module('catchMeApp')
        .controller('TripController', TripController);

    TripController.$inject = ['$scope', '$mdDialog', '$location', 'tripService', 'googleMapService', 'loadingDialogService', 'mapPointFactory', '$routeParams', 'maxVehicleUsageTerm', 'snackBarNotification'];

    function TripController($scope, $mdDialog, $location, tripService, googleMapService, loadingDialogService, mapPointFactory, $routeParams, maxVehicleUsageTerm, snackBarNotification) {
        var tripVm = this;

        //view model
        tripVm.trip = {
            Id: null,
            Seats: null,
            Price: null,
            Origin: mapPointFactory.create(0, 0),
            Destination: mapPointFactory.create(0, 0),
            WayPoints: [],            
            StartDateTime: null,
            Vehicle: { Color: "#c0c0c0" },
            StaticMapUrl: null
        }
        tripVm.dateOpts = {}
        tripVm.saveTrip = saveTrip;
        tripVm.deleteWayPoint = deleteWayPoint;
        tripVm.isOriginAndDestinationValid = isOriginAndDestinationValid;
        tripVm.onOriginAddressChange = onOriginAddressChange;
        tripVm.onDestinationAddressChange = onDestinationAddressChange;

        //private fields
        var googleMap;
        var polilinePoints;
        var center;
        var zoom;
        var originPointAutocomplete;
        var destinationPointAutocomplete;
        var wayPointAutocomplete;

        //initialization        
        activate();

        function activate() {            
            initializeMap();
            initializeGoogleAutocompleteInputs();
            initializeDatePickerAttributes();            
            initializeTrip();
        }

        function initializeTrip() {            
            var tripId = $routeParams.tripId;

            if (tripId && tripId > 0) {
                tripService.getTrip(tripId).then(function (response) {
                    fillTrip(response.data);
                    renderMap();
                });
            }
        }

        function initializeMap() {
            loadingDialogService.show();            
            googleMap = googleMapService.createMap('trip-map', mapPointFactory.create(50.4501, 30.5234));

            googleMapService.getCurrentPosition().then(function(position) {
                setMapCenter(position);
            }).finally(function() {
                loadingDialogService.hide();
            });
        }

        function initializeGoogleAutocompleteInputs() {
            originPointAutocomplete = googleMapService.initAutocomplete('origin-point', addOriginPoint);
            destinationPointAutocomplete = googleMapService.initAutocomplete('destination-point', addDestinationPoint);
            wayPointAutocomplete = googleMapService.initAutocomplete('way-point', addWayPoint);
        }

        function initializeDatePickerAttributes() {
            tripVm.dateOpts.currentDate = moment();
            tripVm.dateOpts.maxYear = tripVm.dateOpts.currentDate.year();
            tripVm.dateOpts.minYear = tripVm.dateOpts.maxYear - maxVehicleUsageTerm;            
        }

        //public functions              
        function saveTrip(tripForm) {
            if (!tripForm.$invalid) {
                loadingDialogService.show();
                var staticMapConfiguration = googleMapService.createStaticMapConfiguration(googleMap, polilinePoints, center, zoom);
                
                tripService.saveTrip(tripVm.trip, staticMapConfiguration).then(function () {
                    snackBarNotification.create('The trip has been saved successfully.', 'OK');
                    $location.path('/Trips');
                    googleMapService.clearMap(googleMap);
                }).finally(function () {                    
                    loadingDialogService.hide();
                });
            }
        }

        function deleteWayPoint(index) {
            tripVm.trip.WayPoints.splice(index, 1);
            renderMap();
        }

        function isOriginAndDestinationValid() {
            return !tripVm.trip.Origin.isEmptyPoint() &&
                   !tripVm.trip.Destination.isEmptyPoint() &&
                    tripVm.trip.Origin.IsValid &&
                    tripVm.trip.Destination.IsValid;
        }

        function onOriginAddressChange() {
            tripVm.trip.Origin.IsValid = false;            
        }

        function onDestinationAddressChange() {
            tripVm.trip.Destination.IsValid = false;
        }

        //private helpers
        function addOriginPoint() {
            var originPoint = getSelectedPoint(originPointAutocomplete);

            if (!wayContainsPoint(originPoint)) {
                if (tripVm.trip.Destination.isEmptyPoint()) {
                    tripVm.trip.Origin = originPoint;
                    tripVm.trip.Origin.IsValid = true;
                    setMapCenter(originPoint);                    
                } else {
                    function onWayDirectionExists() {
                        tripVm.trip.Origin = originPoint;
                        tripVm.trip.Origin.IsValid = true;
                        renderMap();
                    }

                    function onWayDirectionNotExists() {
                        tripVm.trip.Origin.IsValid = false;
                        showWayNotFoundAlert();
                    }

                    googleMapService.isWayDirectionValid(originPoint, tripVm.trip.Destination, tripVm.trip.WayPoints)
                        .then(onWayDirectionExists, onWayDirectionNotExists);
                }
            } else {
                tripVm.trip.Origin.IsValid = false;
                showWayAlreadyContainsPointAlert();
            }
        }

        function addDestinationPoint() {
            var destinationPoint = getSelectedPoint(destinationPointAutocomplete);

            if (!wayContainsPoint(destinationPoint)) {
                if (tripVm.trip.Origin.isEmptyPoint()) {
                    tripVm.trip.Destination = destinationPoint;
                    tripVm.trip.Destination.IsValid = true;
                    setMapCenter(destinationPoint);
                } else {
                    function onWayDirectionExists() {
                        tripVm.trip.Destination = destinationPoint;
                        tripVm.trip.Destination.IsValid = true;
                        renderMap();
                    }

                    function onWayDirectionNotExists() {
                        tripVm.trip.Destination.IsValid = false;
                        showWayNotFoundAlert();
                    }

                    googleMapService.isWayDirectionValid(tripVm.trip.Origin, destinationPoint, tripVm.trip.WayPoints)
                        .then(onWayDirectionExists, onWayDirectionNotExists);
                }
            } else {
                tripVm.trip.Destination.IsValid = false;
                showWayAlreadyContainsPointAlert();
            }
        }

        function addWayPoint() {
            var addedWayPoint = getSelectedPoint(wayPointAutocomplete);

            if (!tripVm.trip.Origin.isEmptyPoint() &&
                !tripVm.trip.Destination.isEmptyPoint()) {
                if (!wayContainsPoint(addedWayPoint)) {
                    function onWayDirectionExists() {
                        tripVm.trip.WayPoints.push(addedWayPoint);
                        renderMap();
                    }

                    var wayPoints = tripVm.trip.WayPoints.concat(addedWayPoint);
                    googleMapService.isWayDirectionValid(tripVm.trip.Origin, tripVm.trip.Destination, wayPoints)
                        .then(onWayDirectionExists, showWayNotFoundAlert);
                } else {
                    showWayAlreadyContainsPointAlert();
                }
            }

            tripVm.trip.WayPointToAdd = "";
        }

        function getSelectedPoint(autocomplete) {
            var place = autocomplete.getPlace();            

            return googleMapService.getMapPoint(place);            
        }

        function renderMap() {
            if (!tripVm.trip.Origin.isEmptyPoint() &&
                !tripVm.trip.Destination.isEmptyPoint()) {
                googleMapService.displayRoute(googleMap,
                    tripVm.trip.Origin,
                    tripVm.trip.Destination,
                    tripVm.trip.WayPoints)
                    .then(function(points) {
                        polilinePoints = points;
                        center = googleMapService.getCenter(googleMap);
                        zoom = googleMapService.getZoom(googleMap);
                    });
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
            googleMap.setCenter(point.convertToGoogleMapPoint());
        }        

        function wayContainsPoint(point) {
            return tripVm.trip.Origin.isEqualTo(point) ||
                tripVm.trip.Destination.isEqualTo(point) ||
                wayPointsContains(point);
        }

        function wayPointsContains(point) {
            var result = false;

            tripVm.trip.WayPoints.forEach(function (element) {
                result = result || element.isEqualTo(point);
            });

            return result;
        }

        function fillTrip(tripEntity) {            
            tripVm.trip = tripEntity;
            tripVm.trip.Origin = mapPointFactory.create(tripEntity.Origin.Latitude,
                tripEntity.Origin.Longitude,
                tripEntity.Origin.FormattedLongAddress,
                tripEntity.Origin.FormattedShortAddress,
                tripEntity.Origin.AddressDetails);
            tripVm.trip.Destination = mapPointFactory.create(tripEntity.Destination.Latitude,
                tripEntity.Destination.Longitude,
                tripEntity.Origin.FormattedLongAddress,
                tripEntity.Origin.FormattedShortAddress,
                tripEntity.Origin.AddressDetails);
            tripVm.trip.WayPoints = parseWayPoints(tripEntity.WayPoints);

            tripVm.trip.Origin.IsValid = true;
            tripVm.trip.Destination.IsValid = true;
        }

        function parseWayPoints(points) {
            var wayPoints = [];

            points.forEach(function(point) {
                wayPoints.push(mapPointFactory.create(point.Latitude,
                    point.Longitude,
                    point.FormattedLongAddress,
                    point.FormattedShortAddress,
                    point.AddressDetails));
            });

            return wayPoints;
        }
    };
})();