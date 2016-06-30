using System.Data.Entity;
using CatchMe.Domain.Values;
using CatchMe.Domain.Entities;
using CatchMe.Repositories.Abstract;
using CatchMe.Repositories.EF.Abstract;

namespace CatchMe.Repositories.EF.Concrete
{
    public class CatchMeContext : DbContext, ICatchMeContext
    {
        public CatchMeContext(IRepositorySettings repositorySettings) : base(repositorySettings.ConnectionString)
        {
            Database.SetInitializer(new NullDatabaseInitializer<CatchMeContext>());
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<MapPoint> MapPoints { get; set; }
        public virtual DbSet<PassengerEntity> Passengers { get; set; }
        public virtual DbSet<RoleEntity> Roles { get; set; }        
        public virtual DbSet<TripEntity> Trips { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<UserProfileEntity> UserProfiles { get; set; }
        public virtual DbSet<VehicleEntity> Vehicles { get; set; }

        void ICatchMeContext.SaveChanges()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MapPoint>()
                .HasMany(e => e.Addresses)
                .WithRequired(e => e.MapPoint)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<RoleEntity>()
                .HasMany(e => e.User)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UserRole").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<TripEntity>()
                .HasMany(t => t.MapPoints)
                .WithRequired()
                .HasForeignKey(mp => mp.TripId);

            modelBuilder.Entity<TripEntity>()
                .HasMany(e => e.Passengers)
                .WithRequired(p => p.Trip)
                .WillCascadeOnDelete(true);            
            
            modelBuilder.Entity<UserEntity>()
                .HasMany(e => e.UserProfiles)
                .WithRequired(e => e.User)
                .HasForeignKey(e=>e.UserId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<VehicleEntity>()
                .HasMany(e => e.Trip)
                .WithRequired(e => e.Vehicle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TripEntity>()
                .Property(e => e.Price)
                .HasPrecision(12, 4);
        }
    }
}
