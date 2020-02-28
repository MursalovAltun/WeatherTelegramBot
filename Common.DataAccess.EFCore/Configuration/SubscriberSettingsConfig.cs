using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configuration
{
    public class SubscriberSettingsConfig : BaseEntityConfig<SubscriberSettings>
    {
        public SubscriberSettingsConfig() : base("SubscriberSettings") { }

        public override void Configure(EntityTypeBuilder<SubscriberSettings> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.IsReceiveDailyWeather)
                .IsRequired();

            builder.Property(x => x.SubscriberId)
                .IsRequired();

            builder.Property(x => x.MeasureSystem)
                .HasMaxLength(10)
                .HasDefaultValue("metric")
                .IsRequired();
        }
    }
}