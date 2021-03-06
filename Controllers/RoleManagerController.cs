using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PlantCareFramework.Controllers
{
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles =  _roleManager.Roles.ToList();
            return View(roles);
        }
    }
}
