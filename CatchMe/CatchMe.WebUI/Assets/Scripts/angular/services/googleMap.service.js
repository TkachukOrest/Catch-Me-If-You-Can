﻿(function () {
    angular
        .module('catchMeApp')
        .service('googleMapService', googleMapService);
    
    function googleMapService() {
        //fields        
        var directionsService = new google.maps.DirectionsService;
        var directionsDisplay = new google.maps.DirectionsRenderer;
        var geocoder = new google.maps.Geocoder();
        var styles = [
            {
                "stylers": [{ "saturation": -100 }, { "gamma": 1 }]
            },
            {
                "elementType": "labels.text.stroke",
                "stylers": [{ "visibility": "off" }]
            }, {
                "featureType": "poi.business",
                "elementType": "labels.text",
                "stylers": [{ "visibility": "off" }]
            }, {
                "featureType": "poi.business",
                "elementType": "labels.icon",
                "stylers": [{ "visibility": "off" }]
            }, {
                "featureType": "poi.place_of_worship",
                "elementType": "labels.text",
                "stylers": [{ "visibility": "off" }]
            }, {
                "featureType": "poi.place_of_worship",
                "elementType": "labels.icon",
                "stylers": [{ "visibility": "off" }]
            }, {
                "featureType": "road",
                "elementType": "geometry",
                "stylers": [{ "visibility": "simplified" }]
            }, {
                "featureType": "water",
                "stylers": [{ "visibility": "on" }, { "saturation": 50 }, { "gamma": 0 }, { "hue": "#50a5d1" }]
            }, {
                "featureType": "administrative.neighborhood",
                "elementType": "labels.text.fill",
                "stylers": [{ "color": "#333333" }]
            }, {
                "featureType": "road.local",
                "elementType": "labels.text",
                "stylers": [{ "weight": 0.5 }, { "color": "#333333" }]
            }, {
                "featureType": "transit.station",
                "elementType": "labels.icon",
                "stylers": [{ "gamma": 1 }, { "saturation": 50 }]
            }
        ];

        var service = {
            createMap: createMap,
            displayRoute: displayRoute,
            decodeAddress: decodeAddress
        };

        return service;

        //functions       
        function createMap(elementId, initializationPoint) {
            var map = new google.maps.Map(document.getElementById(elementId), {
                zoom: 10,
                center: new google.maps.LatLng(parseFloat(initializationPoint.Latitude), parseFloat(initializationPoint.Longitude)),
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                panControl: false,
                mapTypeControl: false,
                zoomControl: true,
                zoomControlOptions: {
                    style: google.maps.ZoomControlStyle.SMALL
                },
                styles: styles
            });

            directionsDisplay.setMap(map);

            return map;
        }

        function displayRoute(map, startPoint, endPoint, points) {
            var wayPoints = convertWayPoints(points);

            directionsService.route({
                origin: new google.maps.LatLng(parseFloat(startPoint.Latitude), parseFloat(startPoint.Longitude)),
                destination: new google.maps.LatLng(parseFloat(endPoint.Latitude), parseFloat(endPoint.Longitude)),
                waypoints: wayPoints,
                optimizeWaypoints: true,
                travelMode: google.maps.TravelMode.DRIVING
            }, function (response, status) {
                if (status === google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setMap(map);
                    directionsDisplay.setDirections(response);
                }
            });
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

        function decodeAddress(address, successcb) {
            geocoder.geocode({ 'address': address }, function(results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    var point = { Latitude: results[0].geometry.location.lat(), Longitude: results[0].geometry.location.lng() }
                    successcb(point);                    
                }
            });
        }
    }
})();