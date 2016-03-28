using System;
using System.Collections.Generic;
using System.Linq;
using CatchMe.Domain.Entities;
using CatchMe.Domain.Values;
using CatchMe.Repositories.Abstract;

namespace CatchMe.Repositories.Static
{
    public class StaticTripRepository : ITripRepository
    {
        private static List<TripEntity> _trips = new List<TripEntity>()
        {
            new TripEntity()
            {
                Id = 1,
                Price = 100,
                Seats = 5,
                Origin = new MapPoint(49.7946898, 24.0647954),
                Destination = new MapPoint(49.842582, 24.003351),
                WayPoints = new List<MapPoint>() {new MapPoint(49.835327, 24.0144097)},
                StartDateTime = DateTime.Today,
                Vehicle = new VehicleEntity()
                {
                    Color = "red",
                    Manufacturer = "Audi",
                    Model = "a6",
                    Year = 2015
                },
                StaticMapUrl = @"https://maps.googleapis.com/maps/api/staticmap?center=49,8399150068689,24,0314&zoom=16&size=640x640&maptype=roadmap&markers=color:blue%7C|weight:5%7C|49,840034,24,0336112|49,8408445,24,0289195&style=feature%3Aall%7Celement%3Aall%7Csaturation%3A-100%7Cgamma%3A1%7C&feature%3Aall%7Celement%3Alabels.text.stroke%7Cvisibility%3Aoff%7C&feature%3Apoi.business%7Celement%3Alabels.text%7Cvisibility%3Aoff%7C&feature%3Apoi.business%7Celement%3Alabels.icon%7Cvisibility%3Aoff%7C&feature%3Apoi.place_of_worship%7Celement%3Alabels.text%7Cvisibility%3Aoff%7C&feature%3Apoi.place_of_worship%7Celement%3Alabels.icon%7Cvisibility%3Aoff%7C&feature%3Aroad%7Celement%3Ageometry%7Cvisibility%3Asimplified%7C&feature%3Awater%7Celement%3Aall%7Cvisibility%3Aon%7Csaturation%3A50%7Cgamma%3A0%7Chue%3A0x50a5d1%7C&feature%3Aadministrative.neighborhood%7Celement%3Alabels.text.fill%7Ccolor%3A0x333333%7C&feature%3Aroad.local%7Celement%3Alabels.text%7Cweight%3A0.5%7Ccolor%3A0x333333%7C&feature%3Atransit.station%7Celement%3Alabels.icon%7Cgamma%3A1%7Csaturation%3A50%7C&path=weight:3%7Ccolor:blue%7Cenc:ekeoHaauqClBcAZIf@G]lCEz@BhEf@dFCTG\OR_@L]JUfBc@~AKRy@n@aBv@Ik@"
            },
            new TripEntity()
            {
                Id = 2,
                Price = 22,
                Seats = 5,
                Origin = new MapPoint(49.7946898, 24.0647954),
                Destination = new MapPoint(49.842582, 24.003351),
                WayPoints = new List<MapPoint>() {new MapPoint(49.835327, 24.0144097)},
                StartDateTime = DateTime.Today,
                Vehicle = new VehicleEntity()
                {
                    Color = "red",
                    Manufacturer = "Audi",
                    Model = "a6",
                    Year = 2015
                },
                StaticMapUrl = @"https://maps.googleapis.com/maps/api/staticmap?center=49,8404729109946,23,9981783544922&zoom=13&size=640x640&maptype=roadmap&markers=color:blue%7C|weight:5%7C|49,8307965,23,9690313999999|49,843072,24,028102&style=feature%3Aall%7Celement%3Aall%7Csaturation%3A-100%7Cgamma%3A1%7C&feature%3Aall%7Celement%3Alabels.text.stroke%7Cvisibility%3Aoff%7C&feature%3Apoi.business%7Celement%3Alabels.text%7Cvisibility%3Aoff%7C&feature%3Apoi.business%7Celement%3Alabels.icon%7Cvisibility%3Aoff%7C&feature%3Apoi.place_of_worship%7Celement%3Alabels.text%7Cvisibility%3Aoff%7C&feature%3Apoi.place_of_worship%7Celement%3Alabels.icon%7Cvisibility%3Aoff%7C&feature%3Aroad%7Celement%3Ageometry%7Cvisibility%3Asimplified%7C&feature%3Awater%7Celement%3Aall%7Cvisibility%3Aon%7Csaturation%3A50%7Cgamma%3A0%7Chue%3A0x50a5d1%7C&feature%3Aadministrative.neighborhood%7Celement%3Alabels.text.fill%7Ccolor%3A0x333333%7C&feature%3Aroad.local%7Celement%3Alabels.text%7Cweight%3A0.5%7Ccolor%3A0x333333%7C&feature%3Atransit.station%7Celement%3Alabels.icon%7Cgamma%3A1%7Csaturation%3A50%7C&path=weight:3%7Ccolor:blue%7Cenc:oqcoHmmhqCiBiRcAcKwBgSWgD_AyIy@cIuA}MmBeRw@eIgEmb@oAgMwAsMwBsTYgB}@mEiAuFkA}E{AiFmCeJ}@iDkAwDkEiOuFmR{AmE]qAOg@gAyNUiFWgDUeAmBcEg@kAIUQy@[sBo@wEIqA?{@AkEpD?RMzCw@PdB"
            }
        };

        public IEnumerable<TripEntity> GetAll()
        {
            return _trips;
        }

        public void Add(TripEntity trip)
        {
            _trips.Add(trip);            
        }

        public TripEntity GetById(int id)
        {
            var trip = _trips.First(x => x.Id == id);

            return trip;
        }

        public void Delete(int id)
        {
            var tripToRemoveIndex = _trips.FindIndex(x => x.Id == id);

            _trips.RemoveAt(tripToRemoveIndex);
        }
    }
}
