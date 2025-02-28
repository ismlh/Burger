using System.Diagnostics;
using System.Security.Claims;
using Burger.Models;
using Microsoft.AspNetCore.Authorization;

namespace Burger.Controllers;

public class HomeController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IMealService _mealService;
    private readonly IReservationsService _reservations;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(ICategoryService categoryService,
                         IMealService mealService,
                         IReservationsService reservations,
                         UserManager<ApplicationUser> userManager)
    {
        this._categoryService = categoryService;
        this._mealService = mealService;
        this._reservations = reservations;
        this._userManager = userManager;
    }

    public async Task<IActionResult> Index(int? id)
    {
        var userId = _userManager.GetUserId(HttpContext.User);

        // Fetch the user's data from the database
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            ViewBag.FullName = user.FullName;
            ViewBag.PhoneNumber = user.PhoneNumber;
            ViewBag.UserName = user.UserName;
            ViewBag.Email = user.Email;
            ViewBag.Id = user.Id;
        }

        HomeVM homeVM = new HomeVM();
        ViewBag.Users = new SelectList( await _userManager.Users.ToListAsync(),"Id", "FullName");

        homeVM.Categories = (ICollection<Category>)await _categoryService.GetAll();
        if (id != null && id != 0)
        {
            ViewBag.activeCategory = id;
            homeVM.Meals = (ICollection<Meal>)await _mealService.Filter(m=>m.CategoryId==id);
        }
        else
        {
            homeVM.Meals = (ICollection<Meal>)await _mealService.GetAll();
        }

        
        if (userId == null)
        {
            return View(homeVM);
        }
        else
        {

            ApplicationUser applicationUser = await _userManager.FindByIdAsync(userId);
            homeVM.ReservationViewModel = new ReservationViewModel
            {
                FullName = applicationUser.FullName,
                date = new DateTime(),
                Email = applicationUser.Email,
                PhoneNumber = applicationUser.PhoneNumber,
                NumOfpersons = 0
            };
            return View(homeVM);
        }
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(ReservationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var reservation = new Reservations
            {
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                NumOfpersons = model.NumOfpersons,
                date = model.date,
                UserId = model.UserId
            };
            try
            {
                await _reservations.Insert(reservation);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
        ViewBag.Users = new SelectList(await _userManager.Users.ToListAsync(), "Id", "UserName");
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
