using System;
using System.Threading.Tasks;
using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public abstract class BaseRepository<TType, TContext>
        where TType : BaseEntity, new()
        where TContext : DataContext
    {
        protected TContext dbContext;

        protected BaseRepository(TContext context)
        {
            this.dbContext = context;
        }

        protected TContext GetContext()
        {
            return dbContext;
        }

        public abstract Task<TType> Get(Guid id);
        public abstract Task<bool> Exists(TType obj);

        public virtual async Task<TType> Edit(TType obj)
        {
            var objectExists = await Exists(obj);
            var context = GetContext();

            context.Entry(obj).State = objectExists ? EntityState.Modified : EntityState.Added;
            await context.SaveChangesAsync();
            return obj;

        }

        public virtual async Task Delete(Guid id)
        {
            var context = GetContext();

            var itemToDelete = await Get(id);
            itemToDelete.IsDelete = true;
            context.Entry(itemToDelete).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public virtual async Task Recover(Guid id)
        {
            var context = GetContext();

            var itemToRecover = await Get(id);
            itemToRecover.IsDelete = false;
            context.Entry(itemToRecover).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}