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
    public class VehicleInformationsController : ControllerBase
    {
        private readonly DataContext _context;

        public VehicleInformationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/VehicleInformations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleInformation>>> GetVehicleInformation()
        {
            return await _context.VehicleInformation.ToListAsync();
        }

        // GET: api/VehicleInformations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleInformation>> GetVehicleInformation(int id)
        {
            var vehicleInformation = await _context.VehicleInformation.FindAsync(id);

            if (vehicleInformation == null)
            {
                return NotFound();
            }

            return vehicleInformation;
        }

        // PUT: api/VehicleInformations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleInformation(int id, VehicleInformation vehicleInformation)
        {
            if (id != vehicleInformation.VehicleId)
            {
                return BadRequest();
            }

            _context.Entry(vehicleInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleInformationExists(id))
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

        // POST: api/VehicleInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleInformation>> PostVehicleInformation(VehicleInformation vehicleInformation)
        {
            _context.VehicleInformation.Add(vehicleInformation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleInformation", new { id = vehicleInformation.VehicleId }, vehicleInformation);
        }

        // DELETE: api/VehicleInformations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleInformation(int id)
        {
            var vehicleInformation = await _context.VehicleInformation.FindAsync(id);
            if (vehicleInformation == null)
            {
                return NotFound();
            }

            _context.VehicleInformation.Remove(vehicleInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleInformationExists(int id)
        {
            return _context.VehicleInformation.Any(e => e.VehicleId == id);
        }
    }
}
