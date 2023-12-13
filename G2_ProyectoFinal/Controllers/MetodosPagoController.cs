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

        // POST: MetodosPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var metodoPago = await _context.MetodoPagos.FindAsync(id);
            if (metodoPago != null)
            {
                _context.MetodoPagos.Remove(metodoPago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MetodoPagoExists(string id)
        {
            return _context.MetodoPagos.Any(e => e.Id == id);
        }
    }
}
