
using Watch.Models.DTO;
using Watch.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Computers.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private WatchEntities db = new WatchEntities();
        // GET: Admin/User
        public ActionResult Index()
        {
            var lstOrder = db.Orders.ToList();
            var model = new List<UserDTO>();
            foreach (var item in db.Users.ToList())
            {
                
                    var user = new UserDTO();
                    user.Fullname = item.Fullname;
                    user.Address = item.Address;
                    user.Phone = item.Phone;
                    user.Email = item.Email;
                    user.User_ID = item.ID;
                    user.Status = item.Status;
                    
                    user.TotalMoney = 0;
                    user.TotalQuantity = 0;
                    foreach (var jtem in lstOrder.Where(x => x.User_ID == item.ID).OrderBy(x => x.CreatedDate).ToList())
                    {
                        user.TotalMoney += jtem.TotalMoney;
                        user.TotalQuantity += jtem.TotalQuantity;
                        user.LastDayBought = jtem.CreatedDate;
                    }

                    model.Add(user);
            }
            return View(model.OrderByDescending(x => x.TotalMoney).ToList());
        }

        


        //Xóa tài khoản
        public JsonResult Delete(long ID)
        {
            var user = db.Users.Find(ID);
            db.Users.Remove(user);
            db.SaveChanges();
            return Json(new
            {
                status = true
            });
        }

        public JsonResult changeStatus(long ID)
        {
            var user = db.Users.Find(ID);
            if (user.Status == true)
                user.Status = false;
            else
                user.Status = true;
            db.SaveChanges();
            return Json(new
            {
                status = true
            });
        }
        
    }
}