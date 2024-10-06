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
    public class InsurancePackagesController : ControllerBase
    {
        private readonly DataContext _context;

        public InsurancePackagesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/InsurancePackages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsurancePackage>>> GetinsurancePackages()
        {
            return await _context.insurancePackage.ToListAsync();
        }

        // GET: api/InsurancePackages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsurancePackage>> GetInsurancePackage(int id)
        {
            var insurancePackage = await _context.insurancePackage.FindAsync(id);

            if (insurancePackage == null)
            {
                return NotFound();
            }

            return insurancePackage;
        }

        // GET: api/InsurancePackages/insurance/5
        [HttpGet("insurance/{id}")]
        public async Task<ActionResult<InsurancePackage>> GetInsurancePackageByInsurance(int id)
        {
            var insurancePackages = await _context.insurancePackage.Where(ip => ip.InsuranceId == id).ToListAsync();

            if (!insurancePackages.Any())
            {
                return NotFound();
            }

            return Ok(insurancePackages);
        }

        // PUT: api/InsurancePackages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsurancePackage(int id, InsurancePackage insurancePackage)
        {
            if (id != insurancePackage.Id)
            {
                return BadRequest();
            }

            _context.Entry(insurancePackage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsurancePackageExists(id))
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

        // POST: api/InsurancePackages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InsurancePackage>> PostInsurancePackage(InsurancePackage insurancePackage)
        {
            _context.insurancePackage.Add(insurancePackage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsurancePackage", new { id = insurancePackage.Id }, insurancePackage);
        }

        // DELETE: api/InsurancePackages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurancePackage(int id)
        {
            var insurancePackage = await _context.insurancePackage.FindAsync(id);
            if (insurancePackage == null)
            {
                return NotFound();
            }

            _context.insurancePackage.Remove(insurancePackage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsurancePackageExists(int id)
        {
            return _context.insurancePackage.Any(e => e.Id == id);
        }
    }
}
