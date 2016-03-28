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
            
            return View(courserepositry.GetALLCourse().ToPagedList(page??1,10));
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
            stdAssignCourse = stdAssignCourserepo.GetAssignedCourseByStudentId(id);
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
        public ActionResult AddChangesStudentAssignCourse(StudentAssignedCourse stdCourse,int CourseId,int StudentId)
        {
            stdCourse.CourseId = CourseId;
            stdCourse.StudentId = StudentId;
            int getReturnValue = stdAssignCourserepo.InsertUpdateAssignedCourseAddChanges(stdCourse);
            return RedirectToAction("GetALLCourse", "Course");
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
            TeacherAssignedCourse tassignCourse = new TeacherAssignedCourse();
            return View(tassignCourse);
        }
        [HttpPost]
        public ActionResult AddChangesTeacherAssignCourse(TeacherAssignedCourse tAssignCourse, int CourseId, int TeacherId)
        {
            var userloggedId = User.Identity.GetUserId();
            tAssignCourse.TeacherId = TeacherId;
            tAssignCourse.CourseId = CourseId;
            tAssignCourse.CreatedById = userloggedId;
            int getStatus = teacherrepo.InsertUpdateAssignedCourseAddChanges(tAssignCourse);

            return RedirectToAction("GetALLCourse", "Course");

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


    }
}