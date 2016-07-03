(function () {
    angular
        .module('catchMeApp')
        .service('tripDetailsDialogService', tripDetailsDialog);

    tripDetailsDialog.$inject = ['$mdDialog'];

    function tripDetailsDialog($mdDialog) {
        var service = {
            show: show,
            hide: hide
        };

        return service;

        //functions
        function show(tripId, targetEvent) {
            $mdDialog.show({
                templateUrl: 'Assets/Scripts/app/templates/trip-details.html',
                parent: angular.element(document.body),
                targetEvent: targetEvent,
                clickOutsideToClose: true,
                controller: tripDetailsDialogVm,
                controllerAs: 'tripDetailsVm',
                bindToController: true,
                locals: { tripId: tripId }
            });
        }

        function hide() {
            $mdDialog.hide();
        }        
    };

    tripDetailsDialogVm.$inject = ['$scope', '$mdDialog', 'tripService', 'googleMapService', 'snackBarNotification', 'mapPointFactory', 'authenticationService', 'loadingDialogService', 'tripId'];
    function tripDetailsDialogVm($scope, $mdDialog, tripService, googleMapService, snackBarNotification, mapPointFactory, authenticationService, loadingDialogService, tripId) {
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
            tripService.getTripDetails(tripId).then(function(response) {
                $scope.trip = response.data.Trip;
                $scope.driverProfile = response.data.DriverProfile;
                $scope.driverName = response.data.DriverName;

                googleMap = googleMapService.createMap('detailed-trip-map', mapPointFactory.create(50.4501, 30.5234));
                googleMapService.displayRoute(googleMap,
                    $scope.trip.Origin,
                    $scope.trip.Destination,
                    $scope.trip.WayPoints);
            }, function() {
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
                loadingDialogService.show();

                return tripService.catchCar(tripId, passangerName).then(function() {
                    snackBarNotification.create('You have been successfully caught this car. Check your email for details.', 'OK');
                    hide();
                }, function() {
                    snackBarNotification.create('Cannot catch this trip. Try again later.', 'OK');
                    hide();
                }).finally(function () {
                    loadingDialogService.hide();
                });;
            } else {
                snackBarNotification.create('All seats are already taken. Try again later.', 'OK');
            }
        }
    }
})();