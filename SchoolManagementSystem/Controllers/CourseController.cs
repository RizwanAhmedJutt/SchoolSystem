using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;

namespace SchoolManagementSystem.Controllers
{
    [Authorize(Roles="Admin")]
    public class CourseController : Controller
    {
        ICourse courserepositry = new CourseBLL();
        IStudentAssignCourse stdAssignCourserepo = new StudentAssignCourseBLL();
        ITeacherAssignedCourse teacherrepo = new TeacherAssignCourseBLL();
        [HttpGet]
        public ActionResult GetALLCourse(int? page)
        {

            return View(courserepositry.GetALLCourse().ToPagedList(page ?? 1, 10));
        }
        [HttpGet]
        public ActionResult AddChangesCourse(int Id)
        {
            Course c;

            if (Id == 0)
            {
                Course course = new Course();

                return View(course);
            }
            else
            {

                c = courserepositry.GetCourseDetailByCourseId(Id);
                return View(c);
            }



        }
        [HttpPost]
        public ActionResult AddChangesCourse(Course c, int AcadmicClassId)
        {
            var userloggedId = User.Identity.GetUserId();
            if (c.CourseId > 0)
            {
                c.ModifiedById = userloggedId;
                c.ClassId = AcadmicClassId;
                c.ModifiedDate = DateTime.Now;

            }
            else
            {
                c.CreatedById = userloggedId;
                c.ClassId = AcadmicClassId;


            }

            int courseId = courserepositry.CourseAddChanges(c);


            return RedirectToAction("GetALLCourse", "Course");

        }
        [HttpGet]
        public ActionResult GetALLStudentAssignCourse()
        {
            List<StudentAssignedCourse> stdassignCourse = stdAssignCourserepo.GetStudentAssignedCourse();
            return View(stdassignCourse);
        }
        [HttpGet]
        public ActionResult AddChangesStudentAssignCourse(int id)
        {
            var userloggedId = User.Identity.GetUserId();
            StudentAssignedCourse stdAssignCourse;
            stdAssignCourse = stdAssignCourserepo.GetStudentAssignedCourseById(id);
            if (id == 0)
            {
                StudentAssignedCourse stdAssigncourse = new StudentAssignedCourse();
                stdAssignCourse.CreatedById = userloggedId;

                return View(stdAssigncourse);
            }
            {
                stdAssignCourse.ModifiedById = userloggedId;
                stdAssignCourse.ModifiedDate = DateTime.Now;
                return View(stdAssignCourse);
            }
        }
        [HttpPost]
        public ActionResult AddChangesStudentAssignCourse(StudentAssignedCourse stdCourse, int CourseId, int StudentId, int AcadmicClassId)
        {
            stdCourse.CourseId = CourseId;
            stdCourse.StudentId = StudentId;
            stdCourse.AcadmicClassId = AcadmicClassId;
            int getReturnValue = stdAssignCourserepo.InsertUpdateAssignedCourseAddChanges(stdCourse);
            return RedirectToAction("GetALLStudentAssignCourse", "Course");
        }
        [HttpGet]
        public ActionResult GetALLTeacherAssignCourse()
        {
            List<TeacherAssignedCourse> objTeacherAssignCourse = teacherrepo.GetTeacherAssignedCourse();
            return View(objTeacherAssignCourse);
        }
        [HttpGet]
        public ActionResult AddChangesTeacherAssignCourse(int Id)
        {
            TeacherAssignedCourse assignCourse = teacherrepo.GetTeacherAssignedCourseById(Id);
            if (assignCourse.TeacherAssignedCourseId == 0)
            {
                TeacherAssignedCourse tassignCourse = new TeacherAssignedCourse();
                return View(tassignCourse);
            }
            else
            {
                return View(assignCourse);
            }
        }
        [HttpPost]
        public ActionResult AddChangesTeacherAssignCourse(TeacherAssignedCourse tAssignCourse, int CourseId, int TeacherId, int AcadmicClassId)
        {
            var userloggedId = User.Identity.GetUserId();
            tAssignCourse.TeacherId = TeacherId;
            tAssignCourse.CourseId = CourseId;
            tAssignCourse.ClassId = AcadmicClassId;
            tAssignCourse.CreatedById = userloggedId;
            int getStatus = teacherrepo.InsertUpdateAssignedCourseAddChanges(tAssignCourse);

            return RedirectToAction("GetALLTeacherAssignCourse", "Course");

        }

        public ActionResult DeleteCourse(int CourseId)
        {
            var userloggedId = User.Identity.GetUserId();
            Course c = courserepositry.GetCourseDetailByCourseId(CourseId);
            c.IsActive = false;
            c.ModifiedById = userloggedId;
            c.ModifiedDate = DateTime.Now;
            int getStatus = courserepositry.DeleteCourse(c);
            return RedirectToAction("GetALLCourse", "Course");
        }

        public ActionResult DDLCourse(int AcadmicClassId)
        {
            try
            {
                if (AcadmicClassId > 0)
                {
                    ViewData["DDLCourse"] = new SelectList(courserepositry.GetALLCourseByAcadmicClassId(AcadmicClassId).OrderBy(c => c.CourseId).ToList(), "CourseId", "CourseName");
                    return View("../DropDownLists/DDLCourse");
                }
                else
                {
                    ViewData["DDLCourse"] = new SelectList(courserepositry.GetALLCourse().OrderBy(c => c.CourseId).ToList(), "CourseId", "CourseName");
                    return View("../DropDownLists/DDLCourse");
                }
            }
            catch (Exception)
            {

                throw;
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
                ViewData["DDLStudent"] = new SelectList(std.GetAllStudentByName().OrderBy(c => c.StudentId).ToList(), "StudentId", "StudentName");
                return View("../DropDownLists/DDLStudent");
            }

        }
    }
}