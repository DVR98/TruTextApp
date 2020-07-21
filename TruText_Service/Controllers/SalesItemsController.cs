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
using TruText_Service.Models;

namespace TruText_Service.Controllers
{
    public class SalesItemsController : ApiController
    {
        private TruTexts_CustomersEntities db = new TruTexts_CustomersEntities();

        // GET: api/SalesItems
        public IQueryable<SalesItem> GetSalesItems()
        {
            return db.SalesItems;
        }

        // GET: api/SalesItems/5
        [ResponseType(typeof(SalesItem))]
        public async Task<IHttpActionResult> GetSalesItem(int id)
        {
            SalesItem salesItem = await db.SalesItems.FindAsync(id);
            if (salesItem == null)
            {
                return NotFound();
            }

            return Ok(salesItem);
        }

        // PUT: api/SalesItems/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSalesItem(int id, SalesItem salesItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != salesItem.SalesItemID)
            {
                return BadRequest();
            }

            db.Entry(salesItem).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesItemExists(id))
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

        // POST: api/SalesItems
        [ResponseType(typeof(SalesItem))]
        public async Task<IHttpActionResult> PostSalesItem(SalesItem salesItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SalesItems.Add(salesItem);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = salesItem.SalesItemID }, salesItem);
        }

        // DELETE: api/SalesItems/5
        [ResponseType(typeof(SalesItem))]
        public async Task<IHttpActionResult> DeleteSalesItem(int id)
        {
            SalesItem salesItem = await db.SalesItems.FindAsync(id);
            if (salesItem == null)
            {
                return NotFound();
            }

            db.SalesItems.Remove(salesItem);
            await db.SaveChangesAsync();

            return Ok(salesItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SalesItemExists(int id)
        {
            return db.SalesItems.Count(e => e.SalesItemID == id) > 0;
        }
    }
}