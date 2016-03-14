/// <reference path="../../catchme.webui/assets/scripts/libs/jasmine/jasmine.js" />

describe("url.configs.test.js", function () {
    var urls;

    beforeEach(module('catchMeApp', ['ngRoute', 'ngAnimate', 'ngMaterial', 'ngMaterialDatePicker']));

    beforeEach(inject(function ($injector) {
        urls = $injector.get('urlConfigs');
    }));

    it("expected api/Trip/GetAllTrips url", function () {
        expect(urls.GetAllTrips).toBe('api/Trip/GetAllTrips');
    });
    it("expected api/Trip/GetTrip url", function () {
        expect(urls.GetTrip).toEqual('api/Trip/GetTrip');
    });
    it("expected api/Trip/AddTrip url", function () {
        expect(urls.AddTrip).toEqual('api/Trip/GetTrip');
    });    
});