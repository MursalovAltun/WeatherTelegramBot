using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Common.DataAccess.EFCore.Repositories
{
    public class SubscriberSettingsRepository : BaseRepository<SubscriberSettings, DataContext>, ISubscriberSettingsRepository
    {
        public SubscriberSettingsRepository(DataContext context) : base(context)
        {
        }

        public override async Task<SubscriberSettings> Get(Guid id)
        {
            return await this.dbContext.SubscriberSettings
                .Where(x => x.Id == id && !x.IsDelete)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<SubscriberSettings> GetBySubscriberId(Guid id)
        {
            return await this.dbContext.SubscriberSettings
                .Where(x => x.SubscriberId == id && !x.IsDelete)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public override async Task<bool> Exists(SubscriberSettings obj)
        {
            return await this.dbContext.SubscriberSettings
                .Where(x => x.Id == obj.Id)
                .AsNoTracking()
                .CountAsync() > 0;
        }
    }
}