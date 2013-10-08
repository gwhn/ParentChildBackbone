using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParentChild.Models;

namespace ParentChild.Controllers
{
    public class HomeController : Controller
    {
        private readonly ParentChildContext _db = new ParentChildContext();

        public ActionResult Index()
        {
            return View(
                _db.Parents
                   .Include(x => x.Children)
                   .Take(ParentController.RecordsPerPage)
                   .AsEnumerable()
                );
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}
