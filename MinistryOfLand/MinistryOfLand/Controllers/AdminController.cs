using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinistryOfLand.Models;
using System.Web.Helpers;
using System.Web.Security;
using System.Data.Entity;
using System.Net;

namespace MinistryOfLand.Controllers
{
    public class AdminController : Controller
    {
        private HRM_DatabaseEntities db = new HRM_DatabaseEntities();
        //
        // GET: /Admin/
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserAccount u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (HRM_DatabaseEntities dc = new HRM_DatabaseEntities())
                {
                    var v = dc.UserAccounts.Where(a => a.username.Equals(u.username) && a.password.Equals(u.password)).FirstOrDefault();                                         
                       if (v != null )
                        {
                            Session["LogedUserID"] = v.userId.ToString();
                            Session["LogedUserFullname"] = v.username.ToString();
                            return RedirectToAction("GenaralInfo");
                        }  
                    else if(v==null)
                       {
                           TempData["wrong"] = "Username Or Password Invalid Please Try Again!";
                       }
                       //else
                       //{
                       //    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                       //}
                }
            }
            return View(u);
        }


        public ActionResult AdminLogin()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(UserAccount u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using (HRM_DatabaseEntities dc = new HRM_DatabaseEntities())
                {
                    var v = dc.UserAccounts.Where(a => a.username.Equals(u.username) && a.password.Equals(u.password) && a.UserType.Equals("Admin")).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.userId.ToString();
                        Session["LogedUserFullname"] = v.username.ToString();
                        return RedirectToAction("Registration", "User");
                    }
                    else if (v == null)
                    {
                        TempData["wrong"] = "Username Or Password Invalid Please Try Again!";
                    }
                    //else
                    //{
                    //    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    //}
                }
            }
            return View(u);
        }

        public ActionResult UserProfile()
        {
            if (Session["LogedUserID"] != null)
            {
                var uid = int.Parse(Session["LogedUserID"].ToString());
                List<Object> myModel = new List<object>();
                myModel.Add(db.GeneralInfoes.Where(i => i.userId == uid).ToList());

                myModel.Add(db.ForeignTravles.Where(i => i.userId == uid).ToList());

                myModel.Add(db.EducationalInfoes.Where(i => i.UserId == uid).ToList());

                myModel.Add(db.LanguageInfoes.Where(i => i.Userid == uid).ToList());

                myModel.Add(db.PostingRecords.Where(i => i.userId == uid).ToList());

                myModel.Add(db.Publications.Where(i => i.userId == uid).ToList());

                myModel.Add(db.Awards.Where(i => i.UserId == uid).ToList());

                myModel.Add(db.ChildrenInfoes.Where(i => i.userId == uid).ToList());

                myModel.Add(db.PromotionalParticulars.Where(i => i.userId == uid).ToList());

                myModel.Add(db.TrainingInfoes.Where(i => i.UserId == uid).ToList());

                myModel.Add(db.UserAccounts.Where(i => i.userId == uid).ToList());
                
                return View(myModel);
            }
            else
            {
                return RedirectToAction("UserProfile");
            }
        }   

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

       public ActionResult Request()
        {
            return View();
        }
        [HttpPost]
       public ActionResult Request(RequestToChange rtc)
       {
            if (ModelState.IsValid)
            {
                WebMail.Send("hrm.ictgov@gmail.com"
                            , rtc.Name + "Is Qequest To You"
                            , rtc.Message + "<br/><br/> My Mobile Number: " + rtc.Mobile + "<br/><br/> My Mail Address : " + rtc.Email + "<br/><br/> Changing Table :" + rtc.Category
                            , null
                            , null
                            , null
                            , true
                            , null
                            , null
                            , null
                            , null
                            , null
                            , rtc.Email);
                ModelState.Clear();
                ViewBag.Msg = "Thank You for your Request, We will get it as soon as possible.";
                RedirectToAction("UserProfile");
            }
            return View();
       }

        public ActionResult GenaralInfo()
        {
            if (Session["LogedUserID"] != null)
            {
                var uid = int.Parse(Session["LogedUserID"].ToString());
                List<Object> myModel = new List<object>();
                myModel.Add(db.GeneralInfoes.Where(i => i.userId == uid).ToList());
                myModel.Add(db.UserAccounts.Where(i => i.userId == uid).ToList());
                return View(myModel);
            }
            else
            {
                return RedirectToAction("GenaralInfo");
            }
         }

        public ActionResult Education()
        {
            if (Session["LogedUserID"] != null)
            {
                var uid = int.Parse(Session["LogedUserID"].ToString());
                List<Object> myModel = new List<object>();
                myModel.Add(db.GeneralInfoes.Where(i => i.userId == uid).ToList());
                myModel.Add(db.UserAccounts.Where(i => i.userId == uid).ToList());
                myModel.Add(db.EducationalInfoes.Where(i => i.UserId == uid).ToList());
                return View(myModel);
            }
            else
            {
                return RedirectToAction("Education");
            }
        }

        public ActionResult Language()
        {
            if (Session["LogedUserID"] != null)
            {
                var uid = int.Parse(Session["LogedUserID"].ToString());
                List<Object> myModel = new List<object>();
                myModel.Add(db.GeneralInfoes.Where(i => i.userId == uid).ToList());
                myModel.Add(db.UserAccounts.Where(i => i.userId == uid).ToList());
                myModel.Add(db.LanguageInfoes.Where(i => i.Userid == uid).ToList());
                return View(myModel);
            }
            else
            {
                return RedirectToAction("Language");
            }
        }

        public ActionResult Training()
        {
            if (Session["LogedUserID"] != null)
            {
                var uid = int.Parse(Session["LogedUserID"].ToString());
                List<Object> myModel = new List<object>();
                myModel.Add(db.GeneralInfoes.Where(i => i.userId == uid).ToList());
                myModel.Add(db.UserAccounts.Where(i => i.userId == uid).ToList());
                myModel.Add(db.TrainingInfoes.Where(i => i.UserId == uid).ToList());
                return View(myModel);
            }
            else
            {
                return RedirectToAction("Training");
            }
        }

        public ActionResult Children()
        {
            if (Session["LogedUserID"] != null)
            {
                var uid = int.Parse(Session["LogedUserID"].ToString());
                List<Object> myModel = new List<object>();
                myModel.Add(db.GeneralInfoes.Where(i => i.userId == uid).ToList());
                myModel.Add(db.UserAccounts.Where(i => i.userId == uid).ToList());
                myModel.Add(db.ChildrenInfoes.Where(i => i.userId == uid).ToList());
                return View(myModel);
            }
            else
            {
                return RedirectToAction("Children");
            }
        }

        public ActionResult Award()
        {
            if (Session["LogedUserID"] != null)
            {
                var uid = int.Parse(Session["LogedUserID"].ToString());
                List<Object> myModel = new List<object>();
                myModel.Add(db.GeneralInfoes.Where(i => i.userId == uid).ToList());
                myModel.Add(db.UserAccounts.Where(i => i.userId == uid).ToList());
                myModel.Add(db.Awards.Where(i => i.UserId == uid).ToList());
                return View(myModel);
            }
            else
            {
                return RedirectToAction("Award");
            }
        }

        public ActionResult Publication()
        {
            if (Session["LogedUserID"] != null)
            {
                var uid = int.Parse(Session["LogedUserID"].ToString());
                List<Object> myModel = new List<object>();
                myModel.Add(db.GeneralInfoes.Where(i => i.userId == uid).ToList());
                myModel.Add(db.UserAccounts.Where(i => i.userId == uid).ToList());
                myModel.Add(db.Publications.Where(i => i.userId == uid).ToList());
                return View(myModel);
            }
            else
            {
                return RedirectToAction("Publication");
            }
        }

        public ActionResult PostingRecords()
        {
            if (Session["LogedUserID"] != null)
            {
                var uid = int.Parse(Session["LogedUserID"].ToString());
                List<Object> myModel = new List<object>();
                myModel.Add(db.GeneralInfoes.Where(i => i.userId == uid).ToList());
                myModel.Add(db.UserAccounts.Where(i => i.userId == uid).ToList());
                myModel.Add(db.PostingRecords.Where(i => i.userId == uid).ToList());
                return View(myModel);
            }
            else
            {
                return RedirectToAction("PostingRecords");
            }
        }

        public ActionResult PromotionalParticulars()
        {
            if (Session["LogedUserID"] != null)
            {
                var uid = int.Parse(Session["LogedUserID"].ToString());
                List<Object> myModel = new List<object>();
                myModel.Add(db.GeneralInfoes.Where(i => i.userId == uid).ToList());
                myModel.Add(db.UserAccounts.Where(i => i.userId == uid).ToList());
                myModel.Add(db.PromotionalParticulars.Where(i => i.userId == uid).ToList());
                return View(myModel);
            }
            else
            {
                return RedirectToAction("PromotionalParticulars");
            }
        }


        public ActionResult Travle()
        {
            if (Session["LogedUserID"] != null)
            {
                var uid = int.Parse(Session["LogedUserID"].ToString());
                List<Object> myModel = new List<object>();
                myModel.Add(db.GeneralInfoes.Where(i => i.userId == uid).ToList());
                myModel.Add(db.UserAccounts.Where(i => i.userId == uid).ToList());
                myModel.Add(db.ForeignTravles.Where(i => i.userId == uid).ToList());
                return View(myModel);
            }
            else
            {
                return RedirectToAction("PromotionalParticulars");
            }
        }

        public ActionResult ForgetAdminPassword(UserAccount u)
        {
            if (ModelState.IsValid)
            {
                using (HRM_DatabaseEntities dc = new HRM_DatabaseEntities())
                {
                    var v = dc.UserAccounts.Where(a => a.username.Equals(u.username) && a.email.Equals(u.email) && a.UserType.Equals("Admin")).FirstOrDefault();
                    if (v != null)
                    {
                        Session["forgetUserID"] = v.userId.ToString();
                        Session["forgetUserFullname"] = v.username.ToString();
                        Session["forgetEmail"] = v.email.ToList();                       
                        return RedirectToAction("ConfirmAdminNewPassword","Admin");
                    }
                }
            }
            return View();
        }

        public ActionResult ConfirmAdminNewPassword()
        {
            return View();    
        }
        [HttpPost]
        public ActionResult ConfirmAdminNewPassword(UserAccount ua)
        {           
                if (Session["forgetUserID"] != null && Session["forgetEmail"]!=null)
                {                    
                    db.UserAccounts.Find(int.Parse(Session["forgetUserID"].ToString())).password=ua.password;                    
                    db.SaveChanges();
                    return RedirectToAction("AdminLogin", "Admin");
                }         
            return View(ua);
        }

        public ActionResult ForgetUserPassword(UserAccount u)
        {
            if (ModelState.IsValid)
            {
                using (HRM_DatabaseEntities dc = new HRM_DatabaseEntities())
                {
                    var v = dc.UserAccounts.Where(a => a.username.Equals(u.username) && a.email.Equals(u.email) && a.UserType.Equals("User")).FirstOrDefault();
                    if (v != null)
                    {
                        Session["forgetUserID"] = v.userId.ToString();
                        Session["forgetUserFullname"] = v.username.ToString();
                        Session["forgetEmail"] = v.email.ToList();
                        return RedirectToAction("ConfirmUserNewPassword", "Admin");
                    }
                }
            }
            return View();
        }

       
        public ActionResult ConfirmUserNewPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmUserNewPassword(UserAccount ua)
        {
            if (Session["forgetUserID"] != null && Session["forgetEmail"] != null)
            {
                db.UserAccounts.Find(int.Parse(Session["forgetUserID"].ToString())).password = ua.password;
                db.SaveChanges();
                return RedirectToAction("Login", "Admin");
            }
            return View(ua);
        }
	}
}