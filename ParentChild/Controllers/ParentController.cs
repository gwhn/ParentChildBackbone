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
    public class ParentController : ApiController
    {
        private readonly ParentChildContext _db = new ParentChildContext();

        // GET api/Parent
        public IEnumerable<Parent> GetParents()
        {
            return _db.Parents.AsEnumerable();
        }

        // GET api/Parent/5
        public Parent GetParent(int id)
        {
            Parent parent = _db.Parents
                               .Include(x => x.Children)
                               .SingleOrDefault(x => x.Id == id);
            if (parent == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return parent;
        }

        // PUT api/Parent/5
        public HttpResponseMessage PutParent(int id, Parent parent)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != parent.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _db.Entry(parent).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Parent
        public HttpResponseMessage PostParent(Parent parent)
        {
            if (ModelState.IsValid)
            {
                _db.Parents.Add(parent);
                _db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, parent);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = parent.Id }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Parent/5
        public HttpResponseMessage DeleteParent(int id)
        {
            Parent parent = _db.Parents.Find(id);
            if (parent == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _db.Parents.Remove(parent);

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, parent);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}