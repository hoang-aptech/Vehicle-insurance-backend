using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vehicle_insurance_backend.DataCtxt;
using vehicle_insurance_backend.models;

namespace vehicle_insurance_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly DataContext _context;

        public VehiclesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> Getvehicles()
        {
            return await _context.vehicles.ToListAsync();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _context.vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // GET: api/Vehicles/5`
        [Authorize(Roles = "User")]
        [HttpGet("by-user/{id}")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicleByUser(int id)
        {
            var vehicle = await _context.vehicles.Where(v => v.userId == id).ToListAsync();

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle(int id, Vehicle vehicle)
        {
            if (id != vehicle.id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicle(Vehicle vehicle)
        {
            _context.vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicle", new { id = vehicle.id }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            try
            {
                var vehicle = await _context.vehicles.FindAsync(id);
                if (vehicle == null)
                {
                    return NotFound();
                }

                _context.vehicles.Remove(vehicle);
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
                        message = "Unable to delete the vehicle because this vehicle is associated with other data.",
                        details = "This vehicle is linked to owner and user's insurance. Please remove or update the related data before deleting."
                    });
                }

                return StatusCode(500, new { message = "An unexpected error occurred while deleting." });
            }
        }

        private bool VehicleExists(int id)
        {
            return _context.vehicles.Any(e => e.id == id);
        }
    }
}
