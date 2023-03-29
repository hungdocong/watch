
using Watch.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Watch.Models.DTO;

namespace Computers.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private WatchEntities db = new WatchEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Export()
        {
            
            return View();
        }

        public ActionResult TotalSaleMonth()
        {
            var lstTotal = new List<TotalSale>();
            return Json(lstTotal, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CateSale()
        {
            var lstTotal = new List<TotalSale>();
            
            return Json(lstTotal, JsonRequestBehavior.AllowGet);

        }

      
    }
}