using MinistryOfLand.App_Start;
using MinistryOfLand.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MinistryOfLand.Controllers
{
    public class UserController : Controller
    {
        private HRM_DatabaseEntities db = new HRM_DatabaseEntities();
        //
        // GET: /User/
        public ActionResult Index()
        {
            UserAccountIDDropDownList();
            return View();
        }

        public ActionResult Training()
        {
            List<TrainingInfo> list = db.TrainingInfoes.ToList();
            UserAccountIDDropDownList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Training(TrainingInfo tra )
        {
            if(ModelState.IsValid)
            {
                db.TrainingInfoes.Add(tra);
                db.SaveChanges();
                return RedirectToAction("Confirmation");
            }          
            UserAccountIDDropDownList();
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("AdminLogin","Admin");
        }
        
        public ActionResult Other()
        {
            return View();
        }

        private void UserAccountIDDropDownList(object selectedUserProfile = null)
        {
            var userIdQuery = from d in db.UserAccounts
                              orderby d.userId
                              select d;
            ViewBag.UserAccountId = new SelectList(userIdQuery, "UserId", "username", selectedUserProfile);
        }

        /*User Registration */
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(UserAccount useraccount, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //string ImageName = System.IO.Path.GetFileName(file.FileName);
                    //string physicalPath = Server.MapPath("~/Content/Image/User/" + ImageName);
                    //file.SaveAs(physicalPath);
                  var fileName=  FileUploader.FileUpload(this.ControllerContext, "Content/Image/User/").FirstOrDefault();
                  if (fileName != null)
                  {
                      useraccount.UserImage = fileName;
                      db.UserAccounts.Add(useraccount);
                      db.SaveChanges();
                  }
                    return RedirectToAction("Registration");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(useraccount);
        }

    /*end user registration function*/

        /*Posting information*/
        public ActionResult Posting()
        {
            UserAccountIDDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Posting(PostingRecord ps)
        {
            if (ModelState.IsValid)
            {
                db.PostingRecords.Add(ps);
                db.SaveChanges();
                return RedirectToAction("Confirmation","User");
            }
            UserAccountIDDropDownList();
            return View();
        }
        /*End Posting record*/


        public ActionResult PromotionalParticular()
        {
            UserAccountIDDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PromotionalParticular(PromotionalParticular pp)
        {
            if (ModelState.IsValid)
            {
                db.PromotionalParticulars.Add(pp);
                db.SaveChanges();

                return RedirectToAction("Confirmation");
            }
            UserAccountIDDropDownList();
            return View();
        }


        /*Publication*/
        public ActionResult Publication()
        {
            UserAccountIDDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Publication(Publication pa)
        {
            if (ModelState.IsValid)
            {
                db.Publications.Add(pa);
                db.SaveChanges();
                return RedirectToAction("Confirmation");
            }
            UserAccountIDDropDownList();
            return View();
        }
        /*End Publication*/


        public ActionResult LanguageInfo()
        {
            UserAccountIDDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LanguageInfo(LanguageInfo la)
        {
            if (ModelState.IsValid)
            {
                db.LanguageInfoes.Add(la);
                db.SaveChanges();
                return RedirectToAction("Confirmation");
            }
            UserAccountIDDropDownList();
            return View();
        }

        public ActionResult ForeignTravle()
        {
            UserAccountIDDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForeignTravle(ForeignTravle ft)
        {
            if (ModelState.IsValid)
            {
                db.ForeignTravles.Add(ft);
                db.SaveChanges();
                return RedirectToAction("Confirmation");
            }
            UserAccountIDDropDownList();
            return View();
        }

        public ActionResult ChildrenInfo()
        {
            UserAccountIDDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChildrenInfo(ChildrenInfo ci)
        {
            if (ModelState.IsValid)
            {
                db.ChildrenInfoes.Add(ci);
                db.SaveChanges();
                return RedirectToAction("Confirmation");
            }
            UserAccountIDDropDownList();
            return View();
        }

        public ActionResult EditRessume()
        {
            return View();
        }

    /*Generel Information Action*/
        public ActionResult GenaralInfo()
        {
            List<GeneralInfo> list = db.GeneralInfoes.ToList();
            UserAccountIDDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenaralInfo(GeneralInfo gi)
        {
            if (ModelState.IsValid)
            {
                db.GeneralInfoes.Add(gi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            UserAccountIDDropDownList();
            return View();
        }

        public ActionResult DataGenaralInfo()
        {
            return View(db.GeneralInfoes.ToList());
        }

        public ActionResult DetailsGeneralInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GeneralInfo gi = db.GeneralInfoes.Find(id);
            if (gi == null)
            {
                return HttpNotFound();
            }
            return View(gi);
        }

        public ActionResult EditGeneralInfo(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            GeneralInfo gi = db.GeneralInfoes.Find(id);
            if (gi == null)
            {
                return HttpNotFound();
            }
            UserAccountIDDropDownList();
            return View(gi);
        }

        [HttpPost]
        public ActionResult EditGeneralInfo(GeneralInfo gi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DataGenaralInfo");
            }
            UserAccountIDDropDownList(gi.userId);
            return View(gi);

        }

        public ActionResult DeleteGeneralInfo(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralInfo gi = db.GeneralInfoes.Find(id);
            if (gi == null)
            {
                return HttpNotFound();
            }
            if (gi != null)
            {
                db.GeneralInfoes.Remove(gi);
                db.SaveChanges();
                return RedirectToAction("DataGenaralInfo");
            }
            return View(gi);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GeneralInfo gi = db.GeneralInfoes.Find(id);
        //    db.GeneralInfoes.Remove(gi);
        //    db.SaveChanges();
        //    return RedirectToAction("DetailsGeneralInfo");
        //}

        /*End General Information */
        /* Award Action*/

        public ActionResult Award()
        {
            List<Award> list = db.Awards.ToList();
            UserAccountIDDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Award(Award award)
        {
            if (ModelState.IsValid)
            {
                db.Awards.Add(award);
                db.SaveChanges();
                return RedirectToAction("Confirmation");
            }
            UserAccountIDDropDownList();
            return View();
        }

        public ActionResult DataAward()
        {
            return View(db.Awards.ToList());
        }

        public ActionResult DetailsAward(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return HttpNotFound();
            }
            return View(award);
        }

        public ActionResult EditAward(int id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return HttpNotFound();
            }
            return View(award);
        }

        [HttpPost]
        public ActionResult EditAward(Award award)
        {
            if (ModelState.IsValid)
            {
                db.Entry(award).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DataAward");
            }
            UserAccountIDDropDownList(award.UserId);
            return View(award);
        }

        public ActionResult DeleteAward(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Award award = db.Awards.Find(id);
            if (award == null)
            {
                return HttpNotFound();
            }
            if(award != null)
            {
                  db.Awards.Remove(award);
                  db.SaveChanges();
               return RedirectToAction("DataAward");
            }
            return View(award);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Award award = db.Awards.Find(id);
        //    db.Awards.Remove(award);
        //    db.SaveChanges();
        //    return RedirectToAction("DataAward");
        //}
        /*End Award Information */

        /*Education Action*/
        public ActionResult Education()
        {
            UserAccountIDDropDownList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Education(EducationalInfo edu)
        {
            if (ModelState.IsValid)
            {
                db.EducationalInfoes.Add(edu);
                db.SaveChanges();
                return RedirectToAction("Confirmation");
            }
            UserAccountIDDropDownList();
            return View();
        }


        public ActionResult DataEducation()
        {
            return View(db.EducationalInfoes.ToList());
        }

        public ActionResult DetailsEducation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EducationalInfo edu = db.EducationalInfoes.Find(id);
            if (edu == null)
            {
                return HttpNotFound();
            }
            return View(edu);
        }

        public ActionResult EditEducation(int id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            EducationalInfo edu = db.EducationalInfoes.Find(id);
            if (edu == null)
            {
                return HttpNotFound();
            }
            return View(edu);
        }

        [HttpPost]
        public ActionResult EditEducation(EducationalInfo edu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(edu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DataEducation");
            }
            UserAccountIDDropDownList(edu.UserId);
            return View(edu);
        }

        public ActionResult DeleteEducation(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationalInfo edu = db.EducationalInfoes.Find(id);
            if (edu == null)
            {
                return HttpNotFound();
            }
            if (edu != null)
            {
                db.EducationalInfoes.Remove(edu);
                db.SaveChanges();
                return RedirectToAction("DataEducation");
            }
            return View(edu);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GeneralInfo gi = db.GeneralInfoes.Find(id);
        //    db.GeneralInfoes.Remove(gi);
        //    db.SaveChanges();
        //    return RedirectToAction("DetailsGeneralInfo");
        //}

        /*End Education Information */
        /*Foreign Travle Information Action*/

        public ActionResult DataForeignTravle()
        {
            return View(db.ForeignTravles.ToList());
        }

        public ActionResult DetailsForeignTravleo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ForeignTravle ft = db.ForeignTravles.Find(id);
            if (ft == null)
            {
                return HttpNotFound();
            }
            return View(ft);
        }

        public ActionResult EditForeignTravle(int id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            ForeignTravle ft = db.ForeignTravles.Find(id);
            if (ft == null)
            {
                return HttpNotFound();
            }
            return View(ft);
        }

        [HttpPost]
        public ActionResult EditForeignTravle(ForeignTravle ft)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ft).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DataForeignTravle");
            }
            UserAccountIDDropDownList(ft.userId);
            return View(ft);
        }

        public ActionResult DeleteForeignTravle(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ForeignTravle ft = db.ForeignTravles.Find(id);
            if (ft == null)
            {
                return HttpNotFound();
            }
            if (ft != null)
            {
                db.ForeignTravles.Remove(ft);
                db.SaveChanges();
                return RedirectToAction("DataForeignTravle");
            }
            return View(ft);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GeneralInfo gi = db.GeneralInfoes.Find(id);
        //    db.GeneralInfoes.Remove(gi);
        //    db.SaveChanges();
        //    return RedirectToAction("DetailsGeneralInfo");
        //}
        /*End Foreign Travle Information */
        /* Language Action*/
        public ActionResult DataLanguageInfo()
        {
            return View(db.LanguageInfoes.ToList());
        }

        public ActionResult DetailsLanguageInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LanguageInfo lan = db.LanguageInfoes.Find(id);
            if (lan == null)
            {
                return HttpNotFound();
            }
            return View(lan);
        }

        public ActionResult EditLanguageInfo(int id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            LanguageInfo lan = db.LanguageInfoes.Find(id);
            if (lan == null)
            {
                return HttpNotFound();
            }
            return View(lan);
        }

        [HttpPost]
        public ActionResult EditLanguageInfo(LanguageInfo lan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DataLanguageInfo");
            }
            UserAccountIDDropDownList(lan.Userid);
            return View(lan);
        }

        public ActionResult DeleteLanguageInfo(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguageInfo lan = db.LanguageInfoes.Find(id);
            if (lan == null)
            {
                return HttpNotFound();
            }
            if (lan != null)
            {
                db.LanguageInfoes.Remove(lan);
                db.SaveChanges();
                return RedirectToAction("DataLanguageInfo");
            }
            return View(lan);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GeneralInfo gi = db.GeneralInfoes.Find(id);
        //    db.GeneralInfoes.Remove(gi);
        //    db.SaveChanges();
        //    return RedirectToAction("DetailsGeneralInfo");
        //}

        /*End Language Information */
        /*Posting Action*/

        public ActionResult DataPostingRecords()
        {
            return View(db.PostingRecords.ToList());
        }

        public ActionResult DetailsPostingRecords(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PostingRecord post = db.PostingRecords.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        public ActionResult EditPostingRecords(int id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            PostingRecord post = db.PostingRecords.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost]
        public ActionResult EditPostingRecords(PostingRecord post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DataPostingRecords");
            }
            UserAccountIDDropDownList(post.userId);
            return View(post);
        }

        public ActionResult DeletePostingRecords(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostingRecord post = db.PostingRecords.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (post != null)
            {
                db.PostingRecords.Remove(post);
                db.SaveChanges();
                return RedirectToAction("DataPostingRecords");
            }
            return View(post);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GeneralInfo gi = db.GeneralInfoes.Find(id);
        //    db.GeneralInfoes.Remove(gi);
        //    db.SaveChanges();
        //    return RedirectToAction("DetailsGeneralInfo");
        //}

        /*End Posting Information */
        /*PromotionalParticular Action*/

        public ActionResult DataPromotionalParticular()
        {
            return View(db.PromotionalParticulars.ToList());
        }

        public ActionResult DetailsPromotionalParticular(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            PromotionalParticular pp = db.PromotionalParticulars.Find(id);
            if (pp == null)
            {
                return HttpNotFound();
            }

            return View(pp);
        }

        public ActionResult EditPromotionalParticular(int id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            PromotionalParticular pp = db.PromotionalParticulars.Find(id);
            if (pp == null)
            {
                return HttpNotFound();
            }

            return View(pp);
        }

        [HttpPost]
        public ActionResult EditPromotionalParticular(PromotionalParticular pp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DataPromotionalParticular");
            }
            UserAccountIDDropDownList(pp.userId);
            return View(pp);
        }

        public ActionResult DeletePromotionalParticular(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionalParticular pp = db.PromotionalParticulars.Find(id);
            if (pp == null)
            {
                return HttpNotFound();
            }
            if (pp != null)
            {
                db.PromotionalParticulars.Remove(pp);
                db.SaveChanges();
                return RedirectToAction("DataPromotionalParticular");
            }
            return View(pp);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GeneralInfo gi = db.GeneralInfoes.Find(id);
        //    db.GeneralInfoes.Remove(gi);
        //    db.SaveChanges();
        //    return RedirectToAction("DetailsGeneralInfo");
        //}

        /*End PromotionalParticular Information */
        /*Publication Action*/

        public ActionResult DataPublication()
        {
            return View(db.Publications.ToList());
        }

        public ActionResult DetailsPublication(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Publication pub = db.Publications.Find(id);
            if (pub == null)
            {
                return HttpNotFound();
            }
            return View(pub);
        }

        public ActionResult EditPublication(int id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Publication pub = db.Publications.Find(id);
            if (pub == null)
            {
                return HttpNotFound();
            }
            return View(pub);
        }

        [HttpPost]
        public ActionResult EditPublication(Publication pub)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pub).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DataPublication");
            }
            UserAccountIDDropDownList(pub.userId);
            return View(pub);
        }

        public ActionResult DeletePublication(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publication pub = db.Publications.Find(id);
            if (pub == null)
            {
                return HttpNotFound();
            }
            if (pub != null)
            {
                db.Publications.Remove(pub);
                db.SaveChanges();
                return RedirectToAction("DataPublication");
            }
            return View(pub);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GeneralInfo gi = db.GeneralInfoes.Find(id);
        //    db.GeneralInfoes.Remove(gi);
        //    db.SaveChanges();
        //    return RedirectToAction("DetailsGeneralInfo");
        //}

        /*End Publication Information */
        /*Publication Action*/

        public ActionResult DataTraining()
        {
            return View(db.TrainingInfoes.ToList());
        }

        public ActionResult DetailsTraining(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TrainingInfo tra = db.TrainingInfoes.Find(id);
            if (tra == null)
            {
                return HttpNotFound();
            }
            return View(tra);
        }

        public ActionResult EditTraining(int id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            TrainingInfo tra = db.TrainingInfoes.Find(id);
            if (tra == null)
            {
                return HttpNotFound();
            }
            return View(tra);
        }

        [HttpPost]
        public ActionResult EditTraining(TrainingInfo tra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DataTraining");
            }
            UserAccountIDDropDownList(tra.UserId);
            return View(tra);
        }

        public ActionResult DeleteTraining(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingInfo tra = db.TrainingInfoes.Find(id);
            if (tra == null)
            {
                return HttpNotFound();
            }
            if (tra != null)
            {
                db.TrainingInfoes.Remove(tra);
                db.SaveChanges();
                return RedirectToAction("DataTraining");
            }
            return View(tra);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GeneralInfo gi = db.GeneralInfoes.Find(id);
        //    db.GeneralInfoes.Remove(gi);
        //    db.SaveChanges();
        //    return RedirectToAction("DetailsGeneralInfo");
        //}

        /*End Publication Information */

        /*Children Info Action*/

        public ActionResult DataChildren()
        {
            return View(db.ChildrenInfoes.ToList());
        }

        public ActionResult DetailsChildren(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ChildrenInfo children = db.ChildrenInfoes.Find(id);
            if (children == null)
            {
                return HttpNotFound();
            }
            return View(children);
        }

        public ActionResult EditChildren(int id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            ChildrenInfo children = db.ChildrenInfoes.Find(id);
            if (children == null)
            {
                return HttpNotFound();
            }
            return View(children);
        }

        [HttpPost]
        public ActionResult EditChildren(ChildrenInfo children)
        {
            if (ModelState.IsValid)
            {
                db.Entry(children).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DataChildren");
            }
            UserAccountIDDropDownList(children.userId);
            return View(children);
        }

        public ActionResult DeleteChildren(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChildrenInfo children = db.ChildrenInfoes.Find(id);
            if (children == null)
            {
                return HttpNotFound();
            }
            if (children != null)
            {
                db.ChildrenInfoes.Remove(children);
                db.SaveChanges();
                return RedirectToAction("DataChildren");
            }
            return View(children);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    GeneralInfo gi = db.GeneralInfoes.Find(id);
        //    db.GeneralInfoes.Remove(gi);
        //    db.SaveChanges();
        //    return RedirectToAction("DetailsGeneralInfo");
        //}
        /*End Publication Information */

        /*Service History*/
        public ActionResult ServiceHistory()
        {
            UserAccountIDDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ServiceHistory(ServiceHistory sh)
        {
            if (ModelState.IsValid)
            {
                db.ServiceHistories.Add(sh);
                db.SaveChanges();
                return RedirectToAction("Confirmation");
            }
            UserAccountIDDropDownList();
            return View();
        }
        /*End Service history*/

        /*Posting Address*/
        public ActionResult PostingAddress()
        {
            UserAccountIDDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostingAddress(PostingAddress pa)
        {
            if (ModelState.IsValid)
            {
                db.PostingAddresses.Add(pa);
                db.SaveChanges();
                return RedirectToAction("Confirmation");
            }
            UserAccountIDDropDownList();
            return View();
        }
        /*End Posting Address*/
        /*User Information*/
        public ActionResult DataUserAccount()
        {
            return View(db.UserAccounts.ToList());
        }

        public ActionResult DetailsUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserAccount ua = db.UserAccounts.Find(id);
            if (ua == null)
            {
                return HttpNotFound();
            }
            return View(ua);
        }

        public ActionResult EditUser(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            UserAccount ua = db.UserAccounts.Find(id);
            if (ua == null)
            {
                return HttpNotFound();
            }
            return View(ua);
        }

        [HttpPost]
        public ActionResult EditUser(UserAccount ua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DataUserAccount");
            }
            UserAccountIDDropDownList(ua.userId);
            return View(ua);
        }

        public ActionResult DeleteUser(int? id)
        {
            UserAccountIDDropDownList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount ua = db.UserAccounts.Find(id);
            if (ua == null)
            {
                return HttpNotFound();
            }
            if (ua != null)
            {
                db.UserAccounts.Remove(ua);
                db.SaveChanges();
                return RedirectToAction("DataUserAccount");
            }
            return View(ua);
        }
            /*End User Information*/
        
        public ActionResult Confirmation()
        {
            return View();
        }

	}
}