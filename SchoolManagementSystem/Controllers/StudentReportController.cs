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
    [Authorize(Roles = "Admin")]
    public class StudentReportController : Controller
    {
        IAssessmentCategories repoAssessmentCategory = new AssessmentCategoriesBLL();
        IDailyAssessmentType repoAssessmentType = new DailyAssessmentTypeBLL();
        IDailyAssessmentSubType repoAssessementSubType = new DailyAssessmentSubTypeBLL();
        IDailyAssessmentOperation repoOperation = new DailyAssessmentOperationBLL();
        // GET: /StudentReport/
        //Get Assessment Categories
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
        // Get Assessment Types
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
        // Student General Assessment
        [HttpGet]
        public ActionResult GetALLStudentGeneralAssessment(int? AcadmicClassId, int? StudentId, string CreateDate)
        {
            
           return View(repoAssessementSubType.GetStudentGeneralAssessment(AcadmicClassId, StudentId, CreateDate).ToList()); 
        }
        [HttpGet]
        public ActionResult AddChangesAssessmentReport(int? AcadmicClassId, int? StudentId, string CreateDate)
        {
            DailyAssessmentHelper myModel = new DailyAssessmentHelper();
            myModel.ParentAssessments = repoAssessmentType.GetALLAssignedParentAssessments();
            if (AcadmicClassId > 0)
            {
                List<DailyAssessmentSubType> GetALLSubAssessments = repoAssessementSubType.GetStudentSingleGeneralAssessment(AcadmicClassId, StudentId, CreateDate).ToList();
                if (GetALLSubAssessments[0].OperationalId > 0)
                {
                    myModel.ChildAssessments = GetALLSubAssessments;
                   
                }
                return View(myModel);
            }
            else
            {
                myModel.ChildAssessments = repoAssessementSubType.GetAllAssessmentSubType();
                return View(myModel);
            }
        }
        [HttpPost]
        public ActionResult AddChangesAssessmentReport(DailyAssessmentHelper helper, int AcadmicClassId, int StudentId)
        {
            var userloggedId = User.Identity.GetUserId();
            for (int i = 0; i < helper.ChildAssessments.Count; i++)
            {
                DailyAssessmentOperation op = new DailyAssessmentOperation();
                op.AcadmicClassId = AcadmicClassId;
                op.StudentId = StudentId;
                op.ParentAssessmentId = helper.ChildAssessments[i].AssessmentTypeId;
                op.AssessmentSubTypeId = helper.ChildAssessments[i].AssessmentSubTypeId;
                op.AssementStatus = helper.ChildAssessments[i].SelectedEvaluation;
                op.WorseConsequence = helper.ChildAssessments[i].Concequence;
                op.AssessmentFormat = helper.ChildAssessments[i].AssessmentFormat; // Assessment format refer to consequence or non-consequence
                op.CreateDate = helper.ChildAssessments[i].CreateDate;
                op.CreatedById = helper.ChildAssessments[i].CreatedById;
                if (helper.ChildAssessments[i].OperationalId == 0)
                {
                    op.CreatedById = userloggedId;
                }
                else
                {
                    op.DailyAssessmentOpertationId = helper.ChildAssessments[i].OperationalId;
                    op.ModifiedById = userloggedId;
                    op.ModifiedDate = DateTime.Now;
                }
                 int getStatus = repoOperation.AddChangesAssessmentOperation(op);
            }

            return RedirectToAction("GetALLStudentGeneralAssessment");
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
                ViewData["DDLAssessment"] = new SelectList(repoAssessmentType.GetAllAssessmentType().Where(x => x.AssessmentCategoryId == AssessmentCategoryId).OrderBy(c => c.AssessmentTypeId).ToList(), "AssessmentTypeId", "AssessmentName");
                return View("../DropDownLists/DDLAssessments");
            }


        }

        public string GetAssessmentCriteria(int AssessmentCategoryId, int AssessmentTypeId)
        {
            string getCriteria = string.Empty;
            var GetAssessmentCriteria = from x in repoAssessmentType.GetAllAssessmentType()
                                        where x.AssessmentCategoryId == AssessmentCategoryId
                                        && x.AssessmentTypeId == AssessmentTypeId
                                        select x.AssessmentCriteria;
            foreach (string item in GetAssessmentCriteria)
            {
                getCriteria = item;
            }
            return getCriteria;
        }
    }
}