using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult TeacherList()
        {
            return View();
        }

        public ActionResult Teacher()
        {
            Teacher teacher = new Teacher();
            teacher.TeacherId = 0;
            return View(teacher);
        }

        public ActionResult TeacherAddress()
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