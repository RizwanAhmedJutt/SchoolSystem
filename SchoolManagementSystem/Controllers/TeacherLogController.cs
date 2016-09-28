using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SMSDataContract.Common;

namespace SchoolManagementSystem.Controllers
{
    public class TeacherLogController : Controller
    {
        ITeacherRepositry repoTeacher = new TeacherRepositry();
        ITeacherLessonPlan repoTeacherLessonPlan = new TeacherLessonBLL();
        ICourse courserepositry = new CourseBLL();
        IAcadmicClassBLL repClass = new AcadmicClassBLL();
        ITeacherAssignedCourse repoTeacherAssignCourse = new TeacherAssignCourseBLL();
        IDailyAssessmentSubType repoAssessementSubType = new DailyAssessmentSubTypeBLL();
        IDailyAssessmentType repoAssessmentType = new DailyAssessmentTypeBLL();
        IDailyAssessmentOperation repoOperation = new DailyAssessmentOperationBLL();
        IAcadmicAssessmentOperation repAcOperation = new AcadmicAssessmentOperationBLL();
        // GET: TeacherLog
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetTeacherLessonPlans(int? AcadmicClassId, int? TeacherId, int? CourseId)
        {
            return View(repoTeacherLessonPlan.GetTeacherLessons(AcadmicClassId, TeacherId, CourseId));
        }
        [HttpGet]
        public ActionResult AddChangesTeacherLessonPlans(int Id)
        {
            TeacherLessonPlan tlp = new TeacherLessonPlan();
            if (Id == 0)
            {

                return View(tlp);
            }
            else
            {
                tlp = repoTeacherLessonPlan.GetTeacherLessonPlan(Id);
                return View(tlp);
            }
        }
        [HttpPost]
        public ActionResult AddChangesTeacherLessonPlans(TeacherLessonPlan lessonPlan)
        {
            var userIdentityId = User.Identity.GetUserId();
            if (lessonPlan.TeacherLessonPlanId > 0)
            {
                lessonPlan.ModifiedById = userIdentityId;
                lessonPlan.ModifiedDate = DateTime.Now;

            }
            else
            {
                lessonPlan.CreatedById = userIdentityId;
                lessonPlan.CreateDate = DateTime.Now;
            }
            int getStatus = repoTeacherLessonPlan.AddChangesLessonPlan(lessonPlan);
            return RedirectToAction("GetTeacherLessonPlans");
        }  

        // Reports
        // Student General Assessment
        [HttpGet]
        public ActionResult GetALLStudentGeneralAssessment(int? AcadmicClassId, int? StudentId, string CreateDate)
        {
            CreateDate = string.IsNullOrEmpty(CreateDate) ? DateTime.Now.ToString() : CreateDate;
            return View(repoAssessementSubType.GetStudentGeneralAssessment(AcadmicClassId, StudentId, Convert.ToDateTime(CreateDate)).ToList());
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
        // Student Acadmic Assessment
        public ActionResult GetALLStudentAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, string CreateDate)
        {
            return View(repoAssessementSubType.GetStudentAcadmicAssessment(AcadmicClassId, StudentId, CourseId, CreateDate).ToList());
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
        // Dropdowns
        public ActionResult DDLStudent(int AcadmicClassId)
        {
            if (AcadmicClassId > 0)
            {
                IStudent std = new StudentBLL();
                ViewData["DDLStudent"] = new SelectList(std.GetALLStudentByClass(AcadmicClassId).OrderBy(c => c.StudentId).ToList(), "StudentId", "StudentName");
                return View("../DropDownLists/DDLStudent");
            }
            else
            {
                IStudent std = new StudentBLL();
                List<Student> students = new List<Student>();
                var getStudent = std.GetAllStudentByName().Where(x => x.IsActive == true).FirstOrDefault();
                students.Add(getStudent);
                ViewData["DDLStudent"] = new SelectList(students.ToList(), "StudentId", "StudentName");
                return View("../DropDownLists/DDLStudent");
            }

        }

        public ActionResult DDLTeacher(int AcadmicClassId)
        {
            try
            {
                if (AcadmicClassId > 0)
                {
                    ViewData["DDLTeacher"] = new SelectList(repoTeacher.GetTeacherByClass(AcadmicClassId).OrderBy(c => c.TeacherId).ToList(), "TeacherId", "TeacherName");
                    return View("../DropDownLists/DDLTeacher");
                }
                else
                {

                    ViewData["DDLTeacher"] = new SelectList(repoTeacher.GetALLTeacherByName().OrderBy(c => c.TeacherId).ToList(), "TeacherId", "TeacherName");
                    return View("../DropDownLists/DDLTeacher");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public ActionResult DDLTeacherAssignedCourse(int TeacherId, int AcadmicClassId)
        {
            try
            {

                ViewData["DDLCourse"] = new SelectList(repoTeacherAssignCourse.GetTeacherAssignedCourseByAcadmicClass(TeacherId, AcadmicClassId).OrderBy(c => c.CourseId).ToList(), "CourseId", "CourseName");
                return View("../DropDownLists/DDLCourse");

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult DDLTeacherAssignedClass(int TeacherId)
        {
            List<AcadmicClass> ObjClass = new List<AcadmicClass>();

            try
            {

                ViewData["DDLAcadmicClass"] = new SelectList(repClass.GetTeacherAssignedAcadmicClassies(TeacherId).OrderBy(c => c.AcadmicClassId).ToList(), "AcadmicClassId", "ClassName");
                return View("../DropDownLists/DDLTeacherAssignedClass");

                //else
                //{
                //    List<Course> courses = new List<Course>();
                //    var query = (from x in courserepositry.GetALLCourse().ToList()
                //                 where x.IsActive == true
                //                 select x).FirstOrDefault();
                //    courses.Add(query);
                //    ViewData["DDLAcadmicClass"] = new SelectList(courses.ToList(), "AcadmicClassId", "ClassName");
                //    return View("../DropDownLists/DDLCourse");
                //}
            }
            catch (Exception)
            {

                throw;
            }
        }




    }
}