using System;
using System.Collections.Generic;
using CatchMe.Entities;
using CatchMe.Repositories.Abstract;

namespace CatchMe.Repositories.Sql
{
    class TripRepository : Repository, ITripRepository
    {
        public bool Add(Trip trip)
        {
            throw new NotImplementedException();
        }        

        public IEnumerable<Trip> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
