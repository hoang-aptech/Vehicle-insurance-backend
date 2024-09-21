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
    public class InsuranceProcessesController : ControllerBase
    {
        private readonly DataContext _context;

        public InsuranceProcessesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/InsuranceProcesses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsuranceProcess>>> GetInsuranceProcess()
        {
            return await _context.InsuranceProcess.ToListAsync();
        }

        // GET: api/InsuranceProcesses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsuranceProcess>> GetInsuranceProcess(int id)
        {
            var insuranceProcess = await _context.InsuranceProcess.FindAsync(id);

            if (insuranceProcess == null)
            {
                return NotFound();
            }

            return insuranceProcess;
        }

        // PUT: api/InsuranceProcesses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsuranceProcess(int id, InsuranceProcess insuranceProcess)
        {
            if (id != insuranceProcess.Id)
            {
                return BadRequest();
            }

            _context.Entry(insuranceProcess).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuranceProcessExists(id))
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

        // POST: api/InsuranceProcesses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InsuranceProcess>> PostInsuranceProcess(InsuranceProcess insuranceProcess)
        {
            _context.InsuranceProcess.Add(insuranceProcess);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsuranceProcess", new { id = insuranceProcess.Id }, insuranceProcess);
        }

        // DELETE: api/InsuranceProcesses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsuranceProcess(int id)
        {
            var insuranceProcess = await _context.InsuranceProcess.FindAsync(id);
            if (insuranceProcess == null)
            {
                return NotFound();
            }

            _context.InsuranceProcess.Remove(insuranceProcess);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsuranceProcessExists(int id)
        {
            return _context.InsuranceProcess.Any(e => e.Id == id);
        }
    }
}
