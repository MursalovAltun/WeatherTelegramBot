using Common.Entities;
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
                .IsRequired();

            builder.Property(x => x.Username)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.Property(x => x.WaitingFor)
                .HasMaxLength(50)
                .IsRequired(false);
        }
    }
}