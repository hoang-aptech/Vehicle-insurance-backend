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
    public class ClaimDetailsController : ControllerBase
    {
        private readonly DataContext _context;

        public ClaimDetailsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/ClaimDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimDetails>>> GetClaimDetails()
        {
            return await _context.ClaimDetails.ToListAsync();
        }

        // GET: api/ClaimDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClaimDetails>> GetClaimDetails(int id)
        {
            var claimDetails = await _context.ClaimDetails.FindAsync(id);

            if (claimDetails == null)
            {
                return NotFound();
            }

            return claimDetails;
        }

        // PUT: api/ClaimDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClaimDetails(int id, ClaimDetails claimDetails)
        {
            if (id != claimDetails.Id)
            {
                return BadRequest();
            }

            _context.Entry(claimDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimDetailsExists(id))
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

        // POST: api/ClaimDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClaimDetails>> PostClaimDetails(ClaimDetails claimDetails)
        {
            _context.ClaimDetails.Add(claimDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClaimDetails", new { id = claimDetails.Id }, claimDetails);
        }

        // DELETE: api/ClaimDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaimDetails(int id)
        {
            var claimDetails = await _context.ClaimDetails.FindAsync(id);
            if (claimDetails == null)
            {
                return NotFound();
            }

            _context.ClaimDetails.Remove(claimDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClaimDetailsExists(int id)
        {
            return _context.ClaimDetails.Any(e => e.Id == id);
        }
    }
}
