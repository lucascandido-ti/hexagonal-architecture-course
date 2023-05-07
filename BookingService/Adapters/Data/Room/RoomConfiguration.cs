using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.Room
{

    public class RoomConfiguration : IEntityTypeConfiguration<Domain.Room.Entities.Room>
    {
        public void Configure(EntityTypeBuilder<Domain.Room.Entities.Room> builder)
        {
            builder.HasKey(e => e.Id);
            builder.OwnsOne(e => e.Price)
                   .Property(e => e.Currency);
            builder.OwnsOne(e => e.Price)
                .Property(e => e.Value);
        }
    }
}
