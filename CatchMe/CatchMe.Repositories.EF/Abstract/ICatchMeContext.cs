using System.Data.Entity;
using CatchMe.Domain.Entities;
using CatchMe.Domain.Values;

namespace CatchMe.Repositories.EF.Abstract
{
    public interface ICatchMeContext
    {
        DbSet<Address> Addresses { get; set; }
        DbSet<MapPoint> MapPoints { get; set; }
        DbSet<PassengerEntity> Passengers { get; set; }
        DbSet<RoleEntity> Roles { get; set; }
        DbSet<TripEntity> Trips { get; set; }
        DbSet<UserEntity> Users { get; set; }
        DbSet<UserProfileEntity> UserProfiles { get; set; }
        DbSet<VehicleEntity> Vehicles { get; set; }

        void SaveChanges();
    }
}
