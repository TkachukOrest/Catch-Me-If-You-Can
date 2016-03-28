using System;
using System.Collections.Generic;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;

namespace CatchMe.Repositories.Sql
{
    class TripRepository : Repository, ITripRepository
    {        
        public IEnumerable<TripEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TripEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(TripEntity trip)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
