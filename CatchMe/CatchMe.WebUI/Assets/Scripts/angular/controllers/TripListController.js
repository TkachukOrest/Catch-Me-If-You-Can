(function () {
    angular
        .module('catchMeApp')
        .controller('TripListController', TripListController);

    function TripListController() {
        var tripListVm = this;

        tripListVm.name = 'angularApp';
    };
})();