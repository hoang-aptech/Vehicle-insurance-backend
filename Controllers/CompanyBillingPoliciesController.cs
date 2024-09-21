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
    public class CompanyBillingPoliciesController : ControllerBase
    {
        private readonly DataContext _context;

        public CompanyBillingPoliciesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CompanyBillingPolicies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyBillingPolicy>>> GetCompanyBillingPolicy()
        {
            return await _context.CompanyBillingPolicy.ToListAsync();
        }

        // GET: api/CompanyBillingPolicies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyBillingPolicy>> GetCompanyBillingPolicy(int id)
        {
            var companyBillingPolicy = await _context.CompanyBillingPolicy.FindAsync(id);

            if (companyBillingPolicy == null)
            {
                return NotFound();
            }

            return companyBillingPolicy;
        }

        // PUT: api/CompanyBillingPolicies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyBillingPolicy(int id, CompanyBillingPolicy companyBillingPolicy)
        {
            if (id != companyBillingPolicy.Id)
            {
                return BadRequest();
            }

            _context.Entry(companyBillingPolicy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyBillingPolicyExists(id))
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

        // POST: api/CompanyBillingPolicies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyBillingPolicy>> PostCompanyBillingPolicy(CompanyBillingPolicy companyBillingPolicy)
        {
            _context.CompanyBillingPolicy.Add(companyBillingPolicy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyBillingPolicy", new { id = companyBillingPolicy.Id }, companyBillingPolicy);
        }

        // DELETE: api/CompanyBillingPolicies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyBillingPolicy(int id)
        {
            var companyBillingPolicy = await _context.CompanyBillingPolicy.FindAsync(id);
            if (companyBillingPolicy == null)
            {
                return NotFound();
            }

            _context.CompanyBillingPolicy.Remove(companyBillingPolicy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyBillingPolicyExists(int id)
        {
            return _context.CompanyBillingPolicy.Any(e => e.Id == id);
        }
    }
}
