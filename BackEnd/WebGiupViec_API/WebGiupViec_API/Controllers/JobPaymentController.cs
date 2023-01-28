using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebGiupViec_API.Models;

namespace WebGiupViec_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPaymentController : ControllerBase
    {
        private readonly WEBGIUPVIECContext _context;

        public JobPaymentController(WEBGIUPVIECContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPayment>>> GetJobPayments()
        {
            return await _context.JobPayments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobPayment>> GetJobPayment(int id)
        {
            var jobPayment = await _context.JobPayments.FindAsync(id);

            if (jobPayment == null)
            {
                return NotFound();
            }

            return jobPayment;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobPayment(int id, JobPayment jobPayment)
        {
            if (id != jobPayment.JobPaymentId)
            {
                return BadRequest();
            }

            _context.Entry(jobPayment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobPaymentExists(id))
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

        [HttpPost]
        public async Task<ActionResult<JobPayment>> PostJobPayment(JobPayment jobPayment)
        {
            _context.JobPayments.Add(jobPayment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobPayment", new { id = jobPayment.JobPaymentId }, jobPayment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPayment(int id)
        {
            var jobPayment = await _context.JobPayments.FindAsync(id);
            if (jobPayment == null)
            {
                return NotFound();
            }

            _context.JobPayments.Remove(jobPayment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobPaymentExists(int id)
        {
            return _context.JobPayments.Any(e => e.JobPaymentId == id);
        }
    }
}
