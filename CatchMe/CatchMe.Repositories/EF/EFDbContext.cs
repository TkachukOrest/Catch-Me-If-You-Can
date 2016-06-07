using System.Data.Entity;
using CatchMe.Domain.Entities;

namespace CatchMe.Repositories.EF
{
    public class EFDbContext: DbContext
    {
        public DbSet<TripEntity> Trips { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }        
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserProfileEntity> UserProfiles { get; set; }       
         
        public EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(null);
        }
    }
}
