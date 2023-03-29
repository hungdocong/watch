using Watch.Models.DTO;
using Watch.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Computers.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private WatchEntities db = new WatchEntities();
        // GET: Admin/Login
        //[CustomRoleProvider(Type = 1)]
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List()
        {
            var model = db.Admins.Where(x => x.Account != "admin").OrderByDescending(x => x.ID).ToList();
            ViewBag.lstPhanQuyen = db.Roles.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult frmLogin(User model)
        {
            //var pass_encrypt = new MD5().Encrypt_MD5(model.Password);
            if (db.Admins.Count(x => x.Account == model.Account && x.Password == model.Password) > 0)
            {
                var result = db.Admins.SingleOrDefault(x => x.Account == model.Account && x.Password == model.Password);
                if(result.Status == false)
                {
                    TempData["error"] = "Tài khoản đăng nhập đã bị khóa.";
                    return Redirect("/admin/login");
                }
                else
                {
                    Update_LastLogin(result.ID);
                    Session["admin"] = result;
                    return Redirect("/admin/home");
                }
                
            }
            else
            {
                TempData["error"] = "Tài khoản hoặc mật khẩu không chính xác";
                return Redirect("/admin/login");
            }
        }

        public ActionResult Logout()
        {
            Session["admin"] = null;
            return Redirect("/admin/login");
        }

        //Cập nhật trạng thái
        public JsonResult changeStatus(long ID)
        {
            var user = db.Admins.Find(ID);
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

        //Cập nhật phân quyền
        public JsonResult UpdateRole(long ID, int RoleID)
        {
            var user = db.Admins.Find(ID);
            user.RoleID = RoleID;
            db.SaveChanges();
            return Json(new
            {
                status = true
            });
        }

        //Xóa tài khoản
        public JsonResult Delete(long ID)
        {
            var user = db.Admins.Find(ID);
            //Xóa file cũ
            if (user.Image != null)
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Assets/Client/img/admin"), user.Image));
            db.Admins.Remove(user);
            db.SaveChanges();
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public ActionResult frmAdd(Watch.Models.EF.Admin entity, HttpPostedFileBase Image)
        {
            try
            {
                //Thêm hình ảnh
                var path = Path.Combine(Server.MapPath("~/Assets/Client/img/admin"), Image.FileName);
                if (System.IO.File.Exists(path))
                {
                    string extensionName = Path.GetExtension(Image.FileName);
                    string filename = Image.FileName + DateTime.Now.ToString("hhmmssddMMyyyy") + extensionName;
                    path = Path.Combine(Server.MapPath("~/Assets/Client/img/admin"), filename);
                    Image.SaveAs(path);
                    entity.Image = filename;
                }
                else
                {
                    Image.SaveAs(path);
                    entity.Image = Image.FileName;
                }
                //entity.Password = new MD5().Encrypt_MD5(entity.Password);
                db.Admins.Add(entity);
                db.SaveChanges();
                TempData["massage"] = "Thêm truy cập thành công";
                TempData["alert"] = "alert-success";
                return Redirect("/admin/login/list");

            }
            catch
            {
                TempData["massage"] = "Thêm truy cập KHÔNG thành công";
                TempData["alert"] = "alert-danger";

                return Redirect("/admin/login/list");
            }
        }

        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult frmUpdate(Watch.Models.EF.Admin entity, HttpPostedFileBase Image)
        {
            try
            {
                var prv = db.Admins.Find(entity.ID);
                prv.Fullname = entity.Fullname;

                if (Image != null && prv.Image != Image.FileName)
                {
                    //Xóa file cũ
                    if (prv.Image != null)
                        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Assets/Client/img/admin"), prv.Image));
                    //Thêm hình ảnh
                    var path = Path.Combine(Server.MapPath("~/Assets/Client/img/admin"), Image.FileName);
                    if (System.IO.File.Exists(path))
                    {
                        string extensionName = Path.GetExtension(Image.FileName);
                        string filename = Image.FileName + DateTime.Now.ToString("hhmmssddMMyyyy") + extensionName;
                        path = Path.Combine(Server.MapPath("~/Assets/Client/img/bradminand"), filename);
                        Image.SaveAs(path);
                        prv.Image = filename;
                    }
                    else
                    {
                        Image.SaveAs(path);
                        prv.Image = Image.FileName;
                    }
                }

                db.SaveChanges();
                TempData["message"] = "Cập nhật thông tin thành công";
                TempData["alert"] = "alert-success";
                return Redirect("/admin/login/list");
            }
            catch
            {
                TempData["message"] = "Cập nhật thông tin KHÔNG thành công";
                TempData["alert"] = "alert-danger";
                return Redirect("/admin/login/list");
            }
        }

        public ActionResult ChangePass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult frmchangePass(string Old_Pass, string New_Pass)
        {
            var admin = Session["admin"] as Watch.Models.EF.Admin;
            var user = db.Admins.Find(admin.ID);
            if (user.Password.Trim() == Old_Pass)
            {
                user.Password = New_Pass.Trim();
                db.SaveChanges();
                Session["admin"] = null;
                TempData["error"] = "Bạn vui lòng đăng nhập lại sau khi đổi mật khẩu.";
                return Redirect("/admin/login");
            }
            else
            {
                TempData["error"] = "Mật khẩu cũ không đúng, vui lòng nhập lại.";
                return Redirect("/admin/admin/changePass");
            }

        }

        public void Update_LastLogin(long admin_id)
        {
            var admin = db.Admins.Find(admin_id);
            admin.LastLogin = DateTime.Now;
            db.SaveChanges();
        }
    }
}