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
        IStudentResultSheet stdResultSheetRepo = new StudentResultSheetBLL();
        ICourse courseRepo = new CourseBLL();
        IStudentAttendance stdAttendanceRepo = new StudentAttendanceBLL();
        IStudentResultSocialAndPersonalSkill socialSkillRepo = new StudentResultSocialAndPersonalSkillBLL();
        IStudentResultSocialDescription socialDescriptionRepo = new StudentResultSocialDescriptionBLL();
        IStudentResultWorkAndSkill studySkillRepo = new StudentResultWorkAndSkillBLL();
        IStudentResultStudyDescription studyDescriptionRepo = new StudentResultStudyDescriptionBLL();
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
        public ActionResult GetALLStudentGeneralAssessment(int? AcadmicClassId, int? StudentId, DateTime StartDate,DateTime EndDate)
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
        public ActionResult DeleteStudentGeneralAssessment(int AcadmicClassId, int StudentId, string CreateDate)
        {
            CreateDate = string.IsNullOrEmpty(CreateDate) ? DateTime.Now.ToString() : CreateDate;
            int DeleteStatus = repoAssessementSubType.DeleteStudentGeneralAssessment(AcadmicClassId, StudentId, Convert.ToDateTime(CreateDate));
            return RedirectToAction("GetALLStudentGeneralAssessment");
        }
        // Student Acadmic Assessment
        public ActionResult GetALLStudentAcadmicAssessment(int? AcadmicClassId, int? StudentId, int? CourseId, DateTime StartDate,DateTime EndDate)
        {
            return View(repoAssessementSubType.GetStudentAcadmicAssessment(AcadmicClassId, StudentId, CourseId, StartDate,EndDate).ToList());
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
        public ActionResult DeleteStudentAcadmicAssessment(int AcadmicClassId, int StudentId, int CourseId, string CreateDate)
        {
            int DeleteStatus = repoAssessementSubType.DeleteStudentAcadmicAssessment(AcadmicClassId, StudentId, CourseId, CreateDate);
            return RedirectToAction("GetALLStudentAcadmicAssessment");
        }
        // Student Results
        public ActionResult AddChangesStudentResult()
        {
            StudentResultSheet srs = new StudentResultSheet();

            return View(srs);
        }
        [HttpGet]
        public ActionResult ResultTemplate(int AcadmicClassId, int StudentId, string PaperTerm)
        {
            var ResultQuery = (from x in stdResultSheetRepo.GetStudentResultSheet().ToList()
                               where x.AcadmicClassId == AcadmicClassId && x.StudentId == StudentId
                               && x.PaperTerm == PaperTerm
                               select x).ToList();
            if (ResultQuery.Count > 0)
            {
                var ResultSheetCourse = (from x in courseRepo.GetALLCourse().ToList()
                                         join y in ResultQuery.ToList() on x.CourseId equals y.CourseId
                                         select new StudentAssignedCourse() { CourseId = x.CourseId, CourseName = x.CourseName, AcadmicClassId = y.AcadmicClassId, StudentId = y.StudentId })
                                         .ToList();
                var StudentAttendance = (from x in stdAttendanceRepo.GetStudentAttendanceSheet().ToList()
                                         where x.AcadmicClassId == AcadmicClassId && x.StudentId == StudentId
                                         && x.PaperTerm == PaperTerm
                                         select x
                                            ).ToList();
                var StdSocialskill = (from x in socialSkillRepo.GetStudentSocialAndPersonalSkill().ToList()
                                      join y in socialDescriptionRepo.GetALLSocailDescriptions().ToList() on x.SocialDescriptionId equals y.SocialDescriptionId
                                      where x.AcadmicClassId == AcadmicClassId && x.StudentId == StudentId
                                      && x.TermType == PaperTerm
                                      select new StudentResultSocialAndPersonalSkill()
                                      {
                                          SocialSkillId = x.SocialSkillId,
                                          TermType = x.TermType,
                                          AcadmicClassId = x.AcadmicClassId,
                                          StudentId = x.StudentId,
                                          SocialDescriptionId = x.SocialDescriptionId,
                                          Description = y.Description,
                                          Grad = x.Grad,
                                          CreatedById = x.CreatedById,
                                          CreatedDate = x.CreatedDate,
                                          ModifiedById = x.ModifiedById,
                                          ModifiedDate = x.ModifiedDate
                                      }
                                         ).ToList();

                var stdWorkskill = (from x in studySkillRepo.GetStudentWorkAndStudySkill().ToList()
                                    join y in studyDescriptionRepo.GetALLStudyDescriptions().ToList() on x.StudyDescriptionId equals y.StudyDescriptionId
                                    where x.AcadmicClassId == AcadmicClassId && x.StudentId == StudentId
                                    && x.TermType == PaperTerm
                                    select new StudentResultWorkAndStudySkill()
                                    {
                                        WorkSkillId = x.WorkSkillId,
                                        TermType = x.TermType,
                                        AcadmicClassId = x.AcadmicClassId,
                                        StudentId = x.StudentId,
                                        StudyDescriptionId = x.StudyDescriptionId,
                                        Description = y.Description,
                                        Grade = x.Grade,
                                        CreatedById = x.CreatedById,
                                        CreatedDate = x.CreatedDate,
                                        ModifiedById = x.ModifiedById,
                                        ModifiedDate = x.ModifiedDate
                                    }

                                    ).ToList();
                ResultSheetHelper rsh = new ResultSheetHelper();
                rsh.AssignedCourses = ResultSheetCourse;
                rsh.StudentResultSheet = ResultQuery;
                rsh.StudentAttendance = StudentAttendance.FirstOrDefault();
                rsh.SrSocialAndPersonalSkill = StdSocialskill;
                rsh.SrWorkAndStudySkill = stdWorkskill;
                return View(rsh);
            }
            else
            {
                IStudentAssignCourse saC = new StudentAssignCourseBLL();
                var query = (from x in saC.GetStudentAssignedCourse().ToList()
                             where x.AcadmicClassId == AcadmicClassId
                             && x.StudentId == StudentId
                             select new StudentAssignedCourse() { CourseId = x.CourseId, CourseName = x.CourseName, AcadmicClassId = x.AcadmicClassId, StudentId = x.StudentId }).ToList();

                var SocailSkills = (from x in socialDescriptionRepo.GetALLSocailDescriptions().ToList()
                                    select new StudentResultSocialAndPersonalSkill() { SocialDescriptionId = x.SocialDescriptionId, Description = x.Description }
                                       ).ToList();

                var StudySkills = (from x in studyDescriptionRepo.GetALLStudyDescriptions().ToList()
                                   select new StudentResultWorkAndStudySkill() { StudyDescriptionId = x.StudyDescriptionId, Description = x.Description }).ToList();

                ResultSheetHelper rsh = new ResultSheetHelper();
                rsh.AssignedCourses = query.ToList();
                for (int i = 0; i < query.Count; i++)
                {
                    StudentResultSheet srsheet = new StudentResultSheet();
                    rsh.StudentResultSheet.Add(srsheet);
                }
                rsh.SrSocialAndPersonalSkill = SocailSkills;
                rsh.SrWorkAndStudySkill = StudySkills;
                rsh.StudentAttendance.StudentId = StudentId;
                rsh.StudentAttendance.AcadmicClassId = AcadmicClassId;
                rsh.PaperTerm = PaperTerm;
                return View(rsh);
            }
        }
        [HttpPost]
        public ActionResult ResultTemplate(ResultSheetHelper rsHelper)
        {
            var loggedUser = User.Identity.GetUserId();
            // Add Subjects result Record..
            for (int i = 0; i < rsHelper.AssignedCourses.Count; i++)
            {
                if (rsHelper.StudentResultSheet[i].StudentResultId == 0)
                {
                    StudentResultSheet srs = new StudentResultSheet()
                    {

                        StudentId = rsHelper.AssignedCourses[i].StudentId,
                        AcadmicClassId = rsHelper.AssignedCourses[i].AcadmicClassId,
                        CourseId = rsHelper.AssignedCourses[i].CourseId,
                        ClassAssessmentPercentage = rsHelper.StudentResultSheet[i].ClassAssessmentPercentage,
                        PaperPercentage = rsHelper.StudentResultSheet[i].PaperPercentage,
                        Grade = rsHelper.StudentResultSheet[i].Grade,
                        Remarks = rsHelper.StudentResultSheet[i].Remarks,
                        CreatedById = loggedUser,
                        CreatedDate = DateTime.Now,
                        PaperTerm = rsHelper.PaperTerm
                    };
                    int getStatus = stdResultSheetRepo.AddChangesStudentResultSheet(srs);
                }

            }
            // Add Student Attnedance
            StudentAttendance stdAttendance = new StudentAttendance();
            if (rsHelper.StudentAttendance.StudentAttendanceId == 0)
            {
                stdAttendance.PaperTerm = rsHelper.PaperTerm;
                stdAttendance.AcadmicClassId = rsHelper.StudentAttendance.AcadmicClassId;
                stdAttendance.StudentId = rsHelper.StudentAttendance.StudentId;
                stdAttendance.WorkingDays = rsHelper.StudentAttendance.WorkingDays;
                stdAttendance.Leaves = rsHelper.StudentAttendance.Leaves;
                stdAttendance.Absents = rsHelper.StudentAttendance.Absents;
                stdAttendance.TotalPercentage = rsHelper.StudentAttendance.TotalPercentage;

                int getStatus = stdAttendanceRepo.AddChangesStudentAttendance(stdAttendance);
            }
            // Social And Personal Skills
            for (int l = 0; l < rsHelper.SrSocialAndPersonalSkill.Count; l++)
            {
                if (rsHelper.SrSocialAndPersonalSkill[l].SocialSkillId == 0)
                {
                    StudentResultSocialAndPersonalSkill sps = new StudentResultSocialAndPersonalSkill();
                    sps.AcadmicClassId = rsHelper.StudentAttendance.AcadmicClassId;
                    sps.StudentId = rsHelper.StudentAttendance.StudentId;
                    sps.TermType = rsHelper.PaperTerm;
                    sps.SocialDescriptionId = rsHelper.SrSocialAndPersonalSkill[l].SocialDescriptionId;
                    sps.Grad = rsHelper.SrSocialAndPersonalSkill[l].Grad;
                    sps.CreatedById = loggedUser;
                    sps.CreatedDate = DateTime.Now;

                    int getStatus = socialSkillRepo.AddChangesStudentResultSocialAndPersonalSkill(sps);
                }
            }
            for (int x = 0; x < rsHelper.SrWorkAndStudySkill.Count; x++)
            {
                if (rsHelper.SrWorkAndStudySkill[x].WorkSkillId == 0)
                {
                    StudentResultWorkAndStudySkill wss = new StudentResultWorkAndStudySkill();
                    wss.AcadmicClassId = rsHelper.StudentAttendance.AcadmicClassId;
                    wss.StudentId = rsHelper.StudentAttendance.StudentId;
                    wss.TermType = rsHelper.PaperTerm;
                    wss.StudyDescriptionId = rsHelper.SrWorkAndStudySkill[x].StudyDescriptionId;
                    wss.Grade = rsHelper.SrWorkAndStudySkill[x].Grade;
                    wss.CreatedById = loggedUser;
                    wss.CreatedDate = DateTime.Now;
                    int getstatus = studySkillRepo.AddChangesStudentWorkAndStudySkill(wss);
                }
            }


            return RedirectToAction("AddChangesStudentResult");
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