using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicle_insurance_backend.DataCtxt;
using vehicle_insurance_backend.FormModels;
using vehicle_insurance_backend.models;

namespace vehicle_insurance_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingsController : ControllerBase
    {
        private readonly DataContext _context;

        public BillingsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Billings
        [Authorize(Roles = "Admin, Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Billing>>> Getbillings()
        {
            return await _context.billings.Include(b => b.Vehicle).Include(b => b.InsurancePackage).ToListAsync();
        }

        // GET: api/Billings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Billing>> GetBilling(int id)
        {
            var billing = await _context.billings.FindAsync(id);

            if (billing == null)
            {
                return NotFound();
            }

            return billing;
        }

        [Authorize(Roles = "User, Employee")]
        // GET: api/Billings/by-user/:id
        [HttpGet("by-user/{id}")]
        public async Task<ActionResult<IEnumerable<Billing>>> GetBillingsByUser(int id)
        {
            return await _context.billings.Include(b => b.Vehicle).Include(b => b.InsurancePackage).ThenInclude(ip => ip.Insurance).Where(b => b.Vehicle.userId == id).ToListAsync();
        }

        [Authorize(Roles = "User, Employee")]
        // GET: api/Billings/transactions/:id
        [HttpGet("transactions/{id}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(int id)
        {
            return await _context.transactions.Where(t => t.BillingId == id).ToListAsync();
        }

        // PUT: api/Billings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBilling(int id, Billing billing)
        {
            if (id != billing.id)
            {
                return BadRequest();
            }

            _context.Entry(billing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillingExists(id))
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

        // PACTH: api/Billings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin, Employee")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBilling(int id, [FromBody] PatchBillingDTO patchBillingDTO)
        {
            var billing = await _context.billings.FindAsync(id); // FindAsync for async operation

            if (billing == null)
            {
                return NotFound(); // Handle case where billing doesn't exist
            }

            billing.expireDate = patchBillingDTO.expireDate;

            await _context.SaveChangesAsync(); // Ensure asynchronous save

            return Ok(); // Return Ok status to indicate success
        }


        // POST: api/Billings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin, Employee")]
        [HttpPost]
        public async Task<ActionResult<Billing>> PostBilling(Billing billing)
        {
            _context.billings.Add(billing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBilling", new { id = billing.id }, billing);
        }

        // DELETE: api/Billings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBilling(int id)
        {
            var billing = await _context.billings.FindAsync(id);
            if (billing == null)
            {
                return NotFound();
            }

            _context.billings.Remove(billing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillingExists(int id)
        {
            return _context.billings.Any(e => e.id == id);
        }
    }
}
