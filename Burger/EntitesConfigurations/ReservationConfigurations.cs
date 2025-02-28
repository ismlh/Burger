

namespace Burger.EntitesConfigurations
{
    public class ReservationConfigurations : IEntityTypeConfiguration<Reservations>
    {
        public void Configure(EntityTypeBuilder<Reservations> builder)
        {
            builder.HasOne(r => r.User).WithMany(u => u.Reservations).HasForeignKey(r => r.UserId);
        }
    }
}
