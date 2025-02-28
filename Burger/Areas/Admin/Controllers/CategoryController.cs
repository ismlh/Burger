

namespace Burger.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]

    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAll());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(string Name,int? id)
        {
            if (id != null)
            {
                var category = new Category { Name = Name,Id=(int)id };
                await _categoryService.Update(category);
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    var category = new Category { Name = Name };
                    await _categoryService.Insert(category);
                }
            }
           
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        { 
            if(id>0)
            {
                Category category =await _categoryService.GetById(id);
                if (category!=null)
                {
                    TempData["Name"] = category.Name;
                    TempData["ID"] = category.Id;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            if(id!=null&&id>0)
            {
                await _categoryService.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
