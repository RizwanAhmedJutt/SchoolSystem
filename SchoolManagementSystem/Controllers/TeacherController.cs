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
    public class TeacherController : Controller
    {
        ITeacherRepositry repoTeacher = new TeacherRepositry();
        // GET: Teacher
        public ActionResult TeacherList()
        {
            List<Teacher> list = new List<Teacher>();
           list =  repoTeacher.GetAllStudents();
           return View(list);
        }

        public ActionResult Teacher(int Id)
        {
            Teacher teacher = new Teacher();
            teacher.TeacherId = 0;
            return View(teacher);
        }

        [HttpPost]
        public ActionResult Teacher(Teacher teach, int Id)
        {
            var userIdentityId = User.Identity.GetUserId();
            if (teach.TeacherId > 0)
            {
                teach.CreatedById = userIdentityId;
                teach.CreatedDate = DateTime.Now;
                teach.ModifiedById = userIdentityId;
                teach.ModifiedDate = DateTime.Now;
                repoTeacher.InsertUpdateTeacher(teach);
            }
            else
            {
                teach.CreatedById = userIdentityId;
                teach.CreatedDate = DateTime.Now;
                //teach.ModifiedById = userIdentityId;
                teach.ModifiedDate = DateTime.Now;
                repoTeacher.InsertUpdateTeacher(teach);
            }
            //return View(teach);

            return RedirectToAction("TeacherAddress", "Teacher", new { Id = teach.TeacherId });
        }

        public ActionResult TeacherAddress(int Id)
        {
            TeacherAddress teacherAdd = new TeacherAddress();
            teacherAdd.TAddressId = 0;
            return View(teacherAdd);
        }

        [HttpPost]
        public ActionResult TeacherAddress(TeacherAddress teachAdd)
        {
            TeacherAddress teacherAdd = new TeacherAddress();
            teacherAdd.TAddressId = 0;
            return View(teacherAdd);
        }
        public ActionResult TeacherAssignCourse()
        {
            return View();
        }
        public ActionResult TeacherContacts()
        {
            TeacherContact contacts = new TeacherContact();

            return View(contacts);

        }
        public ActionResult TeacherProfile()
        {
            return View();
        }

    }
}