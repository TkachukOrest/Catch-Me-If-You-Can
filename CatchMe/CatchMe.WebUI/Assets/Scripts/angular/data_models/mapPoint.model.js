(function () {
    angular
        .module('catchMeApp')
        .factory('MapPoint', mapPointFactory);    

    function mapPointFactory() {
        function mapPoint(latitude, longitude, address) {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Address = address ? address : "";
            this.IsValid = false;

            this.isEqualTo = function(point) {
                return this.Address === point.Address &&
                    this.Latitude === point.Latitude &&
                    this.Longitude === point.Longitude;
            };

            this.isEmptyPoint = function() {
                return (!this.Address) ||
                (this.Latitude === 0) ||
                (this.Longitude === 0);
            };
        };        
        
        return mapPoint;
    }
})();