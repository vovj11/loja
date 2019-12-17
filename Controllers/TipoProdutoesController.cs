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
    public class TipoProdutoesController : Controller
    {
        private readonly LojaContext _context;

        public TipoProdutoesController(LojaContext context)
        {
            _context = context;
        }

        // GET: TipoProdutoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoProduto.ToListAsync());
        }

        public async Task<IActionResult> JogarPedido([Bind("tipoProdutoId,nome,qtd")] TipoProduto tipoProduto)
        {
            Confeccao confeccao = new Confeccao(tipoProduto);
            _context.Update(confeccao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: TipoProdutoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProduto
                .FirstOrDefaultAsync(m => m.tipoProdutoId == id);
            if (tipoProduto == null)
            {
                return NotFound();
            }

            return View(tipoProduto);
        }

        // GET: TipoProdutoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoProdutoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("tipoProdutoId,nome,qtd")] TipoProduto tipoProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoProduto);
        }

        // GET: TipoProdutoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProduto.FindAsync(id);
            if (tipoProduto == null)
            {
                return NotFound();
            }
            return View(tipoProduto);
        }

        // POST: TipoProdutoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("tipoProdutoId,nome,qtd")] TipoProduto tipoProduto)
        {
            if (id != tipoProduto.tipoProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoProdutoExists(tipoProduto.tipoProdutoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (tipoProduto.qtd == 0)
                {
                    return RedirectToAction("JogarPedido", tipoProduto);
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(tipoProduto);
        }

        // GET: TipoProdutoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoProduto = await _context.TipoProduto
                .FirstOrDefaultAsync(m => m.tipoProdutoId == id);
            if (tipoProduto == null)
            {
                return NotFound();
            }

            return View(tipoProduto);
        }

        // POST: TipoProdutoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoProduto = await _context.TipoProduto.FindAsync(id);
            _context.TipoProduto.Remove(tipoProduto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoProdutoExists(int id)
        {
            return _context.TipoProduto.Any(e => e.tipoProdutoId == id);
        }
    }
}
