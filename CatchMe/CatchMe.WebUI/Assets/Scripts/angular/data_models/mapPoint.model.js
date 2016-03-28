(function () {
    angular
        .module('catchMeApp')
        .factory('mapPointFactory', mapPointFactory);

    function mapPointFactory() {
        //factory method
        function create(latitude, longitude, address) {
            return new MapPoint(latitude, longitude, address);
        }

        return { create: create };

        //model
        function MapPoint(latitude, longitude, address) {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Address = address ? address : "";
            this.IsValid = false;

            this.isEqualTo = function (point) {
                return this.Address === point.Address &&
                    this.Latitude === point.Latitude &&
                    this.Longitude === point.Longitude;
            };

            this.isEmptyPoint = function () {
                return (!this.Address) ||
                (this.Latitude === 0) ||
                (this.Longitude === 0);
            };

            this.convertToGoogleMapPoint = function () {
                return { lat: this.Latitude, lng: this.Longitude };
            }
        };
    }
})();