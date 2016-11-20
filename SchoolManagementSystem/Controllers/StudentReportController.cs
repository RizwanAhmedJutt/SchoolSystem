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
using System.Text;

namespace SchoolManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentReportController : Controller
    {
        IAssessmentCategories repoAssessmentCategory = new AssessmentCategoriesBLL();
        IDailyAssessmentType repoAssessmentType = new DailyAssessmentTypeBLL();
        IDailyAssessmentSubType repoAssessementSubType = new DailyAssessmentSubTypeBLL();
        IDailyAssessmentOperation repoOperation = new DailyAssessmentOperationBLL();
        IAcadmicAssessmentOperation repAcOperation = new AcadmicAssessmentOperationBLL();
        ITeacherAssessmentOperation repTAOperation = new TeacherAssessmentOperationBLL();
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
                return View(subassessmen);
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
        public ActionResult GetALLStudentGeneralAssessment(int? AcadmicClassId, int? StudentId, DateTime? StartDate, DateTime? EndDate)
        {
            StartDate = StartDate == null ? DateTime.Now : (DateTime)StartDate;
            EndDate = EndDate == null ? DateTime.Now : (DateTime)EndDate;
            return View(repoAssessementSubType.GetStudentGeneralAssessment(AcadmicClassId, StudentId,(DateTime)StartDate,(DateTime)EndDate).ToList());
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

        public ActionResult DeleteStudentGeneralAssessment(int AcadmicClassId, int StudentId, DateTime CreatedDate)
        {
            int DeleteStatus = repoAssessementSubType.DeleteStudentGeneralAssessment(AcadmicClassId, StudentId, CreatedDate);
            return RedirectToAction("GetALLStudentGeneralAssessment");
        }

        // Student Acadmic Assessment
        public ActionResult GetALLStudentAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, DateTime? StartDate, DateTime? EndDate)
        {
            StartDate = StartDate == null ? DateTime.Now : (DateTime)StartDate;
            EndDate = EndDate == null ? DateTime.Now : (DateTime)EndDate;
            return View(repoAssessementSubType.GetStudentAcadmicAssessment(AcadmicClassId, StudentId, CourseId,(DateTime) StartDate,(DateTime)EndDate).ToList());
        }

        [HttpGet]
        public ActionResult AddChangesAcadmicAssessmentReport(int? AcadmicClassId, int? StudentId, int? CourseId, string CreateDate)
        {
            DailyAssessmentHelper myModel = new DailyAssessmentHelper();
            if (AcadmicClassId > 0)
            {
                List<DailyAssessmentType> GetALLSubAssessments = repoAssessementSubType.GetStudentSingleAcadmicAssessment(AcadmicClassId, StudentId, CourseId, CreateDate).ToList();
                if (GetALLSubAssessments[0].OperationalId > 0)
                {
                    myModel.ParentAssessments = GetALLSubAssessments;
                }
                return View(myModel);
            }
            else
            {
                myModel.ParentAssessments = repoAssessmentType.GetALLAssignedParentAcadmicAssessments();
                return View(myModel);
            }
        }

        [HttpPost]
        public ActionResult AddChangesAcadmicAssessmentReport(DailyAssessmentHelper helpers, int AcadmicClassId, int StudentId, int CourseId)
        {
            var userloggedId = User.Identity.GetUserId();
            for (int i = 0; i < helpers.ParentAssessments.Count; i++)
            {
                AcadmicAssessmentOperation op = new AcadmicAssessmentOperation();
                op.AcadmicClassId = AcadmicClassId;
                op.StudentId = StudentId;
                op.CourseId = CourseId;
                op.ParentAssessmentId = helpers.ParentAssessments[i].AssessmentTypeId;
                op.AssementStatus = helpers.ParentAssessments[i].SelectedEvaluation;
                op.AverageConsequence = helpers.ParentAssessments[i].AverageConcequence;
                op.WorseConsequence = helpers.ParentAssessments[i].Concequence;
                op.AssessmentFormat = helpers.ParentAssessments[i].AssessmentFormat; // Assessment format refer to consequence or non-consequence
                op.CreateDate = helpers.ParentAssessments[i].CreateDate;
                op.CreatedById = helpers.ParentAssessments[i].CreatedById;
                if (helpers.ParentAssessments[i].OperationalId == 0)
                {
                    op.CreatedById = userloggedId;
                }
                else
                {
                    op.AcadmicAssessmentOperationId = helpers.ParentAssessments[i].OperationalId;
                    op.ModifiedById = userloggedId;
                    op.ModifiedDate = DateTime.Now;
                }
                int getStatus = repAcOperation.AddChangesAcadmicAssessmentOperation(op);
            }

            return RedirectToAction("GetALLStudentAcadmicAssessment");
        }
        public ActionResult DeleteStudentAcadmicAssessment(int AcadmicClassId, int StudentId, int CourseId, DateTime CreateDate)
        {
            int DeleteStatus = repoAssessementSubType.DeleteStudentAcadmicAssessment(AcadmicClassId, StudentId, CourseId,CreateDate);
            return RedirectToAction("GetALLStudentAcadmicAssessment");
        }
        // Teacher General Assessment
        [HttpGet]
        public ActionResult GetALLTeacherGeneralAssessment(int? AcadmicClassId, int? TeacherId, int? CourseId, DateTime? StartDate, DateTime? EndDate)
        {
            StartDate = StartDate == null ? DateTime.Now : (DateTime)StartDate;
            EndDate = EndDate == null ? DateTime.Now : (DateTime)EndDate;
            return View(repoAssessementSubType.GetTeacherAcadmicAssessments(AcadmicClassId, TeacherId, CourseId,(DateTime) StartDate,(DateTime) EndDate).ToList());
        }

        [HttpGet]
        public ActionResult AddChangeTeacherGeneralAssessmentReport(int? AcadmicClassId, int? TeacherId, int? CourseId, string CreateDate)
        {
            DailyAssessmentHelper myModel = new DailyAssessmentHelper();

            if (AcadmicClassId > 0)
            {
                List<DailyAssessmentType> GetALLSubAssessments = repoAssessementSubType.GetTeacherSingleAcadmicAssessment(AcadmicClassId, TeacherId, CourseId, CreateDate);
                if (GetALLSubAssessments[0].OperationalId > 0)
                {
                    myModel.ParentAssessments = GetALLSubAssessments;

                }
                return View(myModel);
            }
            else
            {
                myModel.ParentAssessments = repoAssessmentType.GetALLAssignedParentTeacherAcadmicAssessments();
                return View(myModel);
            }
        }
        [HttpPost]
        public ActionResult AddChangeTeacherGeneralAssessmentReport(DailyAssessmentHelper helpers, int AcadmicClassId, int TeacherId, int CourseId)
        {
            var userloggedId = User.Identity.GetUserId();
            for (int i = 0; i < helpers.ParentAssessments.Count; i++)
            {
                TeacherAssessmentOperation op = new TeacherAssessmentOperation();
                op.AcadmicClassId = AcadmicClassId;
                op.TeacherId = TeacherId;
                op.CourseId = CourseId;
                op.ParentAssessmentId = helpers.ParentAssessments[i].AssessmentTypeId;
                op.AssementStatus = helpers.ParentAssessments[i].SelectedEvaluation;
                op.AverageConsequence = helpers.ParentAssessments[i].AverageConcequence;
                op.WorseConsequence = helpers.ParentAssessments[i].Concequence;
                op.AssessmentFormat = helpers.ParentAssessments[i].AssessmentFormat; // Assessment format refer to consequence or non-consequence
                op.CreateDate = helpers.ParentAssessments[i].CreateDate;
                op.CreatedById = helpers.ParentAssessments[i].CreatedById;
                if (helpers.ParentAssessments[i].OperationalId == 0)
                {
                    op.CreatedById = userloggedId;
                }
                else
                {
                    op.TeacherAssessmentOperationId = helpers.ParentAssessments[i].OperationalId;
                    op.ModifiedById = userloggedId;
                    op.ModifiedDate = DateTime.Now;
                }
                int getStatus = repTAOperation.AddChangesTeacherAssessmentOperation(op);
            }

            return RedirectToAction("GetALLTeacherGeneralAssessment");
        }
        public ActionResult DeleteTeacherAssessment(int AcadmicClassId, int TeacherId, int CourseId, DateTime CreateDate)
        {
            int DeleteStatus = repoAssessementSubType.DeleteTeacherAssessment(AcadmicClassId, TeacherId, CourseId, CreateDate);
            return RedirectToAction("GetALLTeacherGeneralAssessment");
        }
        // Student Monthly Report
        public ActionResult GetStudentReport(int? StudentId, int? AcadmicClassId, string Month)
        {
            StringBuilder courseIDs = new StringBuilder();
            StudentMonthReportHelpers smrh = new StudentMonthReportHelpers();
            if (AcadmicClassId > 0)
            {
                
                 smrh.Courses = repAcOperation.GetStudentAssessmentCourse(StudentId, AcadmicClassId, Month);
                // Get CourseId to fetch only specific courses data.
                courseIDs = repAcOperation.StudentAssessmentCourseIDs(StudentId, AcadmicClassId, Month);
                smrh.GeneralAssessment = repAcOperation.GetStudentGeneralAssessmentResult(StudentId, AcadmicClassId, Month);
                // confirm course must exist against assessment
                if (!string.IsNullOrEmpty(courseIDs.ToString()))
                {
                    smrh.AcadmicAssessment = repAcOperation.GetStudentAssessmentByCourses(StudentId, AcadmicClassId, Month, courseIDs);
                    
                }
                return View(smrh);
            }
            else
            {
                return View(smrh);
            }
        }
        // Teacher Monthly Report
        public ActionResult GetTeacherReport(int? AcadmicClassId,int? TeacherId ,string Month)
        {
            StringBuilder courseIDs = new StringBuilder();
            TeacherMonthReportHelpers tmrh = new TeacherMonthReportHelpers();
            if(AcadmicClassId>0)
            {
                tmrh.Courses = repTAOperation.GetTeacherAssessmentCourse(TeacherId, AcadmicClassId, Month);
                courseIDs = repTAOperation.TeacherAssessmentCourseIDs(TeacherId, AcadmicClassId, Month);
                if(!string.IsNullOrEmpty(courseIDs.ToString()))
                {
                    tmrh.TeacherAssessment = repTAOperation.GetTeacherMonthAssessmentResult(AcadmicClassId, TeacherId, courseIDs, Month);
                    return View(tmrh);
                }
            }
            return View(tmrh);
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
            var GeneralAssessmentTypes = from c in repoAssessmentType.GetAllAssessmentType()
                                         where c.AssessmentCriteria == "General"
                                         select c;
            if (AssessmentCategoryId == 0)
            {
                ViewData["DDLAssessment"] = new SelectList(GeneralAssessmentTypes.OrderBy(c => c.AssessmentTypeId).ToList(), "AssessmentTypeId", "AssessmentName");
                return View("../DropDownLists/DDLAssessments");
            }
            else
            {
                ViewData["DDLAssessment"] = new SelectList(GeneralAssessmentTypes.Where(x => x.AssessmentCategoryId == AssessmentCategoryId).OrderBy(c => c.AssessmentTypeId).ToList(), "AssessmentTypeId", "AssessmentName");
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
        [HttpGet]
        public ActionResult GetTeacherReportByClass(string AcadmicClassId,string month,int? TeacherId)
        {
            StringBuilder courseIDs = new StringBuilder();
            TeacherMonthReportHelpers tmrh = new TeacherMonthReportHelpers();
            if (!string.IsNullOrEmpty(AcadmicClassId))
            {
                tmrh.Courses = repTAOperation.GetTeacherAssessmentCourse(TeacherId, AcadmicClassId, month);
               // courseIDs = repTAOperation.TeacherAssessmentCourseIDs(TeacherId, AcadmicClassId, Month);
                //if (!string.IsNullOrEmpty(courseIDs.ToString()))
                //{
                //    tmrh.TeacherAssessment = repTAOperation.GetTeacherMonthAssessmentResult(AcadmicClassId, TeacherId, courseIDs, Month);
                //    return View(tmrh);
                //}
                tmrh.TeacherAssessment = repTAOperation.GetTeacherMonthAssessmentResultByClass(AcadmicClassId, TeacherId, month);
                return View(tmrh);
            }
            return View(tmrh);
        }

        public JsonResult GetAcadmicClassByTerm(string term)
        {
            IAcadmicClassBLL repClass = new AcadmicClassBLL();
            List<AcadmicClass> ObjClass = new List<AcadmicClass>();
            ObjClass = repClass.GetALLAcadmicClassies().Where(x=>x.ClassName.Contains(term)).Select(x=>x).ToList();
            var JqueryUiFormat = (from c in ObjClass
                                  select new
                                  {
                                      label = c.ClassName,
                                      Value = c.AcadmicClassId
                                  }
                                    ).ToList();
            return Json(JqueryUiFormat, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAcadmicClass()
        {
            IAcadmicClassBLL repClass = new AcadmicClassBLL();
            List<AcadmicClass> ObjClass = new List<AcadmicClass>();
            ObjClass = repClass.GetALLAcadmicClassies().ToList();
            var JqueryUiFormat = (from c in ObjClass
                                  select new
                                  {
                                      label = c.ClassName,
                                      Value = c.AcadmicClassId
                                  }
                                    ).ToList();
            return Json(JqueryUiFormat, JsonRequestBehavior.AllowGet);
        }
    }
}