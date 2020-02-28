using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configuration
{
    public class SubscriberConfig : BaseEntityConfig<Subscriber>
    {
        public SubscriberConfig() : base("Subscribers") { }

        public override void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.TelegramUserId)
                .IsRequired();

            builder.Property(x => x.WaitingFor)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(x => x.ChatId)
                .IsRequired();

            builder.Property(x => x.City)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.HasOne(x => x.Settings)
                .WithOne(x => x.Subscriber)
                .HasForeignKey<SubscriberSettings>(x => x.SubscriberId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Property(x => x.Language)
                .HasMaxLength(2)
                .HasDefaultValue("en")
                .IsRequired();

            builder.Property(x => x.UtcOffset)
                .IsRequired(true);
        }
    }
}