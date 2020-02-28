using System;
using System.Threading.Tasks;
using Common.DTO;

namespace Common.Services.Infrastructure.Services
{
    public interface ISubscriberSettingsService
    {
        Task<SubscriberSettingsDTO> Get(Guid id);

        Task<SubscriberSettingsDTO> GetBySubscriberId(Guid id);

        Task<SubscriberSettingsDTO> Edit(SubscriberSettingsDTO dto);
    }
}