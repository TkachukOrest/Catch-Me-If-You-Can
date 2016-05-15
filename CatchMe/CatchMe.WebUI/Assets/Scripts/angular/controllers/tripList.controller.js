(function () {
    angular
        .module('catchMeApp')
        .controller('TripListController', TripListController);

    TripListController.$inject = ['tripService', '$location', 'snackBarNotification', 'authenticationService', '$mdDialog', '$mdMedia'];

    function TripListController(tripService, $location, snackBarNotification, authenticationService, $mdDialog, $mdMedia) {
        var tripListVm = this;

        //view model
        tripListVm.trips = [];
        tripListVm.deleteTrip = deleteTrip;
        tripListVm.editTrip = editTrip;
        tripListVm.openTripDetails = openTripDetails;
        tripListVm.isLoggedIn = authenticationService.isLoggedIn;
        tripListVm.user = authenticationService.user;

        //initializtion
        initialize();

        function initialize() {
            getTrips();
        }

        function getTrips() {
            tripService.getAllTrips().then(function (response) {
                tripListVm.trips = response.data;
            });
        };

        //public function
        function deleteTrip(index, tripId) {
            tripService.deleteTrip(tripId).then(function () {
                snackBarNotification.create('The trip has been deleted successfully.', 'OK');
                tripListVm.trips.splice(index, 1);
            });
        }

        function editTrip(tripId) {
            $location.path('/TripEdit/' + tripId);
        };

        function openTripDetails(tripId, ev) {
            if (tripListVm.isLoggedIn()) {
                $mdDialog.show({
                    templateUrl: 'Assets/Scripts/angular/templates/tripDetails.html',
                    parent: angular.element(document.body),
                    targetEvent: ev,
                    clickOutsideToClose: true,
                    controller: tripDetailsDialogVm,
                    controllerAs: 'tripDetailsVm',
                    bindToController: true,
                    locals: { tripId: tripId }
                });
            } else {
                snackBarNotification.create('Please sign in to get detailed information about this trip.', 'OK');
            }
        };

        function tripDetailsDialogVm($scope, $mdDialog, googleMapService, snackBarNotification, mapPointFactory, authenticationService, tripId) {
            //view model        
            $scope.hide = hide;
            $scope.tripId = tripId;
            $scope.trip = {};
            $scope.driverProfile = {}
            $scope.driverName = '';
            $scope.catchCar = catchCar;

            //fields
            var googleMap;

            //initialization
            (function activate() {
                initializeTrip();
            })();

            function initializeTrip() {
                tripService.getTripDetails(tripId).then(function (response) {
                    $scope.trip = response.data.Trip;
                    $scope.driverProfile = response.data.DriverProfile;
                    $scope.driverName = response.data.DriverName;

                    googleMap = googleMapService.createMap('detailed-trip-map', mapPointFactory.create(50.4501, 30.5234));
                    googleMapService.displayRoute(googleMap,
                        $scope.trip.Origin,
                        $scope.trip.Destination,
                        $scope.trip.WayPoints);
                }, function () {                    
                    hide();
                    snackBarNotification.create('Cannot get trip details. An error has been occured.', 'OK');
                });
            }

            //public methods
            function hide() {
                $mdDialog.hide();
            };

            function catchCar() {
                if ($scope.trip.Seats > $scope.trip.SeatsTaken) {
                    var passangerName = authenticationService.user.userName;
                    var tripId = $scope.tripId;

                    return tripService.catchCar(tripId, passangerName).then(function() {
                        snackBarNotification.create('You have been successfully caught this car. Check your email for details.', 'OK');
                        hide();
                    }, function() {
                        snackBarNotification.create('Cannot catch this trip. Try again later.', 'OK');
                        hide();
                    });
                } else {
                    snackBarNotification.create('All seats are already taken. Try again later.', 'OK');
                }
            }
        }
    };
})();