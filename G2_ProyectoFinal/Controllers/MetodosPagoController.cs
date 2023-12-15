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
    public class MetodosPagoController : Controller
    {
        private readonly SistemaEmpresaContext _context;

        public MetodosPagoController(SistemaEmpresaContext context)
        {
            _context = context;
        }

        // GET: MetodosPago
        public async Task<IActionResult> Index()
        {
            return View(await _context.MetodoPagos.ToListAsync());
        }

        // GET: MetodosPago/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPago = await _context.MetodoPagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metodoPago == null)
            {
                return NotFound();
            }

            return View(metodoPago);
        }

        // GET: MetodosPago/Create
        public IActionResult Create()
        {
            string ultimoId = _context.MetodoPagos.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefault();
            if (string.IsNullOrEmpty(ultimoId))
            {
                ultimoId = "000";
            }
            int numero = int.Parse(ultimoId?.Substring(ultimoId.Length - 3) ?? "0") + 1;
            string prefijo = "MP";
            string nuevoId = $"{prefijo}{numero:D3}";
            ViewData["NuevoID"] = nuevoId;
            return View();
        }

        // POST: MetodosPago/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] MetodoPago metodoPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(metodoPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(metodoPago);
        }

        // GET: MetodosPago/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPago = await _context.MetodoPagos.FindAsync(id);
            if (metodoPago == null)
            {
                return NotFound();
            }
            return View(metodoPago);
        }

        // POST: MetodosPago/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Descripcion")] MetodoPago metodoPago)
        {
            if (id != metodoPago.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(metodoPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodoPagoExists(metodoPago.Id))
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
            return View(metodoPago);
        }

        // GET: MetodosPago/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var metodoPago = await _context.MetodoPagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (metodoPago == null)
            {
                return NotFound();
            }

            return View(metodoPago);
        }

        // POST: MetodoPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var metodoPago = await _context.MetodoPagos.FindAsync(id);
                if (metodoPago == null)
                {
                    return NotFound();
                }

                _context.MetodoPagos.Remove(metodoPago);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    ModelState.AddModelError(string.Empty, "No puedes eliminar este método de pago porque está siendo referenciado por otros registros.");
                    return View("Delete", await _context.MetodoPagos.FindAsync(id));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar el método de pago.");
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        private bool MetodoPagoExists(string id)
        {
            return _context.MetodoPagos.Any(e => e.Id == id);
        }
    }
}
