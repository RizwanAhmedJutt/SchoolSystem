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

namespace SchoolManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        ICourse courserepositry = new CourseBLL();
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


            return View();

        }
         

    }
}