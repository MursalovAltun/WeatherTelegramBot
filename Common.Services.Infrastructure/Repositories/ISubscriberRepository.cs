using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.Repositories
{
    public interface ISubscriberRepository
    {
        Task<Subscriber> Get(Guid id);

        Task<IEnumerable<Subscriber>> Get();

        Task<IEnumerable<Subscriber>> GetDailyReceivers();

        Task<bool> Exists(Subscriber obj);

        Task<Subscriber> Get(string username);

        Task<Subscriber> Edit(Subscriber subscriber);

        Task Delete(Guid id);

        Task Recover(Guid id);
    }
}