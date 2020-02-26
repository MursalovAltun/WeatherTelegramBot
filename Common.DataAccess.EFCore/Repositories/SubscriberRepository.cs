using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class SubscriberRepository : BaseRepository<Subscriber, DataContext>, ISubscriberRepository
    {
        public SubscriberRepository(DataContext context) : base(context) { }

        public override async Task<Subscriber> Get(Guid id)
        {
            return await this.dbContext.Subscribers.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Subscriber>> Get()
        {
            return await this.dbContext.Subscribers.Where(x => !x.IsDelete).AsNoTracking().ToListAsync();
        }

        public override async Task<bool> Exists(Subscriber obj)
        {
            return await this.dbContext.Subscribers.Where(x => x.Id == obj.Id).AsNoTracking().CountAsync() > 0;
        }

        public async Task<Subscriber> Get(string username)
        {
            return await this.dbContext.Subscribers.Where(x => x.Username == username).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}