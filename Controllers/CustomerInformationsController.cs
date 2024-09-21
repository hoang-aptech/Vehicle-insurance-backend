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
    public class CustomerInformationsController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerInformationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CustomerInformations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerInformation>>> GetCustomerInformation()
        {
            return await _context.CustomerInformation.ToListAsync();
        }

        // GET: api/CustomerInformations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerInformation>> GetCustomerInformation(int id)
        {
            var customerInformation = await _context.CustomerInformation.FindAsync(id);

            if (customerInformation == null)
            {
                return NotFound();
            }

            return customerInformation;
        }

        // PUT: api/CustomerInformations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerInformation(int id, CustomerInformation customerInformation)
        {
            if (id != customerInformation.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customerInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInformationExists(id))
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

        // POST: api/CustomerInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerInformation>> PostCustomerInformation(CustomerInformation customerInformation)
        {
            _context.CustomerInformation.Add(customerInformation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerInformation", new { id = customerInformation.CustomerId }, customerInformation);
        }

        // DELETE: api/CustomerInformations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerInformation(int id)
        {
            var customerInformation = await _context.CustomerInformation.FindAsync(id);
            if (customerInformation == null)
            {
                return NotFound();
            }

            _context.CustomerInformation.Remove(customerInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerInformationExists(int id)
        {
            return _context.CustomerInformation.Any(e => e.CustomerId == id);
        }
    }
}
