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
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {
            return await _context.users.ToListAsync();
        }

        // GET: api/Users/deleted
        [HttpGet("deleted")]
        public async Task<ActionResult<IEnumerable<User>>> GetusersDeleted()
        {
            return await _context.users.Where(u => u.deleted == true).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _context.users.Remove(user);
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
                        message = "Unable to delete the User because this User is associated with other data.",
                        details = "This User is linked to Vehicle and customer support. Please remove or update the related data before deleting."
                    });
                }

                return StatusCode(500, new { message = "An unexpected error occurred while deleting." });
            }
        }

        private bool UserExists(int id)
        {
            return _context.users.Any(e => e.id == id);
        }
    }
}
