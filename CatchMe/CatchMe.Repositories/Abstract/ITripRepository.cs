using System.Collections.Generic;
using CatchMe.Domain.Entities;

namespace CatchMe.Repositories.Abstract
{
    public interface ITripRepository
    {
        bool Add(TripEntity trip);

        IEnumerable<TripEntity> GetAll();
    }
}
