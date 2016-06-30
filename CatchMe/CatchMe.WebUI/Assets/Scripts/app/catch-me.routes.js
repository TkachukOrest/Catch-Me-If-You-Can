(function () {
	angular
        .module('catchMeApp')
        .config(config);

    config.$inject = ['$routeProvider', '$locationProvider'];

	function config($routeProvider, $locationProvider) {	    
		$routeProvider.when('/', {
			templateUrl: '/Assets/Scripts/app/templates/trip-list.html'			
		});

		$routeProvider.when('/TripAdd', {
		    templateUrl: '/Assets/Scripts/app/templates/trip.html'
		});

		$routeProvider.when('/TripEdit/:tripId', {
		    templateUrl: '/Assets/Scripts/app/templates/trip.html'
		});

		$routeProvider.when('/Trips', { redirectTo: '/' });

		$routeProvider.when('/SignIn', {
		    templateUrl: '/Assets/Scripts/app/templates/sign-in.html'
		});

		$routeProvider.when('/SignUp', {
		    templateUrl: '/Assets/Scripts/app/templates/sign-up.html'
		});

		$routeProvider.otherwise({ redirectTo: '/' });

		//$locationProvider.html5Mode(true);        
	}
})();