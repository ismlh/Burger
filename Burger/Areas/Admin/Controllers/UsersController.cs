


namespace Burger.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]

    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterVM model, List<string> Roles)
        {
            if (ModelState.IsValid)
            {
                var isExistUser = await _userManager.FindByNameAsync(model.userName);
                if (isExistUser == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.userName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        FullName = model.FullName,
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        if (Roles.Count > 0)
                        {
                            foreach (var role in Roles)
                            {
                                await _userManager.AddToRoleAsync(user, role);

                            }
                        }
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Name Already Exist");
                }
            }
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();

            var User = await _userManager.FindByIdAsync(id);
            ViewBag.selectedRoles = await _userManager.GetRolesAsync(User);

            if (User != null)
            {
                var RegisterVm = new UpdateUserData
                {
                    userName = User.UserName,
                    FullName = User.FullName,
                    PhoneNumber = User.PhoneNumber,
                    Email = User.Email,
                };
                return View(RegisterVm);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserData model, List<string> Roles)
        {

            var isExistUser = await _userManager.FindByNameAsync(model.userName);

            var AllRoles = await _roleManager.Roles.Select(r=>new {r.Name}).ToListAsync();
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();

            ViewBag.selectedRoles = await _userManager.GetRolesAsync(isExistUser);




            if (ModelState.IsValid)
            {

                if (isExistUser==null|| isExistUser.UserName!=model.userName)
                {
                    ModelState.AddModelError("", "Please Dont Change UserName");
                    return View(model);
                }
                isExistUser.PhoneNumber = model.PhoneNumber;
                isExistUser.Email = model.Email;
                isExistUser.FullName = model.FullName;

                var result = await _userManager.UpdateAsync(isExistUser);
                if (result.Succeeded)
                {
                    
                 
                    if (Roles.Count > 0)
                    {
                        foreach (var role in AllRoles)
                        {
                            if(Roles.Contains(role.Name))
                                await _userManager.AddToRoleAsync(isExistUser, role.Name);
                            else
                                await _userManager.RemoveFromRoleAsync(isExistUser, role.Name);
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
            
        }


        public async Task <IActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                    await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
            
        }
    }
}
