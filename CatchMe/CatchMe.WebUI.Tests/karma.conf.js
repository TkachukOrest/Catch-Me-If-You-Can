// Karma configuration
// Generated on Fri Mar 04 2016 02:41:33 GMT+0200 (Финляндия (зима))

module.exports = function (config) {
    config.set({

        // base path that will be used to resolve all patterns (eg. files, exclude)
        basePath: '',


        // frameworks to use
        // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
        frameworks: ['jasmine'],


        // list of files / patterns to load in the browser
        files: [            
            '../catchme.webui/Assets/Scripts/libs/material/material.js',
            '../catchme.webui/Assets/Scripts/libs/iscroll/iscroll.js',
            '../catchme.webui/Assets/Scripts/libs/angular/angular.js',
            '../catchme.webui/Assets/Scripts/libs/angular/angular-animate.js',
            '../catchme.webui/Assets/Scripts/libs/angular/angular-aria.js',
            '../catchme.webui/Assets/Scripts/libs/angular/angular-messages.js',
            '../catchme.webui/Assets/Scripts/libs/angular/angular-material.js',
            '../catchme.webui/Assets/Scripts/libs/angular/angular-route.js',
            '../catchme.webui/Assets/Scripts/libs/angular/angular-mocks.js',
            '../catchme.webui/Assets/Scripts/libs/angular/angular-touch.js',
            '../catchme.webui/Assets/Scripts/libs/moment/moment.js',
            '../catchme.webui/Assets/Scripts/libs/material/material-datetimepicker.js',
            '../catchme.webui/Assets/Scripts/angular/catchMe.app.js',
            '../catchme.webui/Assets/Scripts/angular/catchMe.routes.js',
            '../catchme.webui/Assets/Scripts/angular/catchMe.constants.js',
            '../catchme.webui/Assets/Scripts/angular/catchMe.material.js',
            '../catchme.webui/Assets/Scripts/angular/services/trip.service.js',
            '../catchme.webui/Assets/Scripts/angular/services/googleMap.service.js',
            '../catchme.webui/Assets/Scripts/angular/controllers/tripList.controller.js',
            '../catchme.webui/Assets/Scripts/angular/controllers/tripAdd.controller.js',
            '../catchme.webui/Assets/Scripts/angular/controllers/partialDrawerMenu.controller.js',
            '../catchme.webui/Assets/Scripts/angular/components/hoverClass.directive.js',
            'services/*.js',
            'configs/*.js'
        ],


        // list of files to exclude
        exclude: [
        ],


        // preprocess matching files before serving them to the browser
        // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
        preprocessors: {

        },


        // test results reporter to use
        // possible values: 'dots', 'progress'
        // available reporters: https://npmjs.org/browse/keyword/karma-reporter
        reporters: ['progress'],


        // web server port
        port: 9876,


        // enable / disable colors in the output (reporters and logs)
        colors: true,


        // level of logging
        // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
        logLevel: config.LOG_INFO,


        // enable / disable watching file and executing tests whenever any file changes
        autoWatch: true,


        // start these browsers
        // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
        //browsers: ['Chrome', 'PhantomJS'],
        browsers: ['PhantomJS'],


        // Continuous Integration mode
        // if true, Karma captures browsers, runs the tests and exits
        singleRun: false,

        // Concurrency level
        // how many browser should be started simultaneous
        concurrency: Infinity
    });
}
