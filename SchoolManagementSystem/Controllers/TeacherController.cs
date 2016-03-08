using SMSDataContract.Accounts;
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
        public ActionResult StudentList()
        {
            return View();
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