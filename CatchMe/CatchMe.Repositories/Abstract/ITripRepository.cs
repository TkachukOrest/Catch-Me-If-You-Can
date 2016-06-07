using System.Collections.Generic;
using CatchMe.Domain.Entities;

namespace CatchMe.Repositories.Abstract
{
    public interface ITripRepository
    {        
        TripEntity GetById(int id);

        IEnumerable<TripEntity> GetAll();

        void Save(TripEntity trip);

        void Delete(int id);

        void AddPassenger(int tripId, int passengerId);
    }
}
