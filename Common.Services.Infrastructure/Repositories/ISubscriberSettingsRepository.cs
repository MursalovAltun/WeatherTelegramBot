using System;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories
{
    public interface ISubscriberSettingsRepository
    {
        Task<SubscriberSettings> Get(Guid id);

        Task<SubscriberSettings> GetBySubscriberId(Guid id);

        Task<bool> Exists(SubscriberSettings obj);

        Task<SubscriberSettings> Edit(SubscriberSettings subscriberSettings);
    }
}