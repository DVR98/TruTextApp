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
    public class LoginPagesController : ApiController
    {
        private TruTexts_CustomersEntities db = new TruTexts_CustomersEntities();

        // GET: api/LoginPages
        public IQueryable<LoginPage> GetLoginPages()
        {
            return db.LoginPages;
        }

        // GET: api/LoginPages/5
        [ResponseType(typeof(LoginPage))]
        public async Task<IHttpActionResult> GetLoginPage(decimal id)
        {
            LoginPage loginPage = await db.LoginPages.FindAsync(id);
            if (loginPage == null)
            {
                return NotFound();
            }

            return Ok(loginPage);
        }

        // PUT: api/LoginPages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLoginPage(decimal id, LoginPage loginPage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loginPage.ID)
            {
                return BadRequest();
            }

            db.Entry(loginPage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginPageExists(id))
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

        // POST: api/LoginPages
        [ResponseType(typeof(LoginPage))]
        public async Task<IHttpActionResult> PostLoginPage(LoginPage loginPage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LoginPages.Add(loginPage);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoginPageExists(loginPage.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = loginPage.ID }, loginPage);
        }

        // DELETE: api/LoginPages/5
        [ResponseType(typeof(LoginPage))]
        public async Task<IHttpActionResult> DeleteLoginPage(decimal id)
        {
            LoginPage loginPage = await db.LoginPages.FindAsync(id);
            if (loginPage == null)
            {
                return NotFound();
            }

            db.LoginPages.Remove(loginPage);
            await db.SaveChangesAsync();

            return Ok(loginPage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoginPageExists(decimal id)
        {
            return db.LoginPages.Count(e => e.ID == id) > 0;
        }
    }
}