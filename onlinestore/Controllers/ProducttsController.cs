using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using onlinestore.Models;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Web.Security;

namespace onlinestore.Controllers
{
    [Authorize]
    public class ProducttsController : Controller
    {
        shoppingEntities obj = new shoppingEntities();
        // GET: Productts
        [Authorize(Roles ="Admin")]
        public ActionResult addPro(int? id)
        {
            //ViewBag.pro_fk_cat2 = new SelectList(obj.category2.Where(a => a.cat2_id == id).OrderByDescending(k => k.cat2_fk_cat1).ToList());
            ViewBag.pro_fk_cat1 = new SelectList(obj.category1.ToList(), "cat1_id", "cat1_name");
            ViewBag.pro_fk_cat2 = new SelectList(obj.category2.ToList(), "cat2_id", "cat2_name");
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult addPro(product p, HttpPostedFileBase imgone, HttpPostedFileBase imgtwo, HttpPostedFileBase imgthree ,  int? id )
        {

            if (ModelState.IsValid)
            {
                var imageone = Path.Combine(Server.MapPath("~/productimg/"), Path.GetFileName(imgone.FileName));
                var imagetwo = Path.Combine(Server.MapPath("~/productimg/"), Path.GetFileName(imgtwo.FileName));
                var imagethree = Path.Combine(Server.MapPath("~/productimg/"), Path.GetFileName(imgthree.FileName));
                imgone.SaveAs(imageone);
                imgtwo.SaveAs(imagetwo);
                imgthree.SaveAs(imagethree);
                p.pro_img = imgone.FileName;
                p.pro_img2 = imgtwo.FileName;
                p.pro_img3 = imgthree.FileName;
                //ViewBag.pro_fk_cat2 = new SelectList(obj.category2.Where(a => a.cat2_id == id).OrderByDescending(k => k.cat2_fk_cat1).ToList(), "cat2_id", "cat2_name");
                ViewBag.pro_fk_cat1 = new SelectList(obj.category1.ToList(), "cat1_id", "cat1_name", p.pro_fk_cat1);
                ViewBag.pro_fk_cat2 = new SelectList(obj.category2.ToList(), "cat2_id", "cat2_name", p.pro_fk_cat2);
                obj.products.Add(p);
                obj.SaveChanges();
                ModelState.Clear();



                return RedirectToAction("prodetail", "Productts");
            }
            return View();
        }

        public ActionResult prodetail()
        {
           
            return View(obj.products.ToList());
        }
        [HttpPost]
        public ActionResult prodetail(string name)
        {
            ViewBag.name = name;
            var c = obj.products.Where(a => a.category1.cat1_name == name || a.pro_brand == name || a.pro_title == name ).ToList();
            return View(c);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult prodelete(int? id)
        {
            var d = obj.products.Find(id);
            obj.products.Remove(d);
            obj.SaveChanges();
            TempData["deletpro"] = "Delete Succesfully";
            return RedirectToAction("prodetail","Productts");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult proedit(int? id)
        {
            var e = obj.products.Find(id);
            ViewBag.pro_fk_cat1 = new SelectList(obj.category1.ToList(), "cat1_id", "cat1_name");
            ViewBag.pro_fk_cat2 = new SelectList(obj.category2.ToList(), "cat2_id", "cat2_name");
            return View(e);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult proedit(int? id , product p)
        {
          

            var e = obj.products.Where(a => a.pro_id == id).FirstOrDefault();
            e.pro_title = p.pro_title;
            e.pro_oldprice = p.pro_oldprice;
            e.pro_price = p.pro_price;
            e.pro_brand = p.pro_brand;
            e.pro_qty = p.pro_qty;
           
            e.pro_fk_cat1 = p.pro_fk_cat1;
            e.pro_fk_cat2 = p.pro_fk_cat2;
            ViewBag.pro_fk_cat1 = new SelectList(obj.category1.ToList(), "cat1_id", "cat1_name", p.pro_fk_cat1);
            ViewBag.pro_fk_cat2 = new SelectList(obj.category2.ToList(), "cat2_id", "cat2_name", p.pro_fk_cat2);

            obj.SaveChanges();
            return RedirectToAction("prodetail", "Productts");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult proimgedit(int? id)
        {
              
            return View(obj.products.Find(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult proimgedit(int? id, product p , HttpPostedFileBase pic)
        {
            var image = Path.Combine(Server.MapPath("~/productimg/"), Path.GetFileName(pic.FileName));
            pic.SaveAs(image);

            var l = obj.products.Where(a => a.pro_id == id).FirstOrDefault();
            l.pro_title = p.pro_title;
            l.pro_img = pic.FileName;
            obj.SaveChanges();
            return RedirectToAction("prodetail","Productts");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult proimg2edit(int? id)
        {

            return View(obj.products.Find(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult proimg2edit(int? id, product p, HttpPostedFileBase pictwo)
        {
            var image2 = Path.Combine(Server.MapPath("~/productimg/"), Path.GetFileName(pictwo.FileName));
            pictwo.SaveAs(image2);

            var l = obj.products.Where(a => a.pro_id == id).FirstOrDefault();
            l.pro_title = p.pro_title;
            l.pro_img2 = pictwo.FileName;
            obj.SaveChanges();
            return RedirectToAction("prodetail", "Productts");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult proimg3edit(int? id)
        {

            return View(obj.products.Find(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult proimg3edit(int? id, product p, HttpPostedFileBase picthree)
        {
            var image = Path.Combine(Server.MapPath("~/productimg/"), Path.GetFileName(picthree.FileName));
            picthree.SaveAs(image);

            var l = obj.products.Where(a => a.pro_id == id).FirstOrDefault();
            l.pro_title = p.pro_title;
            l.pro_img3 = picthree.FileName;
            obj.SaveChanges();
            return RedirectToAction("prodetail", "Productts");
        }




    }
}








