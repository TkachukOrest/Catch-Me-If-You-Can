using System;
using System.Collections.Generic;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;

namespace CatchMe.Repositories.Sql
{
    public class TripRepository : Repository, ITripRepository
    {
        public TripRepository(IRepositorySettings repositorySettings) : base(repositorySettings) { }

        public IEnumerable<TripEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save(TripEntity trip)
        {
            throw new NotImplementedException();
        }

        public TripEntity GetById(int id)
        {
            throw new NotImplementedException();
        }       

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }     
    }
}
