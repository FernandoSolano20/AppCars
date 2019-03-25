using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Cars.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cars.Controllers
{
    public class AgencyController : ApiController
    {
        private CarsDBContext _db;
        public AgencyController()
        {
            _db = new CarsDBContext();
            _db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Agency
        public IEnumerable<Agency> GetAllAgencies()
        {
            return _db.Agencies;
        }

        // GET: api/Agency/5
        public Agency GetAgency(int id)
        {
            var agency = _db.Agencies.Find(id);
            if(agency == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return agency;
        }

        // POST: api/Agency
        public HttpResponseMessage PostAgency(Agency agency)
        {
            if (ModelState.IsValid)
            {
                _db.Agencies.Add(agency);
                _db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, agency);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = agency.ID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // PUT: api/Agency/5
        public HttpResponseMessage PutAgency(int id, Agency agency)
        {
            if (ModelState.IsValid && id == agency.ID)
            {
                _db.Entry(agency).State = EntityState.Modified;

                try
                {
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, agency);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE: api/Agency/5
        public HttpResponseMessage DeleteAgency(int id)
        {
            Agency agency = _db.Agencies.Find(id);
            if (agency == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _db.Agencies.Remove(agency);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, agency);
        }
    }
}
