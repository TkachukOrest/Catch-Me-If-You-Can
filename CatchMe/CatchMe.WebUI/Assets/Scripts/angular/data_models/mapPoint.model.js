(function () {
    angular
        .module('catchMeApp')
        .factory('mapPointFactory', mapPointFactory);

    function mapPointFactory() {
        //factory method
        function create(latitude, longitude, address, shortAddress, addressDetails) {
            return new MapPoint(latitude, longitude, address, shortAddress, addressDetails);
        }        

        return { create: create };

        //model
        function MapPoint(latitude, longitude, address, shortAddress, addressDetails) {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.FormattedLongAddress = address ? address : "";
            this.FormattedShortAddress = shortAddress ? shortAddress : "";
            this.AddressDetails = addressDetails ? addressDetails : {};
            this.IsValid = false;

            this.isEqualTo = function (point) {
                return this.FormattedLongAddress === point.FormattedLongAddress &&
                       this.Latitude === point.Latitude &&
                       this.Longitude === point.Longitude;
            };

            this.isEmptyPoint = function () {
                return (!this.FormattedLongAddress) ||
                (this.Latitude === 0) ||
                (this.Longitude === 0);
            };

            this.convertToGoogleMapPoint = function () {
                return { lat: this.Latitude, lng: this.Longitude };
            }
        };
    }
})();