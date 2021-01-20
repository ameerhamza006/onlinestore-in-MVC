using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlinestore.Models;
using System.Web.Security;

namespace onlinestore.Controllers
{
    [Authorize]
    public class CategorysController : Controller
    {
        shoppingEntities obj = new shoppingEntities();
        // GET: Categorys
        [Authorize(Roles = "Admin")]
        public ActionResult Category1()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Category1(category1 c)
        {
            if (ModelState.IsValid)
            {
                obj.category1.Add(c);
                obj.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("cat1list", "Categorys");
            }
            return View();
        }

        public ActionResult Cat1list()
        {
            return View(obj.category1.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult cat1delete(int id)
        {
            var c = obj.category1.Find(id);
            obj.category1.Remove(c);
            obj.SaveChanges();
            TempData["deletcat1"] = "Delete Succesfully";
            return RedirectToAction("Cat1list","Categorys");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult cat1edit(int id)
        {
            
            return View(obj.category1.Find(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult cat1edit(int id, category1 c)
        {
            var cat = obj.category1.Where(a => a.cat1_id == id).FirstOrDefault();
            cat.cat1_name = c.cat1_name;
            obj.SaveChanges();
            return RedirectToAction("Cat1list", "AllinOne");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult addcat2()
        {
            ViewBag.cat2_fk_cat1 = new SelectList(obj.category1.ToList(), "cat1_id", "cat1_name");
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult addcat2(category2 c)
        {
            ViewBag.cat2_fk_cat1 = new SelectList(obj.category1.ToList(), "cat1_id", "cat1_name",c.cat2_fk_cat1);
            obj.category2.Add(c);
            obj.SaveChanges();
            ModelState.Clear();
            return RedirectToAction("Cat2list", "Categorys");
        }

        public ActionResult Cat2list()
        {
            return View(obj.category2.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult cat2delete(int id)
        {
            var c = obj.category2.Find(id);
            obj.category2.Remove(c);
            obj.SaveChanges();
            TempData["deletcat2"] = "Delete Succesfully";
            return RedirectToAction("Cat2list", "Categorys");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult cat2edit(int id)
        {
            ViewBag.cat2_fk_cat1 = new SelectList(obj.category1.ToList(), "cat1_id", "cat1_name");
            return View(obj.category2.Find(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult cat2edit(int id, category2 c)
        {
            var cat = obj.category2.Where(a => a.cat2_id == id).FirstOrDefault();
            cat.cat2_name = c.cat2_name;
            cat.cat2_fk_cat1 = c.cat2_fk_cat1;
            ViewBag.cat2_fk_cat1 = new SelectList(obj.category1.ToList(), "cat1_id", "cat1_name", c.cat2_fk_cat1);
            obj.SaveChanges();
            return RedirectToAction("Cat2list", "Categorys");
        }

    }
}