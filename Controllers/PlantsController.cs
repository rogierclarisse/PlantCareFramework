#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlantCareFramework.Areas.Identity.Data;
using PlantCareFramework.Data;
using PlantCareFramework.Models;

namespace PlantCareFramework.Controllers
{
    [Authorize]
    public class PlantsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public PlantsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }



        // GET: Plants
        public async Task<IActionResult> Index()
        {
            var _user = _context.Users.FirstOrDefault(e => e.UserName == User.Identity.Name);
            var plantCareContext = _context.Plant.Include(p => p.Light).Include(p => p.Place);

            if (_user == null)
            {
                plantCareContext = _context.Plant.Include(p => p.Light).Include(p => p.Place);
            }
            else
            {
                plantCareContext = _context.Plant.Where(p => p.AppUserId == _user.Id).Include(p => p.Light).Include(p => p.Place);
            }

            


            return View(await plantCareContext.ToListAsync());
        }

        public async Task<IActionResult> Task()
        {
   
            var plants = _context.Plant.Where(t => t.CreationDate < DateTime.Now).Include(p => p.Light).Include(p => p.Place);

            foreach(var plant in plants)
            {
                int teller = 0;
                TimeSpan interval = DateTime.Now - plant.CreationDate;
                int days = interval.Days;
                switch (days)
                {
                    case int expression when days < 7:
                        teller = 0;
                        break;
                    case int expression when days < 14:
                        teller = 1;
                        break;
                    case int expression when days < 21:
                        teller = 2;
                        break;
                    case int expression when days < 28:
                        teller = 3;
                        break;
                    case int expression when days < 14:
                        teller = 4;
                        break;
                    default:
                        teller = 4;
                        break;
                }
                plant.WaterNeeded = plant.WaterQuantity * teller;
            }
            _context.SaveChanges();
            var tasks = _context.Plant.Where(t => t.WaterNeeded != 0).Include(p => p.Light).Include(p => p.Place);

            return View(tasks);
        }

        public async Task<IActionResult> Done(int? id)
        {
            var plant = await _context.Plant.FindAsync(id);
            //_context.Plant.Remove(plant);
            plant.CreationDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Task));

        }

        // GET: Plants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant
                .Include(p => p.Light)
                .Include(p => p.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // GET: Plants/Create
        public IActionResult Create()
        {
            ViewData["LightId"] = new SelectList(_context.Light, "Id", "LightIntensity");
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Location");
            return View();
        }

        // POST: Plants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,WaterQuantity,CreationDate,PlaceId,LightId")] Plant plant)
        {
            var _user = _context.Users.FirstOrDefault(e => e.UserName == User.Identity.Name);


            if (ModelState.IsValid)
            {
                plant.AppUserId = _user.Id;
                _context.Add(plant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LightId"] = new SelectList(_context.Light, "Id", "LightIntensity", plant.LightId);
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Location", plant.PlaceId);
            return View(plant);
        }

        // GET: Plants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }
            ViewData["LightId"] = new SelectList(_context.Light, "Id", "LightIntensity", plant.LightId);
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Location", plant.PlaceId);
            return View(plant);
        }

        // POST: Plants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,WaterQuantity,CreationDate,PlaceId,LightId, AppUserId")] Plant plant)
        {
            var _user = _context.Users.FirstOrDefault(e => e.UserName == User.Identity.Name);
            if (id != plant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    plant.AppUserId = _user.Id;
                    _context.Update(plant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantExists(plant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LightId"] = new SelectList(_context.Light, "Id", "LightIntensity", plant.LightId);
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Location", plant.PlaceId);
            return View(plant);
        }

        // GET: Plants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plant
                .Include(p => p.Light)
                .Include(p => p.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // POST: Plants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plant = await _context.Plant.FindAsync(id);
            //_context.Plant.Remove(plant);
            plant.Deleted = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantExists(int id)
        {
            return _context.Plant.Any(e => e.Id == id);
        }

       
    }
}
