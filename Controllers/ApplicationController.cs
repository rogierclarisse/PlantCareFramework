using PlantCareFramework.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using PlantCareFramework.Areas.Identity.Data;
using PlantCareFramework.Data;
using PlantCareFramework.Services;

namespace GroupSpace.Controllers
{
    public class ApplicationController : Controller
    {
        protected readonly ApplicationUser _user;
        protected readonly ApplicationDbContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ILogger<ApplicationController> _logger;
        protected ApplicationController(ApplicationDbContext context,
                                        IHttpContextAccessor httpContextAccessor,
                                        ILogger<ApplicationController> logger)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

            
            
        }
    }
}
