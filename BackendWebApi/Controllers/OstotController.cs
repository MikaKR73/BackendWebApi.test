using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendWebApi.Models;

namespace BackendWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OstotController : ControllerBase
    {
        private readonly OstoksetContext _context = new OstoksetContext();

        // GET: api/Ostot
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Osto>>> GetOstos()
        {
            return await _context.Ostos.ToListAsync();
        }

        // GET: api/Ostot/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Osto>> GetOsto(int id)
        {
            var osto = await _context.Ostos.FindAsync(id);

            if (osto == null)
            {
                return NotFound();
            }

            return osto;
        }

        // PUT: api/Ostot/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOsto(int id, Osto osto)
        {
            if (id != osto.Id)
            {
                return BadRequest();
            }

            _context.Entry(osto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OstoExists(id))
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

        // POST: api/Ostot
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Osto>> PostOsto(Osto osto)
        {
            _context.Ostos.Add(osto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOsto", new { id = osto.Id }, osto);
        }

        // DELETE: api/Ostot/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOsto(int id)
        {
            var osto = await _context.Ostos.FindAsync(id);
            if (osto == null)
            {
                return NotFound();
            }

            _context.Ostos.Remove(osto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OstoExists(int id)
        {
            return _context.Ostos.Any(e => e.Id == id);
        }
    }
}
