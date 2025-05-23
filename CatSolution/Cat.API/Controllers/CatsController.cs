using Cat.Core.Entities;
using Cat.Infrastructure.Data;
using Cat.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cat.API.Controllers
{
    // CatsController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class CatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Core.Entities.Cat>>> GetCats()
        {
            return await _context.Cats
                .Include(c => c.Breed)
                .Include(c => c.Club)
                .Include(c => c.Owner)
                .Include(c => c.Ring)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Core.Entities.Cat>> GetCat(int id)
        {
            var cat = await _context.Cats.FindAsync(id);

            if (cat == null)
            {
                return NotFound();
            }

            return cat;
        }

        [HttpPost]
        public async Task<ActionResult<Core.Entities.Cat>> PostCat(Core.Entities.Cat cat)
        {
            _context.Cats.Add(cat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCat", new { id = cat.Id }, cat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCat(int id, Core.Entities.Cat cat)
        {
            if (id != cat.Id)
            {
                return BadRequest();
            }

            _context.Entry(cat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCat(int id)
        {
            var cat = await _context.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            _context.Cats.Remove(cat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/disqualify")]
        public async Task<IActionResult> DisqualifyCat(int id)
        {
            var cat = await _context.Cats.FindAsync(id);
            if (cat == null)
            {
                return NotFound();
            }

            cat.IsDisqualified = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatExists(int id)
        {
            return _context.Cats.Any(e => e.Id == id);
        }
    }

    // Аналогичные контроллеры для ExpertsController, RingsController, ClubsController и т.д.
}