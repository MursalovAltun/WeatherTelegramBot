using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<SubscriberSettings> SubscriberSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DataContext)));
        }

        // Overriding to set creation & modify date since MySql is not support ValueGeneratedOnAdd/Update();
        // see https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/issues/687
        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).CreationDate = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).ModifyDate = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        // Overriding to set creation & modify date since MySql is not support ValueGeneratedOnAdd/Update();
        // see https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/issues/687
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).CreationDate = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).ModifyDate = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}