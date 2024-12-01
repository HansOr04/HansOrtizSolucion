using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HansOrtizWebContactos.Models;

namespace HansOrtizWebContactos.Controllers
{
    public class HO_ContactosController : Controller
    {
        private readonly HansOrtizContactosContext _context;

        public HO_ContactosController(HansOrtizContactosContext context)
        {
            _context = context;
        }

        // GET: HO_Contactos
        public async Task<IActionResult> Index()
        {
            return View("HOIndex", await _context.HO_Contactos.ToListAsync());
        }

        // GET: HO_Contactos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hO_Contactos = await _context.HO_Contactos
                .FirstOrDefaultAsync(m => m.IdHO_Contactos == id);
            if (hO_Contactos == null)
            {
                return NotFound();
            }

            return View("HODetails", hO_Contactos);
        }

        // GET: HO_Contactos/Create
        public IActionResult Create()
        {
            return View("HOCreate");
        }

        // POST: HO_Contactos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHO_Contactos,FirstName,LastName,PhoneNumber,Email")] HO_Contactos hO_Contactos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hO_Contactos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("HOCreate", hO_Contactos);
        }

        // GET: HO_Contactos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hO_Contactos = await _context.HO_Contactos.FindAsync(id);
            if (hO_Contactos == null)
            {
                return NotFound();
            }
            return View("HOEdit", hO_Contactos);
        }

        // POST: HO_Contactos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHO_Contactos,FirstName,LastName,PhoneNumber,Email")] HO_Contactos hO_Contactos)
        {
            if (id != hO_Contactos.IdHO_Contactos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hO_Contactos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HO_ContactosExists(hO_Contactos.IdHO_Contactos))
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
            return View("HOEdit", hO_Contactos);
        }

        // GET: HO_Contactos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hO_Contactos = await _context.HO_Contactos
                .FirstOrDefaultAsync(m => m.IdHO_Contactos == id);
            if (hO_Contactos == null)
            {
                return NotFound();
            }

            return View("HODelete", hO_Contactos);
        }

        // POST: HO_Contactos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hO_Contactos = await _context.HO_Contactos.FindAsync(id);
            if (hO_Contactos != null)
            {
                _context.HO_Contactos.Remove(hO_Contactos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HO_ContactosExists(int id)
        {
            return _context.HO_Contactos.Any(e => e.IdHO_Contactos == id);
        }
    }
}
