using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using onlinestore.Models;

namespace onlinestore.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        shoppingEntities obj = new shoppingEntities();
        public ActionResult home()
        {
           
                return View(obj.products.OrderByDescending(s => s.pro_id).ToList());
            

        }

        public ActionResult register()
        {
            return View();

        }
        [HttpPost]
        public ActionResult register(login r)
        {
            if(ModelState.IsValid)
            {
                var again = obj.logins.Where(a => a.log_email == r.log_email).FirstOrDefault();
                if(again != null )
                {
                    ViewBag.againreg = "This Email Is Already Register";
                    ModelState.Clear();
                }
                else
                {
                    obj.logins.Add(r);
                    obj.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("login");
                }
                return View();
            }
            ViewBag.registereror = "Your Registration cancelled!  Please Register Again";
            return View();


        }

        public ActionResult login()
        {
            return View();

        }
        
        [HttpPost]
        public ActionResult login(login lo)
        { 
            var l = obj.logins.Where(a => a.log_email == lo.log_email && a.log_password == lo.log_password).FirstOrDefault();
            
           
            
            if (l != null)
            {

                FormsAuthentication.SetAuthCookie(l.log_email, false);

               
                if(l.log_role == "Admin")
                {
                    if(l.log_status == true)
                    {
                        Session["iid"] = l.log_id;
                        Session["fullname"] = l.log_name;
                        Session["role"] = l.log_role;
                        Session["img"] = l.emp_img;

                        return RedirectToAction("dashboard", "Admin");

                    }
                    else
                    {
                        if(l.log_role =="Admin")
                        {
                            if (l.log_status == false)
                            {
                                ViewBag.blockadmin = "Your Account Disable";
                                return View();
                            }
                        }
                        ViewBag.msg = "You are not Approve Yet..Admin";
                        return View();
                    }

                }


                if(l.log_role == "Employee")
                {

                    if (l.log_status == true)
                    {
                        Session["iid"] = l.log_id;
                        Session["fullname"] = l.log_name;
                        Session["role"] = l.log_role;
                        Session["img"] = l.emp_img;
                        Session["password"] = l.log_password;
                        return RedirectToAction("dashboard", "Admin");
                    }
                    else
                    {
                        if(l.log_role == "Employee")
                        {
                            if (l.log_status == false)
                            {
                                ViewBag.blockemp = "Your Account Disable";
                                return View();
                            }
                        }
                        ViewBag.empmsg = "Yoe Are Not Approve Please Wait";
                        return View();
                    }
                }

                if(l.log_role == "User")
                {

                    Session["name"] = l.log_name;
                    Session["id"] = l.log_id;
                    return RedirectToAction("addtocart", "Home");
                }


                return View();
                //return RedirectToAction("login", "Home");
                 



            }
            else
            {
               

                ViewBag.errorlogin = "Please Check Your Email Or Password";
                return View();
            }
        }

        public ActionResult forget()
        {

            return View();

        }
        [HttpPost]
        public ActionResult forget(login l)
        {
            var log = obj.logins.Where(a => a.log_email == l.log_email && a.log_number == l.log_number).FirstOrDefault();
            if (log != null)
            {
                return RedirectToAction("change_password", "Home",new { id = log.log_id });
            }
            ViewBag.wrong = "Not Match Your Email Or Number";
            return View();

        }

        public ActionResult change_password(int id)
        {
            ViewBag.pas = id;
            return View();

        }
        [HttpPost]
        public ActionResult change_password(login l , int id)
        {
            var pas = obj.logins.Where(a => a.log_id == id).FirstOrDefault();
            pas.log_password = l.log_password;
            obj.SaveChanges();   
            return RedirectToAction("login","Home");

        }

        public ActionResult logout()
        {
            //FormsAuthentication.SignOut();
            Session["name"] = null;
            
            
            return RedirectToAction("login", "Home");
           
        }

        public ActionResult product(int? id)
        {

            ViewBag.p = obj.products.Where(a => a.pro_id == id).SingleOrDefault();
            return View();

        }

        public ActionResult categoryview1(int? id)
        {

            
            return View(obj.products.Where(a => a.pro_fk_cat1 == id).OrderByDescending(j => j.pro_id).ToList());

        }



        public ActionResult coment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult coment(int? c_fk_log , string c_message ,DateTime c_date)
        {
            var p = obj.products.ToList();

            foreach(var item in p)
            {
                comment c = new comment();
                c.c_fk_log = c_fk_log;
                c.c_fk_pro = item.pro_id;
                c.c_message = c_message;
                c.c_date = c_date;
                
                obj.comments.Add(c);
                obj.SaveChanges();
            }
           
            return View();

        }

        public ActionResult product1()
        {

            
            return View(obj.products.OrderByDescending(s => s.pro_id).ToList());


        }



        [HttpPost]
        public ActionResult product1(string name,int? price)
        {
            TempData["abc"] = obj.sp_search(name, price).ToList();

            return RedirectToAction("search","Home");

        }

        public ActionResult search()
        {


            return View(TempData["abc"]);

        }




        //List<cart> li = new List<cart>();

        //[HttpPost]
        //public ActionResult product(product pi , string qty , int Id)
        //{
        //    product p = obj.products.Where(a => a.pro_id == Id).SingleOrDefault();

        //    cart c = new cart();

        //    c.productid = p.pro_id;
        //    c.qty = Convert.ToInt32(qty);
        //    c.price = Convert.ToInt32(p.pro_price);
        //    c.bill = c.price * c.qty;
        //    if (TempData["cart"] == null)
        //    {
        //        li.Add(c);
        //        TempData["cart"] = li;
        //    }
        //    else
        //    {
        //        List<cart> li2 = TempData["cart"] as List<cart>;
        //        li2.Add(c);
        //        TempData["cart"] = li2;
        //    }

        //    TempData.Keep();
        //    return RedirectToAction("addtocart");


        //}

        public ActionResult addtocart()
        {
            ViewBag.a = tocart.c;
            return View();

        }
        [HttpPost]
        public ActionResult addtocart(int id, int qty , product p)
        { 
            foreach (var item in tocart.c)
            {
                
                
                if (item.c_id == id)
                {
                    item.c_qty += qty;
                    ViewBag.a = tocart.c;
                   
                   
                    
                    return View();
                }
              
            }
            
            addcart li = new addcart() { c_id = id, c_qty = qty };
            tocart.c.Add(li);
            ViewBag.a = tocart.c;
            return View();

        }

        public ActionResult addcartnew()
        {
            ViewBag.a = tocart.c;
            return View();

        }
        [HttpPost]
        public ActionResult addcartnew(int id, int qty)
        {
            foreach (var item in tocart.c)
            {
                if (item.c_id == id)
                {
                    item.c_id += qty;
                    ViewBag.a = tocart.c;



                    return View();
                }
            }
            addcart li = new addcart() { c_id = id, c_qty = qty };
            tocart.c.Add(li);
            ViewBag.a = tocart.c;
            return View();

        }

        public ActionResult carthistory()
        {
                   
             ViewBag.cc = obj.carts.OrderByDescending(c => c.cart_id).ToList();
             return View();
            
        }


        public ActionResult order(int ammount)
        {

            ViewBag.a = ammount;
            ViewBag.d = tocart.c;
            return View();
        }

        [HttpPost]
        public ActionResult order(billingg b)
        {
            
            obj.billinggs.Add(b);
            obj.SaveChanges();
            ModelState.Clear();
            tocart.c.Clear();
            TempData["ordercon"] = "Thank you. Your order has been received.";


            return RedirectToAction("addtocart", "Home");
        }

        public ActionResult shopdone()
        {

            return View();
        }

        [HttpPost]
        public ActionResult shopdone(int? cart_fk_log)
        {
           var t= tocart.c.ToList();

            foreach (var item in t)
            {
                cart e = new cart();
               
                e.cart_qty = item.c_qty;
                e.cart_fk_log = cart_fk_log;
                e.cart_fk_pro = item.c_id;
                
                obj.carts.Add(e);
                obj.SaveChanges();
               
                
            }
            
            
            return RedirectToAction("addcartnew", "Home");
        }


        public ActionResult confrimntion()
        {
            ViewBag.c = tocart.c;
            return View();
        }


        public ActionResult removecart(int id)
        {
            tocart.c.RemoveAll(a => a.c_id == id);
            //var w=obj.carts.Find(id);
            //obj.carts.Remove(w);
            //obj.SaveChanges();
            return RedirectToAction("addtocart", "Home");
        }



       

        public ActionResult removeall()
        {
            tocart.c.Clear();
           //var e= obj.carts.ToList();
           // obj.carts.RemoveRange(e);
           // obj.SaveChanges();
            return RedirectToAction("addtocart", "Home");
        }

        

      
        public ActionResult applyforjob()
        {
            ViewBag.j_fk_vacan = new SelectList(obj.vacancis.ToList(), "v_id", "v_name");
            return View();

        }
        [HttpPost]
        public ActionResult applyforjob(job r)
        {
            if (ModelState.IsValid)
            {
                ViewBag.j_fk_vacan = new SelectList(obj.vacancis.ToList(), "v_id", "v_name", r.j_fk_vacan);
                var vagain = obj.jobs.Where(j => j.j_email == r.j_email).FirstOrDefault();
                if (vagain != null)
                {
                    ViewBag.againapply = "You Already Applyed For Job Please Wait Our Staf Contact With You soon";
                    ModelState.Clear();

                }

                
                obj.jobs.Add(r);
                obj.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("vacancies", "Home");
            }
            ViewBag.applyeror = "Not Submit Your Form Please Fil Again";
            return View();
        }
        public ActionResult vacancies()
        {

            return View(obj.vacancis.OrderByDescending(a => a.v_id).ToList());

        }
        [HttpPost]
        public ActionResult vacancies(string name)
        {
            var s = obj.vacancis.Where(a => a.v_name == name).ToList();
            return View(s);

        }

        public ActionResult vacanbtn(int? id)
        {
             ViewBag.v = obj.vacancis.Where(a => a.v_id == id).FirstOrDefault();
            return View();

        }

        public ActionResult contact()
        {
            
            return View();

        }
        [HttpPost]
        public ActionResult contact(contact c)
        {
            if (ModelState.IsValid)
            {
                obj.contacts.Add(c);
                obj.SaveChanges();
                ModelState.Clear();
                ViewBag.msg = "Your Message Succesfully Submit";
                return View();
            }
            ViewBag.erormsg = "Your Message Not Sent!";
            return View();
        }

    }
}

