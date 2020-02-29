using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO;
using Common.Entities;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;

namespace Common.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository _subscriberRepository;
        private readonly IMapper _mapper;

        public SubscriberService(ISubscriberRepository subscriberRepository,
                                 IMapper mapper)
        {
            this._subscriberRepository = subscriberRepository;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<SubscriberDTO>> GetAll()
        {
            var subscribers = await this._subscriberRepository.Get();
            return this._mapper.Map<IEnumerable<SubscriberDTO>>(subscribers);
        }

        public async Task<IEnumerable<SubscriberDTO>> GetDailyReceivers()
        {
            var subscribers = await this._subscriberRepository.GetDailyReceivers();
            return this._mapper.Map<IEnumerable<SubscriberDTO>>(subscribers);
        }

        public async Task<SubscriberDTO> GetById(Guid id)
        {
            var result = await this._subscriberRepository.Get(id);
            return this._mapper.Map<SubscriberDTO>(result);
        }

        public async Task<SubscriberDTO> GetByUsername(string username)
        {
            var result = await this._subscriberRepository.Get(username);
            return this._mapper.Map<SubscriberDTO>(result);
        }

        public async Task<SubscriberDTO> GetByTelegramUserId(int telegramUserId)
        {
            var result = await this._subscriberRepository.Get(telegramUserId);
            return this._mapper.Map<SubscriberDTO>(result);
        }

        public async Task<SubscriberDTO> Edit(SubscriberDTO dto)
        {
            var subscriber = this._mapper.Map<Subscriber>(dto);
            var result = await this._subscriberRepository.Edit(subscriber);
            return this._mapper.Map<SubscriberDTO>(result);
        }

        public async Task<bool> Delete(Guid id)
        {
            await this._subscriberRepository.Delete(id);
            return true;
        }

        public async Task<bool> Recover(Guid id)
        {
            await this._subscriberRepository.Recover(id);
            return true;
        }
    }
}