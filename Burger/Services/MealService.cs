





namespace Burger.Services
{
    public class MealService : Repository<Meal>, IMealService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string _ImagesPath;
        private readonly ApplicationDbContext _dbContext;
        public MealService(ApplicationDbContext dbContext, IWebHostEnvironment environment) : base(dbContext)
        {
            _environment = environment;
            _ImagesPath = $"{_environment.WebRootPath}{Utilites.ImagePath}";
            _dbContext = dbContext;
        }

        

        public async Task<string> GameCoverPath(IFormFile cover)
        {
            var coverName = cover.FileName;

            var path = Path.Combine(_ImagesPath, coverName);

            using var strem = File.Create(path);

            await cover.CopyToAsync(strem);


            return coverName;
        }

        public async Task<IEnumerable<Meal>> MealsWithItsCategory()
        {
            return await _dbContext.Meals.Include(m => m.Category).ToListAsync();
        }
    }
}
