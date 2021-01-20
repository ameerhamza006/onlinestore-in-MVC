using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlinestore.Models;
using System.IO;
using System.Web.Security;

namespace onlinestore.Controllers
{
    [Authorize]
    public class VacanciesController : Controller
    {
        shoppingEntities obj = new shoppingEntities();
        // GET: Vacancies
        [Authorize(Roles ="Admin")]
        public ActionResult addvacanci()
        {
            return View();
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult addvacanci(vacanci v, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                var i = Path.Combine(Server.MapPath("~/vacancieimage/"), Path.GetFileName(img.FileName));
                img.SaveAs(i);

                v.v_img = img.FileName;

                obj.vacancis.Add(v);
                obj.SaveChanges();
                ModelState.Clear();
                return View();
            }
            return View();
        }

        public ActionResult vacandetail()
        {
            return View(obj.vacancis.ToList());
        }
        [HttpPost]
        public ActionResult vacandetail(DateTime date)
        {
            ViewBag.date = date;
            var d = obj.vacancis.Where(a => a.v_last_date == date).ToList();
            return View(d);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult vacandelete( int? id)
        {
            var d = obj.vacancis.Find(id);
            obj.vacancis.Remove(d);
            obj.SaveChanges();
            TempData["deletvacan"] = "Delete Succesfully";
            return RedirectToAction("vacandetail","Vacancies");
        }
        [Authorize(Roles ="Admin")]
        public ActionResult vacanedit(int? id)
        {
           
            return View(obj.vacancis.Find(id));
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public ActionResult vacanedit(int? id , vacanci v)
        {
            var e = obj.vacancis.Where(a => a.v_id == id).FirstOrDefault();
            e.v_name = v.v_name;
            e.v_discrip = v.v_discrip;
            e.v_last_date = v.v_last_date;
            e.v_img = v.v_img;
            Session["vacanimg"] = e.v_img;
            obj.SaveChanges();
            return RedirectToAction("vacandetail", "Vacancies");
        }
        
        public ActionResult jobdetail()
        {

            return View(obj.jobs.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult jobdelete(int? id)
        {
            var d = obj.jobs.Find(id);
            obj.jobs.Remove(d);
            TempData["deletj"] = "Delete Succesfully";
            obj.SaveChanges();
            return RedirectToAction("jobdetail","Vacancies");
        }
    }





}