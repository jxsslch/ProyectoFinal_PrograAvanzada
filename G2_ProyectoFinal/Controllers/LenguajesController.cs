﻿using System;
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
            string ultimoId = _context.Lenguajes.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefault();
            if (string.IsNullOrEmpty(ultimoId))
            {
                ultimoId = "000";
            }
            int numero = int.Parse(ultimoId?.Substring(ultimoId.Length - 3) ?? "0") + 1;
            string prefijo = "LEN";
            string nuevoId = $"{prefijo}{numero:D3}";
            ViewData["NuevoID"] = nuevoId;
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
            try
            {
                var lenguaje = await _context.Lenguajes.FindAsync(id);
                if (lenguaje == null)
                {
                    return NotFound();
                }

                _context.Lenguajes.Remove(lenguaje);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 547)
                {
                    ModelState.AddModelError(string.Empty, "No puedes eliminar este lenguaje porque está siendo referenciado por otros registros.");
                    return View("Delete", await _context.Lenguajes.FindAsync(id));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar eliminar el lenguaje.");
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        private bool LenguajeExists(string id)
        {
            return _context.Lenguajes.Any(e => e.Id == id);
        }
    }
}
