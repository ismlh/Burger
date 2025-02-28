

namespace Burger.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
        {
            
        }

        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ReservationConfigurations());
            builder.ApplyConfiguration(new MealConfigurations());

        }

        public virtual DbSet<Reservations> Reservations { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

    }
}
