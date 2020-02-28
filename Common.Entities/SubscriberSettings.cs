using System;

namespace Common.Entities
{
    public class SubscriberSettings : BaseEntity
    {
        public bool IsReceiveDailyWeather { get; set; }

        public string MeasureSystem { get; set; }

        public Guid SubscriberId { get; set; }

        public virtual Subscriber Subscriber { get; set; }
    }
}