﻿using System;
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
    public class JobOfferController : ControllerBase
    {
        private readonly WEBGIUPVIECContext _context;

        public JobOfferController(WEBGIUPVIECContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobOffer>>> GetJobOffers()
        {
            return await _context.JobOffers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobOffer>> GetJobOffer(int id)
        {
            var jobOffer = await _context.JobOffers.FindAsync(id);

            if (jobOffer == null)
            {
                return NotFound();
            }

            return jobOffer;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobOffer(int id, JobOffer jobOffer)
        {
            if (id != jobOffer.JobOfferId)
            {
                return BadRequest();
            }

            _context.Entry(jobOffer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobOfferExists(id))
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
        public async Task<ActionResult<JobOffer>> PostJobOffer(JobOffer jobOffer)
        {
            _context.JobOffers.Add(jobOffer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJobOffer", new { id = jobOffer.JobOfferId }, jobOffer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobOffer(int id)
        {
            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer == null)
            {
                return NotFound();
            }

            _context.JobOffers.Remove(jobOffer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobOfferExists(int id)
        {
            return _context.JobOffers.Any(e => e.JobOfferId == id);
        }
    }
}
