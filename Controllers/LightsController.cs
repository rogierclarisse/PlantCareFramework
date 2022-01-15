#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlantCareFramework.Data;
using PlantCareFramework.Models;

namespace PlantCareFramework.Controllers
{
    public class LightsController : Controller
    {
        private readonly PlantCareContext _context;

        public LightsController(PlantCareContext context)
        {
            _context = context;
        }

        // GET: Lights
        public async Task<IActionResult> Index()
        {
            return View(await _context.Light.ToListAsync());
        }

        // GET: Lights/Details/5
        public async Task<IActionResult> Details(char? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var light = await _context.Light
                .FirstOrDefaultAsync(m => m.Id == id);
            if (light == null)
            {
                return NotFound();
            }

            return View(light);
        }

        // GET: Lights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,None,Medium,High")] Light light)
        {
            if (ModelState.IsValid)
            {
                _context.Add(light);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(light);
        }

        // GET: Lights/Edit/5
        public async Task<IActionResult> Edit(char? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var light = await _context.Light.FindAsync(id);
            if (light == null)
            {
                return NotFound();
            }
            return View(light);
        }

        // POST: Lights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(char id, [Bind("Id,None,Medium,High")] Light light)
        {
            if (id != light.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(light);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LightExists(light.Id))
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
            return View(light);
        }

        // GET: Lights/Delete/5
        public async Task<IActionResult> Delete(char? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var light = await _context.Light
                .FirstOrDefaultAsync(m => m.Id == id);
            if (light == null)
            {
                return NotFound();
            }

            return View(light);
        }

        // POST: Lights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(char id)
        {
            var light = await _context.Light.FindAsync(id);
            _context.Light.Remove(light);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LightExists(char id)
        {
            return _context.Light.Any(e => e.Id == id);
        }
    }
}
