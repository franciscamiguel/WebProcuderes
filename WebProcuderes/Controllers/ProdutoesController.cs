using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebProcuderes.Data;
using WebProcuderes.Models;

namespace WebProcuderes.Controllers
{
    public class ProdutoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produtoes
        public async Task<IActionResult> Index()
        {
            //return _context.Produto != null ? 
            //View(await _context.Produto.ToListAsync()) :
            //Problem("Entity set 'ApplicationDbContext.Produto'  is null.");
            var listagem = await _context.Produto.FromSqlRaw("ConsultaTodos").ToListAsync();
            return View(listagem);

        }

        // GET: Produtoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var param = new SqlParameter("@id", id);
            var produto = await _context.Produto.FromSqlRaw("Consulta @id", param).FirstOrDefaultAsync();

           // var produto = await _context.Produto
                //.FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                // _context.Add(produto);

                var param = new SqlParameter("@nome", produto.Nome);
               await _context.Database.ExecuteSqlRawAsync("Cadastro @nome", param);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            // var produto = await _context.Produto.FindAsync(id);


            var param = new SqlParameter("@id", id);
            var produto = await _context.Produto.FromSqlRaw("Consulta @id", param).FirstOrDefaultAsync();

            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // _context.Update(produto);
                    var param = new SqlParameter("@id", produto.Id);
                    var param2 = new SqlParameter("@nome", produto.Nome);
                    await _context.Database.ExecuteSqlRawAsync("Alterar @id, @nome", param, param2);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
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
            return View(produto);
        }

        // GET: Produtoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produto == null)
            {
                return NotFound();
            }

            //var produto = await _context.Produto
            //.FirstOrDefaultAsync(m => m.Id == id);
            var param = new SqlParameter("@id", id);
            var produto = await _context.Produto.FromSqlRaw("Consulta @id", param).FirstOrDefaultAsync();

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produto == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Produto'  is null.");
            }
            //var produto = await _context.Produto.FindAsync(id);

            // var produto = await _context.Produto.FromSqlRaw("Consultar @id", param).FirstOrDefaultAsync();

            var param = new SqlParameter("@id", id);
            await _context.Database.ExecuteSqlRawAsync("Excluir @id,", param);

           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private  bool ProdutoExists(int id)
        {
            //return (_context.Produto?.Any(e => e.Id == id)).GetValueOrDefault();
            var param = new SqlParameter("@id", id);
            var produto =  _context.Produto.FromSqlRaw("Consulta @id", param).Any();
            return produto;

        }
    }
}
