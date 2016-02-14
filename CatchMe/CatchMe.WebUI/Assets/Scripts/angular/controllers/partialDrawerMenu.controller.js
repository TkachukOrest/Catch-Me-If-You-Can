(function () {
    angular
        .module('catchMeApp')
        .controller('PartialDrawerMenuController', PartialDrawerMenuController);

    function PartialDrawerMenuController() {
        var partialDrawerVm = this;

        partialDrawerVm.linkBlocks = [
            [
                { url: '/Trips', displayName: 'Trips' },
                { url: '/Drivers', displayName: 'Drivers' },
                { url: '/Passengers', displayName: 'Passengers' }
            ], [
                { url: '/About', displayName: 'About' },
                { url: '/Contacts', displayName: 'Contacts' }
            ]
        ];

        partialDrawerVm.linkClick = function () {
            angular.element(document.querySelectorAll('.mdl-layout__drawer, .mdl-layout__obfuscator')).removeClass('is-visible');
        }
    };
})();