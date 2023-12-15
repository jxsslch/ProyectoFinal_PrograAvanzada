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
    public class ClientesController : Controller
    {
        private readonly SistemaEmpresaContext _context;

        public ClientesController(SistemaEmpresaContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var sistemaEmpresaContext = _context.Clientes.Include(c => c.Canton).Include(c => c.Empresa).Include(c => c.Provincia);
            return View(await sistemaEmpresaContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Canton)
                .Include(c => c.Empresa)
                .Include(c => c.Provincia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["CantonId"] = new SelectList(_context.Cantons, "Id", "Nombre");
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nombre");
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Nombre");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cedula,Nombre,Email,NumTelefono,EmpresaId,ProvinciaId,CantonId")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CantonId"] = new SelectList(_context.Cantons, "Id", "Nombre", cliente.CantonId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nombre", cliente.EmpresaId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Nombre", cliente.ProvinciaId);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["CantonId"] = new SelectList(_context.Cantons, "Id", "Nombre", cliente.CantonId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nombre", cliente.EmpresaId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Nombre", cliente.ProvinciaId);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Cedula,Nombre,Email,NumTelefono,EmpresaId,ProvinciaId,CantonId")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            ViewData["CantonId"] = new SelectList(_context.Cantons, "Id", "Nombre", cliente.CantonId);
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "Id", "Nombre", cliente.EmpresaId);
            ViewData["ProvinciaId"] = new SelectList(_context.Provincia, "Id", "Nombre", cliente.ProvinciaId);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.Canton)
                .Include(c => c.Empresa)
                .Include(c => c.Provincia)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }

                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    ModelState.AddModelError(string.Empty, "No puedes eliminar este cliente porque está siendo referenciado por otros registros.");
                    return View("Delete", await _context.Clientes.Include(c => c.Canton).Include(c => c.Empresa).Include(c => c.Provincia).FirstOrDefaultAsync(m => m.Id == id));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar al cliente.");
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        private bool ClienteExists(string id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
