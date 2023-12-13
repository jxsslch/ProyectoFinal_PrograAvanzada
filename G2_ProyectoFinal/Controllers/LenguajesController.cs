using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using G2_ProyectoFinal.Models;

namespace G2_ProyectoFinal.Controllers
{
    public class LenguajesController : Controller
    {
        private readonly SistemaEmpresaContext _context;

        public LenguajesController(SistemaEmpresaContext context)
        {
            _context = context;
        }

        // GET: Lenguajes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lenguajes.ToListAsync());
        }

        // GET: Lenguajes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lenguaje = await _context.Lenguajes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lenguaje == null)
            {
                return NotFound();
            }

            return View(lenguaje);
        }

        // GET: Lenguajes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lenguajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Lenguaje lenguaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lenguaje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lenguaje);
        }

        // GET: Lenguajes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lenguaje = await _context.Lenguajes.FindAsync(id);
            if (lenguaje == null)
            {
                return NotFound();
            }
            return View(lenguaje);
        }

        // POST: Lenguajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nombre")] Lenguaje lenguaje)
        {
            if (id != lenguaje.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lenguaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LenguajeExists(lenguaje.Id))
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
            return View(lenguaje);
        }

        // GET: Lenguajes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lenguaje = await _context.Lenguajes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lenguaje == null)
            {
                return NotFound();
            }

            return View(lenguaje);
        }

        // POST: Lenguajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lenguaje = await _context.Lenguajes.FindAsync(id);
            if (lenguaje != null)
            {
                _context.Lenguajes.Remove(lenguaje);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LenguajeExists(string id)
        {
            return _context.Lenguajes.Any(e => e.Id == id);
        }
    }
}
