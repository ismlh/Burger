


namespace Burger.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]

    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class MealsController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMealService _mealService;

        public MealsController(ICategoryService categoryService,IMealService mealService)
        {
            this._categoryService = categoryService;
            this._mealService = mealService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _mealService.MealsWithItsCategory());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories =new SelectList( await _categoryService.GetAll(),"Id","Name");
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MealVM model)
        {


            if (ModelState.IsValid)
            {
                string ImagePath = await _mealService.GameCoverPath(model.Image);
                var meal = new Meal
                {
                    Name = model.Name,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    Price = model.Price,
                    Image=ImagePath
                    
                };
                await _mealService.Insert(meal);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(await _categoryService.GetAll(), "Id", "Name");

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAll(), "Id", "Name");

            Meal meal =await _mealService.GetById(id);
            if (meal == null) return RedirectToAction(nameof(Index));
            TempData["ID"] = meal.Id;

            MealVM model = new MealVM();
            if (meal != null)
            {

                 model = new MealVM
                {
                    Name = meal.Name,
                    CategoryId = meal.CategoryId,
                    Description = meal.Description,
                    Price = meal.Price,
                };
                TempData["Image"] = meal.Image;
                ViewBag.Image=meal.Image;

            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MealVM model, int? id)
        {


            if (ModelState.IsValid)
            {
                var meal = new Meal
                {
                    Id=(int)id,
                    Name = model.Name,
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    Price = model.Price,
                    
                };
                if (model.Image!=null)
                {
                    string ImagePath = await _mealService.GameCoverPath(model.Image);
                    meal.Image = ImagePath;
                }
                else
                {
                    var Image = TempData["Image"] as string;
                    meal.Image = Image;
                }


                    await _mealService.Update(meal);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(await _categoryService.GetAll(), "Id", "Name");

            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _mealService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
