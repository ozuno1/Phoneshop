using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> UserManager;
        public RoleManager<IdentityRole> RoleManager;
        public IEnumerable<IdentityRole> Roles { get; set; }

        public AdminController(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            this.UserManager = userManager;
            this.RoleManager = roleManager;
        }
        public IActionResult Index()
        {
            var users = UserManager.Users.Select(user => new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = string.Join(",", UserManager.GetRolesAsync(user).Result.ToArray())
            }).ToList();
            return View(users);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user == null)
            {
                return BadRequest("User nuk gjendet");
            }
            var model = new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return BadRequest("User nuk gjendet" + model.Id);
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            IdentityUser user = await UserManager.FindByIdAsync(id);

            if (user != null)

            {
                await UserManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> Create(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest("User nuk gjendet");
            }
            var model = new UserViewModel
            {
                UserName = user.UserName
            };
            var roles = RoleManager.Roles;
            ViewBag.Roles = new SelectList(roles.ToList(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUserRole(UserViewModel u)
        {
            var name = Convert.ToString(await RoleManager.FindByIdAsync(u.Role));
            var user = await UserManager.FindByIdAsync(u.Id);
            if (user == null)
            {
                return BadRequest("User nuk ekziston" + name);
            }
            await UserManager.AddToRoleAsync(user, name);
            return RedirectToAction("Index");
        }

    }
}

