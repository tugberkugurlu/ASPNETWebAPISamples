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
    public class AccommodationPropertiesController : ApiController
    {
        private ReservationContext db = new ReservationContext();

        // GET api/AccommodationProperties
        public IQueryable<AccommodationProperty> GetAccommodationProperties()
        {
            return db.AccommodationProperties;
        }

        // GET api/AccommodationProperties/5
        [ResponseType(typeof(AccommodationProperty))]
        public async Task<IHttpActionResult> GetAccommodationProperty(int id)
        {
            AccommodationProperty accommodationproperty = await db.AccommodationProperties.FindAsync(id);
            if (accommodationproperty == null)
            {
                return NotFound();
            }

            return Ok(accommodationproperty);
        }

        // PUT api/AccommodationProperties/5
        public async Task<IHttpActionResult> PutAccommodationProperty(int id, AccommodationProperty accommodationproperty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != accommodationproperty.Id)
            {
                return BadRequest();
            }

            db.Entry(accommodationproperty).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccommodationPropertyExists(id))
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

        // POST api/AccommodationProperties
        [ResponseType(typeof(AccommodationProperty))]
        public async Task<IHttpActionResult> PostAccommodationProperty(AccommodationProperty accommodationproperty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AccommodationProperties.Add(accommodationproperty);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = accommodationproperty.Id }, accommodationproperty);
        }

        // DELETE api/AccommodationProperties/5
        [ResponseType(typeof(AccommodationProperty))]
        public async Task<IHttpActionResult> DeleteAccommodationProperty(int id)
        {
            AccommodationProperty accommodationproperty = await db.AccommodationProperties.FindAsync(id);
            if (accommodationproperty == null)
            {
                return NotFound();
            }

            db.AccommodationProperties.Remove(accommodationproperty);
            await db.SaveChangesAsync();

            return Ok(accommodationproperty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AccommodationPropertyExists(int id)
        {
            return db.AccommodationProperties.Count(e => e.Id == id) > 0;
        }
    }
}