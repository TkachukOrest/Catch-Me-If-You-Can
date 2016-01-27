$(function () {        
    var googleMapsService = new CatchMe.Services.GoogleMapsService('trip-map', { Latitude: 49.7946898, Longitude: 24.0647954});
    
    var startPoint = { Latitude: 49.7946898, Longitude: 24.0647954};
    var endPoint = { Latitude: 49.842582, Longitude: 24.003351};
    var wayPoints = [{ Latitude: 49.835327, Longitude: 24.0144097}];

    googleMapsService.displayRoute(startPoint, endPoint, wayPoints);
});