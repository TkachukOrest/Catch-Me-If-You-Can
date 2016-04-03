(function () {
    angular
        .module('catchMeApp')
        .service('loadingDialogService', loadingDialogService);

    loadingDialogService.$inject = ['$mdDialog'];

    function loadingDialogService($mdDialog) {
        var service = {
            show: show,
            hide: hide            
        };

        return service;

        //functions
        function show() {            
            $mdDialog.show({                
                template: ' <div layout="row" layout-sm="column" layout-align="space-around"> ' +
                    '<md-progress-circular md-mode="indeterminate"></md-progress-circular>' +
                    '</div>',
                parent: angular.element(document.body),
                clickOutsideToClose: false,
                fullscreen: false
            });            
        }

        function hide(){
            $mdDialog.hide();
        }
    };
})();