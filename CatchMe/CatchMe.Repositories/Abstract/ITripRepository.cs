using System.Collections.Generic;
using CatchMe.Domain.Entities;

namespace CatchMe.Repositories.Abstract
{
    public interface ITripRepository
    {        
        TripEntity GetById(int id);

        IEnumerable<TripEntity> GetAll();

        void Add(TripEntity trip);

        void Delete(int id);
    }
}
