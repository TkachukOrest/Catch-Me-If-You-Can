using System;
using System.Collections.Generic;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;

namespace CatchMe.Repositories.Sql
{
    class TripRepository : Repository, ITripRepository
    {
        public bool Add(TripEntity trip)
        {
            throw new NotImplementedException();
        }        

        public IEnumerable<TripEntity> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
