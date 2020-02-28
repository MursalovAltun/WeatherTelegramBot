using System;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;

namespace Common.Services
{
    public class SubscriberSettingsService : ISubscriberSettingsService
    {
        private readonly ISubscriberSettingsRepository _subscriberSettingsRepository;
        private readonly IMapper _mapper;

        public SubscriberSettingsService(ISubscriberSettingsRepository subscriberSettingsRepository,
                                         IMapper mapper)
        {
            this._subscriberSettingsRepository = subscriberSettingsRepository;
            this._mapper = mapper;
        }

        public async Task<SubscriberSettingsDTO> Get(Guid id)
        {
            var result = await this._subscriberSettingsRepository.Get(id);
            return this._mapper.Map<SubscriberSettingsDTO>(result);
        }

        public async Task<SubscriberSettingsDTO> GetBySubscriberId(Guid id)
        {
            var result = await this._subscriberSettingsRepository.GetBySubscriberId(id);
            return this._mapper.Map<SubscriberSettingsDTO>(result);
        }

        public async Task<SubscriberSettingsDTO> Edit(SubscriberSettingsDTO dto)
        {
            var subscriberSettings = this._mapper.Map<SubscriberSettings>(dto);
            var result = await this._subscriberSettingsRepository.Edit(subscriberSettings);
            return this._mapper.Map<SubscriberSettingsDTO>(result);
        }
    }
}