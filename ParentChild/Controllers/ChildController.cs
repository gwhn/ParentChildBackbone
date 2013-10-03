using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ParentChild.Models;

namespace ParentChild.Controllers
{
    public class ChildController : ApiController
    {
        private readonly ParentChildContext _db = new ParentChildContext();

        // GET api/Child/4
        public IEnumerable<Child> GetChildren()
        {
            return _db.Children.AsEnumerable();
        }

        // GET api/Child/5
        public Child GetChild(int id)
        {
            Child child = _db.Children.Find(id);
            if (child == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return child;
        }

        // PUT api/Child/5
        public HttpResponseMessage PutChild(int id, Child child)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != child.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _db.Entry(child).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, child);
        }

        // POST api/Child
        public HttpResponseMessage PostChild(Child child)
        {
            if (ModelState.IsValid)
            {
                _db.Children.Add(child);
                _db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, child);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = child.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Child/5
        public HttpResponseMessage DeleteChild(int id)
        {
            Child child = _db.Children.Find(id);
            if (child == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _db.Children.Remove(child);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, child);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}