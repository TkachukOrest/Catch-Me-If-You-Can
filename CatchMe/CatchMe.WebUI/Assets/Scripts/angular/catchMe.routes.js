(function () {
	angular
        .module('catchMeApp')
        .config(config);

    config.$inject = ['$routeProvider', '$locationProvider'];

	function config($routeProvider, $locationProvider) {	    
		$routeProvider.when('/', {
			templateUrl: '/Assets/Scripts/angular/templates/tripList.html'			
		});

		$routeProvider.when('/TripAdd', {
		    templateUrl: '/Assets/Scripts/angular/templates/trip.html'		    
		});

		$routeProvider.when('/TripEdit/:tripId', {
		    templateUrl: '/Assets/Scripts/angular/templates/trip.html'
		});

		$routeProvider.when('/Trips', { redirectTo: '/' });

		$routeProvider.when('/SignIn', {
		    templateUrl: '/Assets/Scripts/angular/templates/signIn.html'
		});

		$routeProvider.when('/SignUp', {
		    templateUrl: '/Assets/Scripts/angular/templates/signUp.html'
		});

		$routeProvider.otherwise({ redirectTo: '/' });

		$locationProvider.html5Mode(true);        
	}
})();