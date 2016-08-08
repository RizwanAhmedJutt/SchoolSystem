using IdentitySample.Models;
using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using SMSDataContract.Accounts;

namespace SchoolManagementSystem.Controllers
{
    // [Authorize(Roles = "Student,Admin")]

    public class StudentLogController : Controller
    {
       
       
        
        
        IAcadmicAssessmentOperation repAcOperation = new AcadmicAssessmentOperationBLL();        
        IStudentResultSheet stdResultSheetRepo = new StudentResultSheetBLL();
        IStudentResultSocialDescription socialDescriptionRepo = new StudentResultSocialDescriptionBLL();
        IStudentResultStudyDescription studyDescriptionRepo = new StudentResultStudyDescriptionBLL();
        IStudentResultSocialAndPersonalSkill socialSkillRepo = new StudentResultSocialAndPersonalSkillBLL();
        IStudentResultWorkAndSkill studySkillRepo = new StudentResultWorkAndSkillBLL();
        IStudentAttendance stdAttendanceRepo = new StudentAttendanceBLL();
       
        public ActionResult GetStudentReport(string Month)
        {
            StringBuilder courseIDs = new StringBuilder();
            StudentMonthReportHelpers smrh = new StudentMonthReportHelpers();
            basicDetail bd = GetStudentDetail();
            smrh.AcadmicClassId = bd.AcadmicClassId;
            smrh.StudentId = bd.StudentId;
            if (!string.IsNullOrEmpty(Month))
            {

                smrh.Courses = repAcOperation.GetStudentAssessmentCourse(smrh.StudentId, smrh.AcadmicClassId, Month);
                // Get CourseId to fetch only specific courses data.
                courseIDs = repAcOperation.StudentAssessmentCourseIDs(smrh.StudentId, smrh.AcadmicClassId, Month);
                // confirm course must exist against assessment
                if (!string.IsNullOrEmpty(courseIDs.ToString()))
                {
                    smrh.AcadmicAssessment = repAcOperation.GetStudentAssessmentByCourses(smrh.StudentId, smrh.AcadmicClassId, Month, courseIDs);
                    smrh.GeneralAssessment = repAcOperation.GetStudentGeneralAssessmentResult(smrh.StudentId, smrh.AcadmicClassId, Month);
                }
                return View(smrh);
            }
            else
            {
                return View(smrh);
            }
        }
        [HttpGet]
        public ActionResult GetGeneralResultSheet(int? AcadmicClassId, int? StudentId)
        {
            basicDetail bd = GetStudentDetail();
            AcadmicClassId = bd.AcadmicClassId;
            StudentId = bd.StudentId;
            GeneralResultSheetHelper rsh = new GeneralResultSheetHelper();
            rsh.AcadmicClassId = bd.AcadmicClassId;
            rsh.StudentId = bd.StudentId;
            if (AcadmicClassId != null && StudentId != null)
            {
                var attendance = stdAttendanceRepo.GetStudentAttendanceSheet().Where(x => x.AcadmicClassId == AcadmicClassId && x.StudentId == StudentId).Select(x => x).ToList();
                var socialskill = (from x in socialSkillRepo.GetStudentSocialAndPersonalSkill().ToList()
                                   where x.AcadmicClassId == AcadmicClassId && x.StudentId == StudentId
                                   join d in socialDescriptionRepo.GetALLSocailDescriptions().ToList() on x.SocialDescriptionId equals d.SocialDescriptionId
                                   select new StudentResultSocialAndPersonalSkill()
                                   {
                                       Description = d.Description,
                                       Grad = x.Grad,
                                       TermType = x.TermType
                                   }).ToList();
                var workskill = (from x in studySkillRepo.GetStudentWorkAndStudySkill().ToList()
                                 where x.AcadmicClassId == AcadmicClassId && x.StudentId == StudentId
                                 join d in studyDescriptionRepo.GetALLStudyDescriptions().ToList() on x.StudyDescriptionId equals d.StudyDescriptionId
                                 select new StudentResultWorkAndStudySkill()
                                 {
                                     Description = d.Description,
                                     Grade = x.Grade,
                                     TermType = x.TermType
                                 }).ToList();


                rsh.StudentResultSheet = stdResultSheetRepo.GetGeneralStudentResultSheet(AcadmicClassId, StudentId);
                rsh.StudentAttendance = attendance;
                rsh.SrSocialAndPersonalSkill = socialskill;
                rsh.SrWorkAndStudySkill = workskill;
               
                return View(rsh);
            }
            else
            {
                return View(rsh);
            }
        }
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

        public basicDetail GetStudentDetail()
        {
            IStudent stdRepo = new StudentBLL();
            var context = new IdentitySample.Models.ApplicationDbContext();
            var userId = User.Identity.GetUserId();
            var CurrentUserDetail = context.Users.Where(user => user.Id == userId).ToList();
            basicDetail bd = new basicDetail();
            bd.StudentId = CurrentUserDetail.FirstOrDefault().StudentId;
            var query = (from x in stdRepo.GetAllStudents().ToList()
                         where x.StudentId == bd.StudentId
                         select x).FirstOrDefault();
            if (query != null)
                bd.AcadmicClassId = query.AcadmicClassId;
            return bd;


        }
        public class basicDetail
        {
            public int? StudentId { get; set; }
            public int AcadmicClassId { get; set; }
        }

    }
}