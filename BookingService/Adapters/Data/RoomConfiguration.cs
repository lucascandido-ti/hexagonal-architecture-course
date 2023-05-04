using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Entities = Domain.Entities;

namespace Data
{
   
    public class RoomConfiguration : IEntityTypeConfiguration<Entities.Room>
    {
        public void Configure(EntityTypeBuilder<Entities.Room> builder)
        {
            builder.HasKey(e => e.Id);
            builder.OwnsOne(e => e.Price)
                   .Property(e => e.Currency);
            builder.OwnsOne(e => e.Price)
                .Property(e => e.Value);
        }
    }
}
