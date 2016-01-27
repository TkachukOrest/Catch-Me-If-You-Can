CatchMe.Services.GoogleMapsService = function (elementId, initializationPoint) {
    var self = this;
    self.displayRoute = displayRoute;

    //initialization
    initMap();
    initServices();

    //fields
    var map;
    var directionsService;
    var directionsDisplay;

    //methods
    function initMap() {
        map = new google.maps.Map(document.getElementById(elementId), {
            zoom: 5,
            center: new google.maps.LatLng(initializationPoint.Latitude, initializationPoint.Longitude),
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            panControl: false,
            mapTypeControl: false,
            zoomControl: true,
            zoomControlOptions: {
                style: google.maps.ZoomControlStyle.SMALL
            }
        });
    }

    function initServices() {
        directionsService = new google.maps.DirectionsService;
        directionsDisplay = new google.maps.DirectionsRenderer;

        directionsDisplay.setMap(map);
    }

    function convertWayPoints(points) {
        var waypoints = [];

        for (var i = 0; i < points.length; i++) {
            waypoints.push({
                location: new google.maps.LatLng(parseFloat(points[i].Latitude), parseFloat(points[i].Longitude)),
                stopover: true
            });
        };

        return waypoints;
    }

    function displayRoute(startPoint, endPoint, points) {
        var wayPoints = convertWayPoints(points);                     

        directionsService.route({
            origin: new google.maps.LatLng(parseFloat(startPoint.Latitude), parseFloat(startPoint.Longitude)),
            destination: new google.maps.LatLng(parseFloat(endPoint.Latitude), parseFloat(endPoint.Longitude)),
            waypoints: wayPoints,
            optimizeWaypoints: true,
            travelMode: google.maps.TravelMode.DRIVING
        }, function (response, status) {
            if (status === google.maps.DirectionsStatus.OK) {
                directionsDisplay.setDirections(response);
                //var route = response.routes[0];
                //var summaryPanel = document.getElementById('directions-panel');
                //summaryPanel.innerHTML = '';

                //for (var i = 0; i < route.legs.length; i++) {
                //    var routeSegment = i + 1;
                //    summaryPanel.innerHTML += '<b>Route Segment: ' + routeSegment +'</b><br>';
                //    summaryPanel.innerHTML += route.legs[i].start_address + ' to ';
                //    summaryPanel.innerHTML += route.legs[i].end_address + '<br>';
                //    summaryPanel.innerHTML += route.legs[i].distance.text + '<br><br>';
                //}
            }
        });
    }
};
