using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinistryOfLand.Models;

namespace MinistryOfLand.Controllers
{
    public class ViewController : Controller
    {
        private HRM_DatabaseEntities db = new HRM_DatabaseEntities();
        //
        // GET: /View/
        public ActionResult ViewResume()
        {
            List<Object> myModel = new List<object>();
            myModel.Add(db.GeneralInfoes.ToArray().LastOrDefault());
            myModel.Add(db.ForeignTravles.ToList());
            myModel.Add(db.EducationalInfoes.ToList());
            myModel.Add(db.LanguageInfoes.ToList());
            myModel.Add(db.PostingRecords.ToList());
            myModel.Add(db.Publications.ToList());
            myModel.Add(db.Awards.ToList());
            return View(myModel);
        }


        public ActionResult Resume()
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
	}
}