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
    public class InsurancecontentsController : ControllerBase
    {
        private readonly DataContext _context;

        public InsurancecontentsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Insurancecontents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insurancecontent>>> Getinsurancecontents()
        {
            return await _context.insurancecontents.ToListAsync();
        }

        // GET: api/Insurancecontents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Insurancecontent>> GetInsurancecontent(int id)
        {
            var insurancecontent = await _context.insurancecontents.FindAsync(id);

            if (insurancecontent == null)
            {
                return NotFound();
            }

            return insurancecontent;
        }

        // GET: api/Insurancecontents/Insurance/id
        [HttpGet("Insurance/{InsuranceId}")]
        public async Task<ActionResult<IEnumerable<Insurancecontent>>> GetinsurancecontentsByInsurance(int InsuranceId)
        {
            return await _context.insurancecontents.Where(ic => ic.InsuranceId == InsuranceId).ToListAsync();
        }

        // PUT: api/Insurancecontents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsurancecontent(int id, Insurancecontent insurancecontent)
        {
            if (id != insurancecontent.Id)
            {
                return BadRequest();
            }

            _context.Entry(insurancecontent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsurancecontentExists(id))
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

        // POST: api/Insurancecontents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Insurancecontent>> PostInsurancecontent(Insurancecontent insurancecontent)
        {
            _context.insurancecontents.Add(insurancecontent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsurancecontent", new { id = insurancecontent.Id }, insurancecontent);
        }

        // DELETE: api/Insurancecontents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurancecontent(int id)
        {
            var insurancecontent = await _context.insurancecontents.FindAsync(id);
            if (insurancecontent == null)
            {
                return NotFound();
            }

            _context.insurancecontents.Remove(insurancecontent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsurancecontentExists(int id)
        {
            return _context.insurancecontents.Any(e => e.Id == id);
        }
    }
}
