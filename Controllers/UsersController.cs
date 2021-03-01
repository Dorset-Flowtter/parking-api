using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingApp.Data;
using ParkingApp.Models;

namespace ParkingApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly Context _context;
        public UsersController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingValues>>> GetValues()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{licensePlate}")]
        public async Task<ActionResult<ParkingValues>> GetValues_ById(int licensePlate)
        {
            var values = await _context.Users.FindAsync(licensePlate);
            if(values != null)
            {
                return values;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<ParkingValues>> Post_Values(ParkingValues values)
        {
            _context.Users.Add(values);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetValues", new { licensePlate = values.LicensePlate }, values);
        }

        [HttpDelete("{licensePlate}")]
        public async Task<ActionResult<ParkingValues>> Delete_values(int licensePlate)
        {
            var values = await _context.Users.FindAsync(licensePlate);
            if (values == null)
            {
                return NotFound();
            }

            _context.Users.Remove(values);
            await _context.SaveChangesAsync();

            return values;
        }

        [HttpPut("{licensePlate}")]
        public async Task<IActionResult> Put_Values(int licensePlate, ParkingValues values)
        {
            if (licensePlate != values.LicensePlate)
            {
                return BadRequest();
            }

            _context.Entry(values).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValuesExists(licensePlate))
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

        private bool ValuesExists(int licensePlate)
        {
            return _context.Users.Any(e => e.LicensePlate == licensePlate);
        }
    }
}