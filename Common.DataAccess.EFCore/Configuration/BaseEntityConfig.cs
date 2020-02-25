using Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Common.DataAccess.EFCore.Configuration
{
    public abstract class BaseEntityConfig<TType> : IEntityTypeConfiguration<TType>
        where TType : BaseEntity
    {
        protected string TableName { get; set; }

        protected BaseEntityConfig(string tableName)
        {
            this.TableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<TType> builder)
        {
            builder.ToTable(this.TableName);
            builder.HasKey(obj => obj.Id);

            builder.Property(x => x.CreationDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(x => x.ModifyDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();
        }
    }
}