using AutoMapper;
using Common.DTO;
using Common.Entities;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class SubscriberSettingsProfile : Profile
    {
        public SubscriberSettingsProfile()
        {
            CreateMap<SubscriberSettings, SubscriberSettingsDTO>().ReverseMap();
        }
    }
}