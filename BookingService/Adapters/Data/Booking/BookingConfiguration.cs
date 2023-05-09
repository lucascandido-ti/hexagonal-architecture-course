

using Entities = Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Booking
{
    public class BookingConfiguration : IEntityTypeConfiguration<Entities.Booking>
    {
        public void Configure(EntityTypeBuilder<Entities.Booking> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
