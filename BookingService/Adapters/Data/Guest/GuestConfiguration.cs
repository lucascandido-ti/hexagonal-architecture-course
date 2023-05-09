using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities = Domain.Entities;

namespace Data.Guest
{
    public class GuestConfiguration : IEntityTypeConfiguration<Entities.Guest>
    {
        public void Configure(EntityTypeBuilder<Entities.Guest> builder)
        {
            builder.HasKey(e => e.Id);
            builder.OwnsOne(e => e.DocumentId)
                   .Property(e => e.IdNumber);
            builder.OwnsOne(e => e.DocumentId)
                .Property(e => e.DocumentType);
        }
    }
}
