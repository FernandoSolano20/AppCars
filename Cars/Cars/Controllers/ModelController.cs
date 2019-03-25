using Cars.DTOs;
using Cars.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cars.Controllers
{
    [RoutePrefix("api/model")]
    public class ModelController : ApiController
    {
        private CarsDBContext _db;
        private static readonly Expression<Func<Model, ModelDto>> AsModelDto =
            x => new ModelDto
            {
                ModelID = x.ModelID,
                ModelName = x.ModelName,
                BrandName = x.Brand.BrandName
            };
        public ModelController()
        {
            _db = new CarsDBContext();
            _db.Configuration.ProxyCreationEnabled = false;
        }

        [Route("", Name = "GetModelsByBrand")]
        public IQueryable<ModelDto> GetAllBrands()
        {
            return _db.Models.Include(b => b.Brand.BrandName).Select(AsModelDto);
        }

        [HttpGet, Route("~/api/model/brand/{brandID:int}")]
        public IQueryable<ModelDto> GetAllModelsByBrand(int brandID)
        {
            return _db.Models.Include(b => b.Brand.BrandName).Where(b => b.BrandID == brandID).Select(AsModelDto);
        }

        [HttpGet, Route("{modelID:int}")]
        public Model GetModel(int modelID)
        {
            var model = _db.Models.Find(modelID);
            if (model == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return model;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost, Route("addModel")]
        public HttpResponseMessage PostModel(Model model)
        {
            if (ModelState.IsValid)
            {
                _db.Models.Add(model);
                _db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
                response.Headers.Location = new Uri(Url.Link("GetModelsByBrand", new { id = model.ModelID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpPut, Route("updateModel/{modelID:int}")]
        public HttpResponseMessage PutModel(int modelID, Model model)
        {
            if (ModelState.IsValid && modelID == model.ModelID)
            {
                _db.Entry(model).State = EntityState.Modified;

                try
                {
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, model);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete, Route("deleteModel/{modelID:int}")]
        public HttpResponseMessage DeleteModel(int modelID)
        {
            Model model = _db.Models.Find(modelID);
            if (model == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _db.Models.Remove(model);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
}
