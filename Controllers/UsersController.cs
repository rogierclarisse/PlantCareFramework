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
    [Authorize]
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
    
       

        public IActionResult UserRoles()
        {
            
           
            return View();
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
