namespace Burger.Services
{
    public interface IMealService:IRepository<Meal>
    {
        Task<string> GameCoverPath(IFormFile cover);
        Task<IEnumerable<Meal>> MealsWithItsCategory();
    }
}
