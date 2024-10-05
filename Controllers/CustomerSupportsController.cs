﻿using System;
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
    public class CustomerSupportsController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomerSupportsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CustomerSupports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerSupport>>> GetcustomerSupports()
        {
            return await _context.customerSupports.ToListAsync();
        }

        [HttpGet("by-user/{userId}")]
        public IActionResult GetCustomerSupportByUserId(int userId)
        {
            var query = _context.customerSupports
                .Include(cs => cs.vehicle)
                .ThenInclude(v => v.User)
                .Where(cs => cs.vehicle.userId == userId).Select(cs => new
                {
                    Id = cs.id,
                    Type = cs.type,
                    Description = cs.description,
                    Place = cs.place,
                    VehicleName = cs.vehicle.name,
                    CreatedAt = cs.createdAt,
                });

            var customerSupports = query.ToList();

            if (customerSupports == null || !customerSupports.Any())
            {
                return NotFound("No customer support found for this user.");
            }

            return Ok(customerSupports);
        }

        // GET: api/CustomerSupports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerSupport>> GetCustomerSupport(int id)
        {
            var customerSupport = await _context.customerSupports.FindAsync(id);

            if (customerSupport == null)
            {
                return NotFound();
            }

            return customerSupport;
        }

        // PUT: api/CustomerSupports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerSupport(int id, CustomerSupport customerSupport)
        {
            if (id != customerSupport.id)
            {
                return BadRequest();
            }

            _context.Entry(customerSupport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerSupportExists(id))
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

        // POST: api/CustomerSupports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CustomerSupport>> PostCustomerSupport(CustomerSupport customerSupport)
        {
            _context.customerSupports.Add(customerSupport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerSupport", new { id = customerSupport.id }, customerSupport);
        }

        // DELETE: api/CustomerSupports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerSupport(int id)
        {
            var customerSupport = await _context.customerSupports.FindAsync(id);
            if (customerSupport == null)
            {
                return NotFound();
            }

            _context.customerSupports.Remove(customerSupport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerSupportExists(int id)
        {
            return _context.customerSupports.Any(e => e.id == id);
        }
    }
}