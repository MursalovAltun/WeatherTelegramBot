using System;

namespace Common.DTO
{
    public class SubscriberSettingsDTO
    {
        public Guid Id { get; set; }

        public bool IsReceiveDailyWeather { get; set; }

        public Guid SubscriberId { get; set; }

        public SubscriberDTO Subscriber { get; set; }
    }
}