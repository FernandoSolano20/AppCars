using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Cars.DTOs;
using Cars.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Cars.Controllers
{
    [RoutePrefix("api/brand")]
    public class BrandController : ApiController
    {
        private CarsDBContext _db;
        private static readonly Expression<Func<Brand, BrandDto>> AsBrandDto =
            x => new BrandDto
            {
                BrandID = x.BrandID,
                BrandName = x.BrandName,
                NameAgency = x.Agency.NameAgency
            };
        public BrandController()
        {
            _db = new CarsDBContext();
            _db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/Brand
        [Route("", Name ="GetBrandByAgency")]
        public IQueryable<BrandDto> GetAllBrands()
        {
            return _db.Brands.Include(b => b.Agency.NameAgency).Select(AsBrandDto);
        }

        [HttpGet, Route("~/api/brand/agency/{agencyID:int}")]
        public IQueryable<BrandDto> GetAllBrandsByAgency(int agencyID)
        {
            return _db.Brands.Include(b => b.Agency.NameAgency).Where(b => b.AgencyID == agencyID).Select(AsBrandDto);
        }

        // GET api/brand/5
        [HttpGet, Route("{brandID:int}")]
        public Brand GetBrand(int brandID)
        {
            var brand = _db.Brands.Find(brandID);
            if (brand == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return brand;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost, Route("addBrand")]
        public HttpResponseMessage PostBrand(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _db.Brands.Add(brand);
                _db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, brand);
                response.Headers.Location = new Uri(Url.Link("GetBrandByAgency", new { id = brand.BrandID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut, Route("updateBrand/{brandID:int}")]
        public HttpResponseMessage PutBrand(int brandID, Brand brand)
        {
            if (ModelState.IsValid && brandID == brand.BrandID)
            {
                _db.Entry(brand).State = EntityState.Modified;

                try
                {
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, brand);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete, Route("deleteBrand/{brandID:int}")]
        public HttpResponseMessage DeleteBrand(int brandID)
        {
            Brand brand = _db.Brands.Find(brandID);
            if (brand == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _db.Brands.Remove(brand);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, brand);
        }
    }
}
