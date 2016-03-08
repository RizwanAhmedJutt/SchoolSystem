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
    public class StudentController : Controller
    {
        IStudent student = new StudentBLL();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StudentList()
        {
            List<Student> list = new List<Student>();
            list = student.GetAllStudents();
            return View(list);
        }
        [HttpGet]
        public ActionResult AddChangesStu(int id)
        {
            //id = 0;
            //Student std;
            var userloggedId = User.Identity.GetUserId();
            if (id == 0)
            {
                Student stu = new Student();
                stu.CreatedById = userloggedId;
                return View(stu);
            }
            else
            {
                Student std = new Student();
                std.StudentId = id;
                std = student.GetStudentById(id);
                return View(std);
            }
        }
        [HttpPost]
        public ActionResult AddChangesStu(Student std)
        {
            int getStudentValue = student.StudentAddChanges(std);
            return View(getStudentValue);
        }
        [HttpGet]
        public ActionResult GardianDetail()
        {

            GuardianDetail grdianDetail = new GuardianDetail();
            return View(grdianDetail);
        }
        public ActionResult AdmissionGranted()
        {
            AdmissionGranted admGrant = new AdmissionGranted();
            //admGrant.AdmissionId = 0;
            return View(admGrant);
        }

        public ActionResult StudentContacts()
        {
            GuardianContacts contact = new GuardianContacts();
            return View(contact);
        }
        public ActionResult StudentAddress()
        {
            StudentAddress stdAdd = new StudentAddress();
            return View(stdAdd);
        }

        public ActionResult PreviousAcadmicRecord()
        {
            ST_PreviousAcadmicDetail Stu_PreAcadmicReco = new ST_PreviousAcadmicDetail();
            return View(Stu_PreAcadmicReco);
        }

        public ActionResult StudentProfile()
        {
            StudentProfile studProfile = new StudentProfile();

            return View(studProfile);
        }
        public ActionResult DeleteStudent()
        {
            return View();
        }
    }
}