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
    public class CompanyExpensesController : ControllerBase
    {
        private readonly DataContext _context;

        public CompanyExpensesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CompanyExpenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyExpenses>>> GetCompanyExpenses()
        {
            return await _context.CompanyExpenses.ToListAsync();
        }

        // GET: api/CompanyExpenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyExpenses>> GetCompanyExpenses(int id)
        {
            var companyExpenses = await _context.CompanyExpenses.FindAsync(id);

            if (companyExpenses == null)
            {
                return NotFound();
            }

            return companyExpenses;
        }

        // PUT: api/CompanyExpenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyExpenses(int id, CompanyExpenses companyExpenses)
        {
            if (id != companyExpenses.Id)
            {
                return BadRequest();
            }

            _context.Entry(companyExpenses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExpensesExists(id))
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

        // POST: api/CompanyExpenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyExpenses>> PostCompanyExpenses(CompanyExpenses companyExpenses)
        {
            _context.CompanyExpenses.Add(companyExpenses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyExpenses", new { id = companyExpenses.Id }, companyExpenses);
        }

        // DELETE: api/CompanyExpenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyExpenses(int id)
        {
            var companyExpenses = await _context.CompanyExpenses.FindAsync(id);
            if (companyExpenses == null)
            {
                return NotFound();
            }

            _context.CompanyExpenses.Remove(companyExpenses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExpensesExists(int id)
        {
            return _context.CompanyExpenses.Any(e => e.Id == id);
        }
    }
}
