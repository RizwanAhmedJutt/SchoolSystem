﻿using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace SchoolManagementSystem.Controllers
{
    [Authorize(Roles="Admin")]
    public class CourseController : Controller
    {
        ICourse courserepositry = new CourseBLL();
        IStudentAssignCourse stdAssignCourserepo = new StudentAssignCourseBLL();
        [HttpGet]
        public ActionResult GetALLCourse()
        {
            List<Course> getCourses = courserepositry.GetALLCourse();
            return View(getCourses);
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
            return View();
        }
    }
}