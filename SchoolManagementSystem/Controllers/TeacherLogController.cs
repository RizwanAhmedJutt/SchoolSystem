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