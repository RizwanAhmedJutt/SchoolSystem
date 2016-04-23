using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Script.Serialization;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class StudentReportController : Controller
    {
        IAssessmentCategories repoAssessmentCategory = new AssessmentCategoriesBLL();
        IDailyAssessmentType repoAssessmentType = new DailyAssessmentTypeBLL();
        IDailyAssessmentSubType repoAssessementSubType = new DailyAssessmentSubTypeBLL();
        // GET: /StudentReport/
        public ActionResult GetALLAssessmentCategories()
        {
            return View(repoAssessmentCategory.GetALLAssessmentCategories().ToList());
        }
        [HttpGet]
        public ActionResult AddChangesAssessmentCategory(int Id)
        {
            AssessmentCategories dAssessCategory = repoAssessmentCategory.GetAssessmentCategoryById(Id);
            if (Id == 0)
            {
                AssessmentCategories dassesscategory = new AssessmentCategories();

                return View(dassesscategory);
            }
            else
                return View(dAssessCategory);
        }
        [HttpPost]
        public ActionResult AddChangesAssessmentCategory(AssessmentCategories dAssesscategory)
        {
            var userloggedId = User.Identity.GetUserId();
            if (dAssesscategory.AssessmentCategoryId == 0)
            {
                dAssesscategory.CreatedById = userloggedId;

            }
            else
            {
                dAssesscategory.ModifiedById = userloggedId;
                dAssesscategory.ModifiedDate = DateTime.Now;
            }
            int getStatus = repoAssessmentCategory.AddChangeAssessmentCategories(dAssesscategory);
            return RedirectToAction("GetALLAssessmentCategories");
        }
        public ActionResult GetALLAssessment()
        {
            return View(repoAssessmentType.GetAllAssessmentType().ToList());
        }
        [HttpGet]
        public ActionResult AddChangesAssessmentType(int Id)
        {
            DailyAssessmentType dAssessType = repoAssessmentType.GetDailyAssessmentById(Id);
            if (Id == 0)
            {
                DailyAssessmentType dassesstype = new DailyAssessmentType();

                return View(dassesstype);
            }
            else
                return View(dAssessType);
        }
        [HttpPost]
        public ActionResult AddChangesAssessmentType(DailyAssessmentType dAssesstype, int AssessmentCategoryId)
        {
            var userloggedId = User.Identity.GetUserId();
            dAssesstype.AssessmentCategoryId = AssessmentCategoryId;
            if (dAssesstype.AssessmentTypeId == 0)
            {
                dAssesstype.CreatedById = userloggedId;

            }
            else
            {
                dAssesstype.ModifiedById = userloggedId;
                dAssesstype.ModifiedDate = DateTime.Now;
            }
            int getStatus = repoAssessmentType.AddChangeDailyAssessmentType(dAssesstype);
            return RedirectToAction("GetALLAssessment");
        }
        //Get All SubAssement Type
        public ActionResult GetALLSubAssessment(int? page)
        {
            return View(repoAssessementSubType.GetAllAssessmentSubType().ToPagedList(page ?? 1, 10));
        }
        [HttpGet]
        public ActionResult AddChangesSubAssessmentType(int Id)
        {
            DailyAssessmentSubType subAssessment = repoAssessementSubType.GetDailyAssessmentSubTypeById(Id);
            if (Id == 0)
            {
                DailyAssessmentSubType subassessmen = new DailyAssessmentSubType();
                return View(subAssessment);
            }
            return View(subAssessment);
        }
        [HttpPost]
        public ActionResult AddChangesSubAssessmentType(DailyAssessmentSubType dailyAssementSubType)
        {
            var userloggedId = User.Identity.GetUserId();
            if (dailyAssementSubType.AssessmentSubTypeId == 0)
            {
                dailyAssementSubType.CreatedById = userloggedId;

            }
            else
            {
                dailyAssementSubType.ModifiedById = userloggedId;
                dailyAssementSubType.ModifiedDate = DateTime.Now;
            }
            int getStatus = repoAssessementSubType.AddChangesAssessmentSubType(dailyAssementSubType);
            return RedirectToAction("GetALLSubAssessment");
        }

        public ActionResult AddChangesDailyAssessmentReport()
        {
            List<DailyAssessmentType> type = repoAssessmentType.GetAllAssessmentType();
            List<DailyAssessmentSubType> subtype = repoAssessementSubType.GetAllAssessmentSubType();
            dynamic mymodel = new ExpandoObject();
            mymodel.AssessmentType = type;
            mymodel.AssessmentSubType = subtype;
            return View(mymodel);
        }
        [HttpGet]
        public ActionResult AddChangesAssessmentReport()
        {


            DailyAssessmentHelper myModel = new DailyAssessmentHelper();
            myModel.ParentAssessments = repoAssessmentType.GetALLAssignedParentAssessments();
            myModel.ChildAssessments = repoAssessementSubType.GetAllAssessmentSubType();
            return View(myModel);
        }
        [HttpPost]
        public ActionResult AddChangesAssessmentReport(DailyAssessmentHelper helper, int AcadmicClassId, int StudentId)
        {

            return View();
        }
        public string CheckAssessmentTypeExist(string AssessmentName, int AssessmentCategoryId)
        {
            DailyAssessmentType assessmenttype = repoAssessmentType.GetDailyAssessmentTypeByName(AssessmentName, AssessmentCategoryId);
            if (assessmenttype.AssessmentTypeId > 0)
                return new JavaScriptSerializer().Serialize(true);// Room Name  already Exist
            else
                return new JavaScriptSerializer().Serialize(false);   // Room name not already Exist

        }
       
        public string CheckSubAssementExist(int ParentAssessmentId, string SubAssessmentName)
        {
            DailyAssessmentSubType assessmentsubtype = repoAssessementSubType.CheckSubAssementExist(ParentAssessmentId, SubAssessmentName);
            if (assessmentsubtype.AssessmentSubTypeId > 0)
                return new JavaScriptSerializer().Serialize(true);
            else
                return new JavaScriptSerializer().Serialize(false);   
        }
        public string CheckAssessmentCategoryExist(string AssessmentName)
        {
            AssessmentCategories assessmentsubtype = repoAssessmentCategory.GetAssessmentCategoryByName(AssessmentName);
            if (assessmentsubtype.AssessmentCategoryId > 0)
                return new JavaScriptSerializer().Serialize(true);
            else
                return new JavaScriptSerializer().Serialize(false);   
        } 
        [HttpGet]
        public ActionResult DDLAssessment(int AssessmentCategoryId)
        {
            if (AssessmentCategoryId == 0)
            {
                ViewData["DDLAssessment"] = new SelectList(repoAssessmentType.GetAllAssessmentType().OrderBy(c => c.AssessmentTypeId).ToList(), "AssessmentTypeId", "AssessmentName");
                return View("../DropDownLists/DDLAssessments");
            }
            else
            {
                ViewData["DDLAssessment"] = new SelectList(repoAssessmentType.GetAllAssessmentType().Where(x=>x.AssessmentCategoryId==AssessmentCategoryId).OrderBy(c => c.AssessmentTypeId).ToList(), "AssessmentTypeId", "AssessmentName");
                return View("../DropDownLists/DDLAssessments");
            }
           

        }

        public string GetAssessmentCriteria(int AssessmentCategoryId,int AssessmentTypeId)
        {
            string getCriteria=string.Empty;
            var GetAssessmentCriteria = from x in repoAssessmentType.GetAllAssessmentType()
                                        where x.AssessmentCategoryId == AssessmentCategoryId
                                        && x.AssessmentTypeId == AssessmentTypeId
                                        select x.AssessmentCriteria;
            foreach (string item in GetAssessmentCriteria)
            {
                getCriteria= item;
            }
            return getCriteria;
        }
    }
}