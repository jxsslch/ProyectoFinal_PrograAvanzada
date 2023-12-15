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
    public class CantonesController : Controller
    {
        private readonly SistemaEmpresaContext _context;

        public CantonesController(SistemaEmpresaContext context)
        {
            _context = context;
        }

        // GET: Cantones
        public async Task<IActionResult> Index()
        {
            var sistemaEmpresaContext = _context.Cantons.Include(c => c.Provincia);
            return View(await sistemaEmpresaContext.ToListAsync());
        }

        // GET: Cantones/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canton = await _context.Cantons
                .Include(c => c.Provincia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (canton == null)
            {
                return NotFound();
            }

            return View(canton);
        }

        // GET: Cantones/Create
        public IActionResult Create()
        {
            string ultimoId = _context.Cantons.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefault();
            if (string.IsNullOrEmpty(ultimoId))
            {
                ultimoId = "000";
            }
            int numero = int.Parse(ultimoId?.Substring(ultimoId.Length - 3) ?? "0") + 1;
            string prefijo = "CA";
            string nuevoId = $"{prefijo}{numero:D3}";
            ViewData["NuevoID"] = nuevoId;
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Nombre");
            return View();
        }

        // POST: Cantones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,ProvinciaId")] Canton canton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(canton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Nombre", canton.ProvinciaId);
            return View(canton);
        }

        // GET: Cantones/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canton = await _context.Cantons.FindAsync(id);
            if (canton == null)
            {
                return NotFound();
            }
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Nombre", canton.ProvinciaId);
            return View(canton);
        }

        // POST: Cantones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nombre,ProvinciaId")] Canton canton)
        {
            if (id != canton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(canton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CantonExists(canton.Id))
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
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Nombre", canton.ProvinciaId);
            return View(canton);
        }

        // GET: Cantones/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var canton = await _context.Cantons
                .Include(c => c.Provincia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (canton == null)
            {
                return NotFound();
            }

            return View(canton);
        }

        // POST: Cantones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var canton = await _context.Cantons.FindAsync(id);

                if (canton == null)
                {
                    return NotFound();
                }

                _context.Cantons.Remove(canton);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    ModelState.AddModelError(string.Empty, "No puedes eliminar este cantón porque está siendo referenciado por otros registros.");

                    var canton = await _context.Cantons.FindAsync(id);

                    return View("Delete", canton);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar el cantón.");
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        private bool CantonExists(string id)
        {
            return _context.Cantons.Any(e => e.Id == id);
        }
    }
}
