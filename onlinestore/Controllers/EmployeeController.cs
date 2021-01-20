using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlinestore.Models;
using System.IO;
using System.Web.Security;

namespace onlinestore.Controllers
{   [Authorize]
    public class EmployeeController : Controller
    {
        shoppingEntities obj = new shoppingEntities();
        // GET: Employee
        [Authorize(Roles ="Admin")]
        public ActionResult addemp()
        {
            ViewBag.fk_job = new SelectList(obj.jobs.ToList() , "j_id" , "j_fullname");
            
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult addemp(login e, HttpPostedFileBase pic)
        {
            if (ModelState.IsValid)
            {
                var empic = Path.Combine(Server.MapPath("~/empimages/"), Path.GetFileName(pic.FileName));
                pic.SaveAs(empic);

                e.emp_img = pic.FileName;

                ViewBag.fk_job = new SelectList(obj.jobs.ToList(), "j_id", "j_fullname", e.fk_job);

                obj.logins.Add(e);
               int a = obj.SaveChanges();
                if(a == 1)
                {
                    ViewBag.add = "Succesfully Added";
                    ModelState.Clear();
                    return RedirectToAction("empdetail", "Employee");
                }
                else
                {
                    ViewBag.eror = "Employee Not Added Please Add Again";
                }
               
            }
            
            return View();
           
        }

        public ActionResult empdetail()
        {

            return View(obj.logins.ToList());
        }
        [HttpPost]
        public ActionResult empdetail(string name)
        {
            ViewBag.name = name;
            var s = obj.logins.Where(a => a.job.j_fullname == name  || a.job.j_email == name || a.job.j_qualification == name || a.job.j_gander == name).ToList();
            return View(s);
        }

        public ActionResult empsingle( int? id)
        {
            var s = obj.logins.Find(id);
            return View(s);
        }

        public ActionResult Userdetail()
        {
            
            return View(obj.logins.ToList());
        }
        [HttpPost]
        public ActionResult Userdetail(string name)
        {
            var s = obj.logins.Where(a => a.log_name == name || a.log_city == name || a.log_email == name ).ToList();
            return View(s);
        }



        [Authorize(Roles = "Admin")]
        public ActionResult empdelete(int? id)
        {
            var l = obj.logins.Find(id);
            obj.logins.Remove(l);
            obj.SaveChanges();
            TempData["empdelet"] = "Succesfully Delete";
            return RedirectToAction("empdetail","Employee");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult userdelete(int? id)
        {
            var l = obj.logins.Find(id);
            obj.logins.Remove(l);
            obj.SaveChanges();
            TempData["userdelet"] = "Succesfully Delete";
            return RedirectToAction("Userdetail", "Employee");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult empedit(int? id)
        {

            return View(obj.logins.Find(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult empedit(int? id , login l)
        {
            
           
            var e = obj.logins.Where(a => a.log_id == id).FirstOrDefault();
            e.emp_client = l.emp_client;
          
            e.log_status = l.log_status;
            e.log_password = l.log_password;
            e.job.j_fullname = l.job.j_fullname;
            e.job.j_email = l.job.j_email;
            e.job.j_number = l.job.j_number;
            e.job.j_city = l.job.j_city;
            e.job.j_country = l.job.j_country;
            e.job.j_address = l.job.j_address;
            e.job.j_qualification = l.job.j_qualification;
            e.job.j_experince = l.job.j_experince;
            e.job.j_gander = l.job.j_gander;
            e.job.j_age = l.job.j_age;
            e.log_name = l.log_name;
            e.log_email = l.log_email;
            e.log_number = l.log_number;
            e.log_role = l.log_role;
            e.log_password = l.log_password;
            e.log_city = l.log_city;
            e.log_address = l.log_address;
            obj.SaveChanges();
            
            return RedirectToAction("empdetail","Employee");
        }

       
        
        public ActionResult status()
        {
            return View(obj.logins.ToList());
        }
        
        [HttpPost]
        public ActionResult status( string name)
        {
            ViewBag.name = name;
            var s = obj.logins.Where(a => a.job.j_fullname == name || a.log_name == name || a.log_email == name).ToList();
            return View(s);
        }
        
        public ActionResult active(int? id)
        {
            return View(obj.logins.Find(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult active(int? id , login e)
        {
            var b = obj.logins.Where(a => a.log_id == id).FirstOrDefault();
            b.log_status = e.log_status;
            obj.SaveChanges();
            return RedirectToAction("status","Employee");
        }
    }
}