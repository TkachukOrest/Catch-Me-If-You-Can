using System.Collections.Generic;
using CatchMe.Domain.Entities;
using CatchMe.MapService;

namespace CatchMe.BL.Abstract
{
    public interface ITripManager
    {
        IEnumerable<TripEntity> GetAll();

        void AddTrip(TripEntity trip, StaticMapConfiguration staticMapConfiguration);
    }
}
