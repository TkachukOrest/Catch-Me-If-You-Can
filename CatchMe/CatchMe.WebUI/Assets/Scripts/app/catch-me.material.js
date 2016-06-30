(function () {
    angular
        .module('catchMeApp')
        .config(mdlThemeConfig);

    mdlThemeConfig.$inject = ['$mdThemingProvider'];

    function mdlThemeConfig($mdThemingProvider) {
        var customizedBlueGreyTheme = $mdThemingProvider.extendPalette('blue-grey', {
            '500': '86a8a8'
        });
        $mdThemingProvider.definePalette('customizedBlueGreyTheme', customizedBlueGreyTheme);

        $mdThemingProvider.theme('default')
          .primaryPalette('customizedBlueGreyTheme', {
              'default': '500',
              'hue-1': '100',
              'hue-2': '500',
              'hue-3': '800'
          })
          .accentPalette('grey');
    }
})();