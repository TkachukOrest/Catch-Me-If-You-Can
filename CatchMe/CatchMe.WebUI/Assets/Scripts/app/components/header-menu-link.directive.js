(function () {
    angular
        .module('catchMeApp')
        .directive('headerMenuLink', headerMenuLink);

    function headerMenuLink() {
        var directive = {
            restrict: 'C',            
            link: link
        };

        return directive;

        function link(scope, element) {
            element.on('click', function () {                
                angular.element(document.querySelectorAll('.mdl-layout__drawer, .mdl-layout__obfuscator')).removeClass('is-visible');
            });            
        }
    };
})();