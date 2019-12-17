using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Loja.Models;

namespace Loja.Controllers
{
    public class ConfeccaoController : Controller
    {
        private readonly LojaContext _context;

        public ConfeccaoController(LojaContext context)
        {
            _context = context;
        }

        // GET: Confeccao
        public async Task<IActionResult> Index()
        {
            var lojaContext = _context.Confeccao.Include(c => c.tipoProduto);
            return View(await lojaContext.ToListAsync());
        }

        // GET: Confeccao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confeccao = await _context.Confeccao
                .Include(c => c.tipoProduto)
                .FirstOrDefaultAsync(m => m.confeccaoId == id);
            if (confeccao == null)
            {
                return NotFound();
            }

            return View(confeccao);
        }

        // GET: Confeccao/Create
        public IActionResult Create()
        {
            ViewData["tipoProdutoId"] = new SelectList(_context.Set<TipoProduto>(), "tipoProdutoId", "tipoProdutoId");
            return View();
        }

        // POST: Confeccao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("confeccaoId,tipoProdutoId,data")] Confeccao confeccao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(confeccao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["tipoProdutoId"] = new SelectList(_context.Set<TipoProduto>(), "tipoProdutoId", "tipoProdutoId", confeccao.tipoProdutoId);
            return View(confeccao);
        }

        // GET: Confeccao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confeccao = await _context.Confeccao.FindAsync(id);
            if (confeccao == null)
            {
                return NotFound();
            }
            ViewData["tipoProdutoId"] = new SelectList(_context.Set<TipoProduto>(), "tipoProdutoId", "tipoProdutoId", confeccao.tipoProdutoId);
            return View(confeccao);
        }

        // POST: Confeccao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("confeccaoId,tipoProdutoId,data")] Confeccao confeccao)
        {
            if (id != confeccao.confeccaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(confeccao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfeccaoExists(confeccao.confeccaoId))
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
            ViewData["tipoProdutoId"] = new SelectList(_context.Set<TipoProduto>(), "tipoProdutoId", "tipoProdutoId", confeccao.tipoProdutoId);
            return View(confeccao);
        }

        // GET: Confeccao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confeccao = await _context.Confeccao
                .Include(c => c.tipoProduto)
                .FirstOrDefaultAsync(m => m.confeccaoId == id);
            if (confeccao == null)
            {
                return NotFound();
            }

            return View(confeccao);
        }

        // POST: Confeccao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var confeccao = await _context.Confeccao.FindAsync(id);
            _context.Confeccao.Remove(confeccao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfeccaoExists(int id)
        {
            return _context.Confeccao.Any(e => e.confeccaoId == id);
        }
    }
}
