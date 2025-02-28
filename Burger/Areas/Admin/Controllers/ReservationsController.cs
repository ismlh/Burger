



namespace Burger.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]

    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class ReservationsController : Controller
    {
        private readonly IReservationsService _reservations;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationsController(IReservationsService reservations,
                                     UserManager<ApplicationUser> userManager)
        {
            this._reservations = reservations;
            this._userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _reservations.ReservationWithUser());
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Create()
        {
            ViewBag.Users = new SelectList( await _userManager.Users.ToListAsync(),"Id", "UserName");
            return View();
        }
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
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Users = new SelectList(await _userManager.Users.ToListAsync(), "Id", "UserName");
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _reservations.GetById(id);
            if (reservation == null) return RedirectToAction(nameof(Index));
            TempData["ID"] = reservation.Id;

            ViewBag.Users = new SelectList(await _userManager.Users.ToListAsync(), "Id", "UserName");
            return View(new ReservationViewModel() 
                { 
                    FullName=reservation.FullName,
                    Email=reservation.Email,
                    PhoneNumber=reservation.PhoneNumber,
                    date=reservation.date,
                    UserId=reservation.UserId,
                    NumOfpersons=reservation.NumOfpersons
                }
            );
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ReservationViewModel model)
        {
            if (ModelState.IsValid)
            {
                int id =(int) TempData["ID"];

                var reservation = new Reservations
                {
                    Id =id,
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    NumOfpersons = model.NumOfpersons,
                    date = model.date,
                    UserId = model.UserId
                };
                try
                {
                    await _reservations.Update(reservation);
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

        public async Task<IActionResult> Delete(int id)
        {
            var resrevation = await _reservations.GetById(id);
            if(resrevation!=null)
            {
                try
                {
                    await _reservations.Delete(id);
                }
                catch(Exception ex)
                {
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
