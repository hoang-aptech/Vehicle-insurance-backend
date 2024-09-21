using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicle_insurance_backend.DataCtxt;
using vehicle_insurance_backend.models;

namespace vehicle_insurance_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimatesController : ControllerBase
    {
        private readonly DataContext _context;

        public EstimatesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Estimates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estimate>>> GetEstimates()
        {
            return await _context.Estimates.ToListAsync();
        }

        // GET: api/Estimates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estimate>> GetEstimate(int id)
        {
            var estimate = await _context.Estimates.FindAsync(id);

            if (estimate == null)
            {
                return NotFound();
            }

            return estimate;
        }

        // PUT: api/Estimates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstimate(int id, Estimate estimate)
        {
            if (id != estimate.Id)
            {
                return BadRequest();
            }

            _context.Entry(estimate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstimateExists(id))
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

        // POST: api/Estimates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estimate>> PostEstimate(Estimate estimate)
        {
            _context.Estimates.Add(estimate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstimate", new { id = estimate.Id }, estimate);
        }

        // DELETE: api/Estimates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstimate(int id)
        {
            var estimate = await _context.Estimates.FindAsync(id);
            if (estimate == null)
            {
                return NotFound();
            }

            _context.Estimates.Remove(estimate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstimateExists(int id)
        {
            return _context.Estimates.Any(e => e.Id == id);
        }
    }
}
