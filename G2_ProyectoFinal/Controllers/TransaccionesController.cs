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
    public class TransaccionesController : Controller
    {
        private readonly SistemaEmpresaContext _context;

        public TransaccionesController(SistemaEmpresaContext context)
        {
            _context = context;
        }

        // GET: Transacciones
        public async Task<IActionResult> Index()
        {
            var sistemaEmpresaContext = _context.Transacciones.Include(t => t.Cliente).Include(t => t.Lenguaje).Include(t => t.MetodoPago).Include(t => t.Moneda);
            return View(await sistemaEmpresaContext.ToListAsync());
        }

        // GET: Transacciones/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccione = await _context.Transacciones
                .Include(t => t.Cliente)
                .Include(t => t.Lenguaje)
                .Include(t => t.MetodoPago)
                .Include(t => t.Moneda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccione == null)
            {
                return NotFound();
            }

            return View(transaccione);
        }

        // GET: Transacciones/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre");
            ViewData["LenguajeId"] = new SelectList(_context.Lenguajes, "Id", "Nombre");
            ViewData["MetodoPagoId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion");
            ViewData["MonedaId"] = new SelectList(_context.Moneda, "Id", "Nombre");
            return View();
        }

        // POST: Transacciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,LenguajeId,MonedaId,MetodoPagoId,Monto,Fecha")] Transaccione transaccione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaccione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", transaccione.ClienteId);
            ViewData["LenguajeId"] = new SelectList(_context.Lenguajes, "Id", "Nombre", transaccione.LenguajeId);
            ViewData["MetodoPagoId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", transaccione.MetodoPagoId);
            ViewData["MonedaId"] = new SelectList(_context.Moneda, "Id", "Nombre", transaccione.MonedaId);
            return View(transaccione);
        }

        // GET: Transacciones/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccione = await _context.Transacciones.FindAsync(id);
            if (transaccione == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", transaccione.ClienteId);
            ViewData["LenguajeId"] = new SelectList(_context.Lenguajes, "Id", "Nombre", transaccione.LenguajeId);
            ViewData["MetodoPagoId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", transaccione.MetodoPagoId);
            ViewData["MonedaId"] = new SelectList(_context.Moneda, "Id", "Nombre", transaccione.MonedaId);
            return View(transaccione);
        }

        // POST: Transacciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ClienteId,LenguajeId,MonedaId,MetodoPagoId,Monto,Fecha")] Transaccione transaccione)
        {
            if (id != transaccione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaccione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccioneExists(transaccione.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nombre", transaccione.ClienteId);
            ViewData["LenguajeId"] = new SelectList(_context.Lenguajes, "Id", "Nombre", transaccione.LenguajeId);
            ViewData["MetodoPagoId"] = new SelectList(_context.MetodoPagos, "Id", "Descripcion", transaccione.MetodoPagoId);
            ViewData["MonedaId"] = new SelectList(_context.Moneda, "Id", "Nombre", transaccione.MonedaId);
            return View(transaccione);
        }

        // GET: Transacciones/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccione = await _context.Transacciones
                .Include(t => t.Cliente)
                .Include(t => t.Lenguaje)
                .Include(t => t.MetodoPago)
                .Include(t => t.Moneda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaccione == null)
            {
                return NotFound();
            }

            return View(transaccione);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var transaccione = await _context.Transacciones.FindAsync(id);
            if (transaccione != null)
            {
                _context.Transacciones.Remove(transaccione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccioneExists(string id)
        {
            return _context.Transacciones.Any(e => e.Id == id);
        }
    }
}
