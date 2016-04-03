(function () {
    angular
        .module('catchMeApp')
        .service('googleMapService', googleMapService);

    googleMapService.$inject = ['$q', 'googleMapConfigs', 'mapPointFactory'];

    function googleMapService($q, googleMapConfigs, mapPointFactory) {
        //fields        
        var directionsService = new google.maps.DirectionsService;
        var directionsDisplay = new google.maps.DirectionsRenderer;
        var geocoder = new google.maps.Geocoder();
        var styles = googleMapConfigs.styles;

        //service
        var service = {
            createMap: createMap,
            displayRoute: displayRoute,
            deocodeAddress: deocodeAddress,
            initAutocomplete: initAutocomplete,
            isWayDirectionValid: isWayDirectionValid,
            getCurrentPosition: getCurrentPosition,
            createStaticMapConfiguration: createStaticMapConfiguration,
            clearMap: clearMap,
            getCenter: getCenter,
            getZoom: getZoom
        };

        return service;

        //public functions               
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
            var deferred = $q.defer();
            var wayPoints = convertToWayPoints(points);

            directionsService.route({
                origin: new google.maps.LatLng(parseFloat(startPoint.Latitude), parseFloat(startPoint.Longitude)),
                destination: new google.maps.LatLng(parseFloat(endPoint.Latitude), parseFloat(endPoint.Longitude)),
                waypoints: wayPoints,
                travelMode: google.maps.TravelMode.DRIVING
            }, function (response, status) {
                if (status === google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setMap(map);
                    directionsDisplay.setDirections(response);
                    deferred.resolve(response.routes[0].overview_polyline);
                }
            }, function () {
                deferred.reject();
            });

            return deferred.promise;
        }

        function deocodeAddress(address) {
            var deferred = $q.defer();

            geocoder.geocode({ 'address': address }, function (results, status) {
                if (status === google.maps.GeocoderStatus.OK) {
                    var point = { Latitude: results[0].geometry.location.lat(), Longitude: results[0].geometry.location.lng() }

                    deferred.resolve(point);
                } else {
                    deferred.reject(status);
                }
            });

            return deferred.promise;
        }

        function initAutocomplete(elementId, onPlaceChanged) {
            var initializedAutocomplete = new google.maps.places.Autocomplete((document.getElementById(elementId)), { types: ['geocode'] });

            if (onPlaceChanged) {
                initializedAutocomplete.addListener('place_changed', onPlaceChanged);
            }

            return initializedAutocomplete;
        }

        function isWayDirectionValid(startPoint, endPoint, points) {
            var deferred = $q.defer();

            var wayPoints = convertToWayPoints(points);

            directionsService.route({
                origin: new google.maps.LatLng(parseFloat(startPoint.Latitude), parseFloat(startPoint.Longitude)),
                destination: new google.maps.LatLng(parseFloat(endPoint.Latitude), parseFloat(endPoint.Longitude)),
                waypoints: wayPoints,
                travelMode: google.maps.TravelMode.DRIVING
            }, function (response, status) {
                if (status === google.maps.DirectionsStatus.OK) {
                    deferred.resolve();
                } else {
                    deferred.reject(status);
                }
            }, function () { deferred.reject(status); });

            return deferred.promise;
        }

        function getCurrentPosition() {
            var deferred = $q.defer();

            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var currentPosition = mapPointFactory.create(position.coords.latitude, position.coords.longitude);
                    deferred.resolve(currentPosition);
                }, function () {
                    deferred.reject();
                });
            } else {
                deferred.reject();
            }

            return deferred.promise;
        }

        function createStaticMapConfiguration(googleMap, pathPoints, center, zoom) {            
            return {
                Center: center,
                Zoom: zoom,
                MapType: "roadmap",
                StyleRules: getStaticStyle(styles),
                Path: pathPoints
            };
        }

        function clearMap(googleMap) {
            directionsDisplay.suppressMarkers = true;
            directionsDisplay.setMap(googleMap);
            directionsDisplay.setDirections({ routes: [] });
            directionsDisplay.setMap();
        }

        //private helpers
        function convertToWayPoints(points) {
            var waypoints = [];

            for (var i = 0; i < points.length; i++) {
                waypoints.push({
                    location: new google.maps.LatLng(parseFloat(points[i].Latitude), parseFloat(points[i].Longitude)),
                    stopover: true
                });
            };

            return waypoints;
        }

        function getStaticStyle(styles) {
            var result = [];
            styles.forEach(function (elem) {
                var style = '';
                if (elem.stylers) {
                    if (elem.stylers.length > 0) {
                        style += (elem.hasOwnProperty('featureType') ? 'feature:' + elem.featureType : 'feature:all') + '|';
                        style += (elem.hasOwnProperty('elementType') ? 'element:' + elem.elementType : 'element:all') + '|';
                        elem.stylers.forEach(function (val) {
                            var propertyname = Object.keys(val)[0];
                            var propertyval = val[propertyname].toString().replace('#', '0x');
                            style += propertyname + ':' + propertyval + '|';
                        });
                    }
                }
                result.push(encodeURIComponent(style));
            });

            return result.join('&');
        }

        function getCenter(googleMap) {
            var center = googleMap.getCenter();

            return mapPointFactory.create(center.lat(), center.lng());
        }

        function getZoom(googleMap) {
            return googleMap.getZoom();
        }
    }
})();
