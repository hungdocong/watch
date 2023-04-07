using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Watch.Models.EF;

namespace Watch.Controllers
{
    public class HomeController : Controller
    {
        private WatchEntities db = new WatchEntities();

        public ActionResult Index()
        {
            ViewBag.lstProduct = db.Products.ToList();
            return View();
        }
    
       
    }
}