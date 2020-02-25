﻿using AutoMapper;
using Common.DTO;
using Common.Entities;
using Telegram.Bot.Types;

namespace Common.Services.Infrastructure.MappingProfiles
{
    public class SubscriberProfile : Profile
    {
        public SubscriberProfile()
        {
            CreateMap<Subscriber, SubscriberDTO>().ReverseMap();
            CreateMap<User, SubscriberDTO>().ReverseMap();
        }
    }
}