using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CacheCowSample.Models;

namespace CacheCowSample.Controllers
{
    public class DestinationsController : ApiController
    {
        private ReservationContext db = new ReservationContext();

        // GET api/Destinations
        public IQueryable<Destination> GetDestinations()
        {
            return db.Destinations;
        }

        // GET api/Destinations/5
        [ResponseType(typeof(Destination))]
        public async Task<IHttpActionResult> GetDestination(int id)
        {
            Destination destination = await db.Destinations.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }

            return Ok(destination);
        }

        // PUT api/Destinations/5
        public async Task<IHttpActionResult> PutDestination(int id, Destination destination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != destination.Id)
            {
                return BadRequest();
            }

            db.Entry(destination).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Destinations
        [ResponseType(typeof(Destination))]
        public async Task<IHttpActionResult> PostDestination(Destination destination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Destinations.Add(destination);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = destination.Id }, destination);
        }

        // DELETE api/Destinations/5
        [ResponseType(typeof(Destination))]
        public async Task<IHttpActionResult> DeleteDestination(int id)
        {
            Destination destination = await db.Destinations.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }

            db.Destinations.Remove(destination);
            await db.SaveChangesAsync();

            return Ok(destination);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DestinationExists(int id)
        {
            return db.Destinations.Count(e => e.Id == id) > 0;
        }
    }
}