using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlantCareFramework.Areas.Identity.Data;
using PlantCareFramework.Areas.Identity.Pages.Account.Manage;
using PlantCareFramework.Data;
using System.Linq;
using PlantCareFramework.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PlantCareFramework.Controllers
{
    [Authorize (Roles = "Admin")]
    public class UsersController : Controller
    {
        //private ApplicationUser _appuser;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ApplicationDbContext context, 
                               UserManager<ApplicationUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
           
        }
        public IActionResult Index()
        {   
            ////var users = from user in _context.Users
                        
            return View(_context.Users.ToList());
        }
    
       
        [HttpGet]
        public async Task<IActionResult> UserRoles(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            UserRoleViewModel model = new UserRoleViewModel
            {
                UserId = user.Id,
                RoleNames = new List<string> {}
            };

            foreach(var role in _roleManager.Roles)
            {
                model.RoleNames.Add(role.Name);
            }

            
           
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            var userid = model.UserId;
            
            var roleid = model.RoleNames[0];


            if (!await _userManager.IsInRoleAsync(user, roleid))
            {
                _context.UserRoles.Add(new IdentityUserRole<string>
                {
                    UserId = userid,
                    RoleId = roleid.ToString()
                });

                _context.SaveChanges();
                TempData["message"]= user.UserName+" was assigned the "+roleid+" role";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["message"] = user.UserName + " was already assigned the " + roleid + " role";
                return RedirectToAction(nameof(Index));
            }

            
        }

            public async Task<IActionResult> Block(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Block")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user.IsActive == true)
            {
                user.IsActive = false;
            }
            else
            {
                user.IsActive = true;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
