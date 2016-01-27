(function () {
	angular
        .module('catchMeApp')
        .config(config);

	function config($routeProvider, $locationProvider) {	    
		$routeProvider.when('/', {
			templateUrl: '/Templates/TripList.html',
			controller: 'TripListController',
			controllerAs: 'tripListVm'
		});

        $routeProvider.when('/AddNewTrip', {
			templateUrl: '/Templates/AddNewTrip.html',
			controller: 'AddNewTripController',
			controllerAs: 'addNewTripVm'
		});

		$routeProvider.when('/Trips', { redirectTo: '/' });

		$routeProvider.otherwise({ redirectTo: '/' });

	    $locationProvider.html5Mode(true);
	}
})();