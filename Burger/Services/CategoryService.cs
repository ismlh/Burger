
namespace Burger.Services
{
    public class CategoryService : Repository<Category>, ICategoryService
    {
        public CategoryService(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
