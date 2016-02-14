using System.Collections.Generic;
using System.Linq;
using CatchMe.Entities;

namespace CatchMe.Repositories.Abstract
{
    public interface ITripRepository
    {
        bool Add(Trip trip);

        IEnumerable<Trip> GetAll();
    }
}
