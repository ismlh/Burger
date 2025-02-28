
namespace Burger.EntitesConfigurations
{
    public class MealConfigurations : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasOne(m => m.Category).WithMany(c => c.Meals).HasForeignKey(m => m.CategoryId);
        }
    }
}
