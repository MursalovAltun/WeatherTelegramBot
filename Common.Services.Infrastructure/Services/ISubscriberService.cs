using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTO;

namespace Common.Services.Infrastructure.Services
{
    public interface ISubscriberService
    {
        Task<IEnumerable<SubscriberDTO>> GetAll();

        Task<SubscriberDTO> GetById(Guid id);

        Task<SubscriberDTO> GetByUsername(string username);

        Task<SubscriberDTO> Edit(SubscriberDTO dto);

        Task<bool> Delete(Guid id);

        Task<bool> Recover(Guid id);
    }
}