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
    public class ProvinciasController : Controller
    {
        private readonly SistemaEmpresaContext _context;

        public ProvinciasController(SistemaEmpresaContext context)
        {
            _context = context;
        }

        // GET: Provincias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Provincia.ToListAsync());
        }

        // GET: Provincias/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincium = await _context.Provincia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provincium == null)
            {
                return NotFound();
            }

            return View(provincium);
        }

        // GET: Provincias/Create
        public IActionResult Create()
        {
            string ultimoId = _context.Provincia.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefault();
            if (string.IsNullOrEmpty(ultimoId))
            {
                ultimoId = "000";
            }
            int numero = int.Parse(ultimoId?.Substring(ultimoId.Length - 3) ?? "0") + 1;
            string prefijo = "PR";
            string nuevoId = $"{prefijo}{numero:D3}";
            ViewData["NuevoID"] = nuevoId;
            return View();
        }

        // POST: Provincias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Provincium provincium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provincium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provincium);
        }

        // GET: Provincias/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincium = await _context.Provincia.FindAsync(id);
            if (provincium == null)
            {
                return NotFound();
            }
            return View(provincium);
        }

        // POST: Provincias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nombre")] Provincium provincium)
        {
            if (id != provincium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provincium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinciumExists(provincium.Id))
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
            return View(provincium);
        }

        // GET: Provincias/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provincium = await _context.Provincia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (provincium == null)
            {
                return NotFound();
            }

            return View(provincium);
        }

        // POST: Provincia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var provincium = await _context.Provincia.FindAsync(id);

                if (provincium == null)
                {
                    return NotFound();
                }

                _context.Provincia.Remove(provincium);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    ModelState.AddModelError(string.Empty, "No puedes eliminar esta provincia porque está siendo referenciada por otros registros.");

                    var provincia = await _context.Provincia.FindAsync(id);

                    return View("Delete", provincia);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar la provincia.");
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        private bool ProvinciumExists(string id)
        {
            return _context.Provincia.Any(e => e.Id == id);
        }
    }
}
