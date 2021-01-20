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
    public class AdminController : Controller
    {
        shoppingEntities obj = new shoppingEntities();

        // GET: Admin
        
        public ActionResult dashboard()
        {
            if (Session["iid"] != null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("login", "Home");
            }
        }
        public ActionResult logout()
        {
            Session["iid"] = null;
            Session.Remove("iid");
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();



            return RedirectToAction("login", "Home");

        }

        
        public ActionResult order_detail()
        {
            ViewBag.ca = obj.carts.ToList();
            
            return View(obj.billinggs.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult deleteorder(int? id)
        {
            var r = obj.carts.Find(id);
            obj.carts.Remove(r);
            obj.SaveChanges();
            TempData["delorder"] = " Your Order Succesfully Delete";
            return RedirectToAction("order_detail");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult deletebilling(int? id)
        {
            var b = obj.billinggs.Find(id);
            obj.billinggs.Remove(b);
            obj.SaveChanges();
            TempData["delbil"] = "Succesfully Delete";
            return RedirectToAction("order_detail");
        }
        
        public ActionResult searchtable()
        {
            
            
            return View(obj.billinggs.ToList());
        }
        [HttpPost]
        public ActionResult searchtable(DateTime? start , DateTime? end)
        {
            ViewBag.start = start;
            ViewBag.end = end;
            var s = from a in obj.billinggs where a.bill_date >= start && a.bill_date <= end select a;

            return View(s);
        }

        public ActionResult daily()
        {
            return View(obj.billinggs.ToList());
        }
        [HttpPost]
        public ActionResult daily(string name)
        {

            var a = obj.billinggs.Where(v => v.bill_firstname == name).ToList();
            return View(a);
        }

        public ActionResult contactlist()
        {

            return View(obj.contacts.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult contactdelete( int id)
        {
            var f = obj.contacts.Find(id);
            obj.contacts.Remove(f);
            obj.SaveChanges();
            TempData["delcontact"] = "Delete Succesfully!";
            return RedirectToAction("contactlist");
        }

        public ActionResult commentdetail()
        {

            return View(obj.comments.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult commentdelete(int? id)
        {
           var d = obj.comments.Find(id);
            obj.comments.Remove(d);
            obj.SaveChanges();
            TempData["delcomment"] = "delete Succesfully";
            return RedirectToAction("commentdetail","Admin");
        }

       

    }
}



