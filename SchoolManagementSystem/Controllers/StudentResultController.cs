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
            if (ResultQuery.Count>0)
            {
                var ResultSheetCourse = (from x in ResultQuery.ToList()
                                         select new StudentAssignedCourse() { CourseId = x.CourseId, AcadmicClassId = x.AcadmicClassId, StudentId = x.StudentId }).ToList();
                ResultSheetHelper rsh = new ResultSheetHelper();
                rsh.AssignedCourses = ResultSheetCourse;
                rsh.StudentResultSheet = ResultQuery;
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
                rsh.SrSocialAndPersonalSkill = SocailSkills;
                rsh.SrWorkAndStudySkill = StudySkills;
                rsh.PaperTerm = PaperTerm;
                return View(rsh);
            }
        }
        [HttpPost]
        public ActionResult ResultTemplate(ResultSheetHelper rsHelper)
        {
            var loggedUser = User.Identity.GetUserId();

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
                    StudentResultSheet srs1 = srs;
                }
              
            }

            return RedirectToAction("AddChangesStudentResult");
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