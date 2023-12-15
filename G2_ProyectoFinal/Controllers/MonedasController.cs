using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using G2_ProyectoFinal.Models;
using Microsoft.Data.SqlClient;

namespace G2_ProyectoFinal.Controllers
{
    public class MonedasController : Controller
    {
        private readonly SistemaEmpresaContext _context;

        public MonedasController(SistemaEmpresaContext context)
        {
            _context = context;
        }

        // GET: Monedas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Moneda.ToListAsync());
        }

        // GET: Monedas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monedum = await _context.Moneda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monedum == null)
            {
                return NotFound();
            }

            return View(monedum);
        }

        // GET: Monedas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Monedas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Monedum monedum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monedum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monedum);
        }

        // GET: Monedas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monedum = await _context.Moneda.FindAsync(id);
            if (monedum == null)
            {
                return NotFound();
            }
            return View(monedum);
        }

        // POST: Monedas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nombre")] Monedum monedum)
        {
            if (id != monedum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monedum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonedumExists(monedum.Id))
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
            return View(monedum);
        }

        // GET: Monedas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monedum = await _context.Moneda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monedum == null)
            {
                return NotFound();
            }

            return View(monedum);
        }

        // POST: Moneda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var monedum = await _context.Moneda.FindAsync(id);
                if (monedum == null)
                {
                    return NotFound();
                }

                _context.Moneda.Remove(monedum);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    ModelState.AddModelError(string.Empty, "No puedes eliminar esta moneda porque está siendo referenciada por otros registros.");
                    return View("Delete", await _context.Moneda.FindAsync(id));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar la moneda.");
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        private bool MonedumExists(string id)
        {
            return _context.Moneda.Any(e => e.Id == id);
        }
    }
}
