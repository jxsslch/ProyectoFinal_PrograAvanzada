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
    public class EmpresasController : Controller
    {
        private readonly SistemaEmpresaContext _context;

        public EmpresasController(SistemaEmpresaContext context)
        {
            _context = context;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Empresas.ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            string ultimoId = _context.Empresas.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefault();
            if (string.IsNullOrEmpty(ultimoId))
            {
                ultimoId = "000";
            }
            int numero = int.Parse(ultimoId?.Substring(ultimoId.Length - 3) ?? "0") + 1;
            string prefijo = "EM";
            string nuevoId = $"{prefijo}{numero:D3}";
            ViewData["NuevoID"] = nuevoId;
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,NumTelefono,Email")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Nombre,NumTelefono,Email")] Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
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
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                var empresa = await _context.Empresas.FindAsync(id);
                if (empresa == null)
                {
                    return NotFound();
                }

                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    ModelState.AddModelError(string.Empty, "No puedes eliminar esta empresa porque está siendo referenciada por otros registros.");
                    return View("Delete", await _context.Empresas.FindAsync(id));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar la empresa.");
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        private bool EmpresaExists(string id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }
    }
}
