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
    public class CustomerInsurancesController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerInsurancesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CustomerInsurances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerInsurance>>> GetcustomerInsurances()
        {
            return await _context.customerInsurances.ToListAsync();
        }

        // GET: api/CustomerInsurances/by-user/:id
        [HttpGet("by-user/{id}")]
        public async Task<ActionResult<IEnumerable<CustomerInsurance>>> GetcustomerInsurancesByUser(int id)
        {
            return await _context.customerInsurances.Include(ci => ci.Vehicle).Include(ci => ci.Insurance).Where(ci => ci.Vehicle.userId == id).ToListAsync();
        }

        // GET: api/CustomerInsurances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInsurance>> GetCustomerInsurance(int id)
        {
            var customerInsurance = await _context.customerInsurances.FindAsync(id);

            if (customerInsurance == null)
            {
                return NotFound();
            }

            return customerInsurance;
        }

        // PUT: api/CustomerInsurances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerInsurance(int id, CustomerInsurance customerInsurance)
        {
            if (id != customerInsurance.id)
            {
                return BadRequest();
            }

            _context.Entry(customerInsurance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInsuranceExists(id))
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

        // POST: api/CustomerInsurances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerInsurance>> PostCustomerInsurance(CustomerInsurance customerInsurance)
        {
            _context.customerInsurances.Add(customerInsurance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerInsurance", new { id = customerInsurance.id }, customerInsurance);
        }

        // DELETE: api/CustomerInsurances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerInsurance(int id)
        {
            try
            {
                var customerInsurance = await _context.customerInsurances.FindAsync(id);
                if (customerInsurance == null)
                {
                    return NotFound();
                }

                _context.customerInsurances.Remove(customerInsurance);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                // Check if it's a foreign key constraint violation
                if (ex.InnerException != null && ex.InnerException.Message.Contains("foreign key constraint"))
                {
                    // Provide a user-friendly message with the affected tables
                    return BadRequest(new
                    {
                        message = "Unable to delete the CustomerInsurance because this CustomerInsurance is associated with other data.",
                        details = "This CustomerInsurance is linked to Billing. Please remove or update the related data before deleting."
                    });
                }

                return StatusCode(500, new { message = "An unexpected error occurred while deleting." });
            }
        }

        private bool CustomerInsuranceExists(int id)
        {
            return _context.customerInsurances.Any(e => e.id == id);
        }
    }
}
