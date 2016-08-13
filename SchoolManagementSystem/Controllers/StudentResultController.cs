using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Script.Serialization;

namespace SchoolManagementSystem.Controllers
{

    [Authorize(Roles = "Admin")]
    public class StudentResultController : Controller
    {
        IStudentResultSheet stdResultSheetRepo = new StudentResultSheetBLL();
        IStudentResultSocialDescription socialDescriptionRepo = new StudentResultSocialDescriptionBLL();
        IStudentResultStudyDescription studyDescriptionRepo = new StudentResultStudyDescriptionBLL();
        IStudentResultSocialAndPersonalSkill socialSkillRepo = new StudentResultSocialAndPersonalSkillBLL();
        IStudentResultWorkAndSkill studySkillRepo = new StudentResultWorkAndSkillBLL();
        IStudentAttendance stdAttendanceRepo = new StudentAttendanceBLL();
        ICourse courseRepo = new CourseBLL();


        public ActionResult Index()
        {
            return View();
        }

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
        [HttpGet]
        public ActionResult GetGeneralResultSheet(int? AcadmicClassId, int? StudentId)
        {
            GeneralResultSheetHelper rsh = new GeneralResultSheetHelper();
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

        // Student Result Social Description
        public ActionResult GetALLSocailDescriptions(int? page)
        {

            return View(socialDescriptionRepo.GetALLSocailDescriptions().ToPagedList(page ?? 1, 10));
        }

        [HttpGet]
        public ActionResult AddChangesSocailDescriptions(int Id)
        {

            if (Id == 0)
            {
                StudentResultSocialDescription socialDescription = new StudentResultSocialDescription();
                return View(socialDescription);
            }
            else
            {
                var query = (from x in socialDescriptionRepo.GetALLSocailDescriptions().ToList()
                             where x.SocialDescriptionId == Id
                             select new StudentResultSocialDescription
                             {
                                 SocialDescriptionId = x.SocialDescriptionId,
                                 Description = x.Description,
                                 CreatedById = x.CreatedById,
                                 CreatedDate = x.CreatedDate,
                                 ModifiedById = x.ModifiedById,
                                 ModifiedDate = x.ModifiedDate
                             }).FirstOrDefault();
                //StudentResultSocialDescription objsocialDescription = query;
                return View(query);


            }
        }
        [HttpPost]
        public ActionResult AddChangesSocailDescriptions(StudentResultSocialDescription socialDescription)
        {
            var userloggedId = User.Identity.GetUserId();
            if (socialDescription.SocialDescriptionId > 0)
            {
                socialDescription.ModifiedById = userloggedId;
                socialDescription.ModifiedDate = DateTime.Now;

            }
            else
            {
                socialDescription.CreatedById = userloggedId;
                socialDescription.CreatedDate = DateTime.Now;
            }

            int getStatus = socialDescriptionRepo.AddChangesSocialDescriptions(socialDescription);
            return RedirectToAction("GetALLSocailDescriptions");
        }

        public string CheckSocailDescriptionsExist(string Description)
        {
            bool result = false;
            var query = (from x in socialDescriptionRepo.GetALLSocailDescriptions().ToList()
                         where x.Description == Description
                         select new StudentResultSocialDescription
                         {
                             SocialDescriptionId = x.SocialDescriptionId,
                             Description = x.Description,
                             CreatedById = x.CreatedById,
                             CreatedDate = x.CreatedDate,
                             ModifiedById = x.ModifiedById,
                             ModifiedDate = x.ModifiedDate
                         }).FirstOrDefault();
            if (query != null)
            {
                if (query.SocialDescriptionId > 0)
                    result = true;
            }
            else
            { result = false; }
            return new JavaScriptSerializer().Serialize(result);

        }
        // Studenet Result Study Description

        public ActionResult GetALLStudyDescriptions(int? page)
        {

            return View(studyDescriptionRepo.GetALLStudyDescriptions().ToPagedList(page ?? 1, 10));
        }

        [HttpGet]
        public ActionResult AddChangesStudyDescriptions(int Id)
        {

            if (Id == 0)
            {
                StudentResultStudyDescription studyDescription = new StudentResultStudyDescription();
                return View(studyDescription);
            }
            else
            {
                var query = (from x in studyDescriptionRepo.GetALLStudyDescriptions().ToList()
                             where x.StudyDescriptionId == Id
                             select new StudentResultStudyDescription
                             {
                                 StudyDescriptionId = x.StudyDescriptionId,
                                 Description = x.Description,
                                 CreatedById = x.CreatedById,
                                 CreatedDate = x.CreatedDate,
                                 ModifiedById = x.ModifiedById,
                                 ModifiedDate = x.ModifiedDate
                             }).FirstOrDefault();
                return View(query);


            }
        }
        [HttpPost]
        public ActionResult AddChangesStudyDescriptions(StudentResultStudyDescription studyDescription)
        {
            var userloggedId = User.Identity.GetUserId();
            if (studyDescription.StudyDescriptionId > 0)
            {
                studyDescription.ModifiedById = userloggedId;
                studyDescription.ModifiedDate = DateTime.Now;

            }
            else
            {
                studyDescription.CreatedById = userloggedId;
                studyDescription.CreatedDate = DateTime.Now;
            }

            int getStatus = studyDescriptionRepo.AddChangesStudyDescriptions(studyDescription);
            return RedirectToAction("GetALLStudyDescriptions");
        }

        public string CheckStudyDescriptionsExist(string Description)
        {
            bool result = false;
            var query = (from x in studyDescriptionRepo.GetALLStudyDescriptions().ToList()
                         where x.Description == Description
                         select new StudentResultStudyDescription
                         {
                             StudyDescriptionId = x.StudyDescriptionId,
                             Description = x.Description,
                             CreatedById = x.CreatedById,
                             CreatedDate = x.CreatedDate,
                             ModifiedById = x.ModifiedById,
                             ModifiedDate = x.ModifiedDate
                         }).FirstOrDefault();
            if (query != null)
            {
                if (query.StudyDescriptionId > 0)
                    result = true;
            }

            return new JavaScriptSerializer().Serialize(result);

        }




    }
}