(function () {
    angular
        .module('catchMeApp')
        .factory('addressDetailsFactory', addressDetailsFactory);

    function addressDetailsFactory() {
        //factory method
        function create(streetNumber, streetName, district, city, region, country) {
            return new AddressDetails(streetNumber, streetName, district, city, region, country);
        }

        return {
            create: create          
        };

        //model
        function AddressDetails(streetNumber, streetName, district, city, region, country) {
            this.StreetNumber = streetNumber;
            this.StreetName = streetName;
            this.District = district;
            this.City = city;
            this.Region = region;
            this.Country = country;                        
        };
    }
})();