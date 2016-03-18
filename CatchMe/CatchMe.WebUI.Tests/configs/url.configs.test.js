/// <reference path="../../catchme.webui/assets/scripts/libs/jasmine/jasmine.js" />

describe("url.configs.test.js", function () {
    var urls;

    beforeEach(module('catchMeApp'));

    beforeEach(inject(function ($injector) {
        urls = $injector.get('urlConfigs');
    }));

    it("expected api/Trip/GetAllTrips url", function () {
        expect(urls.GetAllTrips).toBe('api/Trip/GetAllTrips');
    });
    it("expected api/Trip/GetTrip url", function () {
        expect(urls.GetTrip).toBe('api/Trip/GetTrip');
    });
    it("expected api/Trip/AddTrip url", function () {
        expect(urls.AddTrip).toBe('api/Trip/AddTrip');
    });    
});