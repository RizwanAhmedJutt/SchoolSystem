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
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {

        IStudent student = new StudentBLL();
        IStudentAddressBLL studentAddress = new StudentAddressBLL();
        IGuardianDetail guardiainrepositry = new GuardianDetailBLL();
        IGuardianContact gContactrepositry = new GuardianContactBLL();
        IST_PreviousAcadmicRecord previousrepositry = new ST_PreviousAcadmicDetailBLL();
        IAdmissionGranted aGrantedrepositry = new AdmissionGrantedBLL();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StudentList()
        {
            List<Student> getStudentList = student.GetAllStudents();
            return View(getStudentList);
        }
        [HttpGet]
        public ActionResult AddChangesStudent(int id)
        {


            Student std;

            if (id == 0)
            {
                Student stu = new Student();

                return View(stu);
            }
            else
            {

                std = student.GetStudentById(id);
                return View(std);
            }
        }
        [HttpPost]
        public ActionResult AddChangesStudent(Student std, int AcadmicClassId, int ddlSibling, int ddlCurrentSibling)
        {
            var userloggedId = User.Identity.GetUserId();
            if (std.StudentId > 0)
            {
                std.ModifiedById = userloggedId;
                std.AcadmicClassId = AcadmicClassId;
                std.NoOfSibling = ddlSibling;
                std.NoOfSiblingCurrentSchool = ddlCurrentSibling;

            }
            else
            {
                std.CreatedById = userloggedId;
                std.AcadmicClassId = AcadmicClassId;
                std.NoOfSibling = ddlSibling;
                std.NoOfSiblingCurrentSchool = ddlCurrentSibling;

            }

            TempData["getStudentValue"] = student.StudentAddChanges(std);
            TempData.Keep("getStudentValue");

            return RedirectToAction("AddChangesStudentAddress", new { studentId = TempData["getStudentValue"] });

        }
        [HttpGet]
        public ActionResult AddChangesStudentAddress(int studentId)
        {
            StudentAddress objstdAddress;
            objstdAddress = studentAddress.GetStudentAddressByStudentId(studentId);
            if (objstdAddress.StudentAddressId == 0)
            {
                StudentAddress stdAdd = new StudentAddress();
                stdAdd.StudentId = studentId;
                return View(stdAdd);
            }
            else
                return View(objstdAddress);



        }
        [HttpPost]
        public ActionResult AddChangesStudentAddress(StudentAddress stdAddress, int StudentId, int CityId)
        {

            stdAddress.StudentId = StudentId;
            stdAddress.CityId = CityId;
            TempData["getStudentId"] = studentAddress.StudentAddressAddChanges(stdAddress);
            TempData.Keep("getStudentId");
            return RedirectToAction("AddChangesGuardianDetail", new { studentId = Convert.ToInt32(TempData["getStudentId"].ToString()) });
        }
        [HttpGet]
        public ActionResult AddChangesGuardianDetail(int studentId)
        {
            GuardianDetail objguardian;

            objguardian = guardiainrepositry.GetGuardianInfoByStudentId(studentId);
            if (objguardian.StudentGuardianId == 0)
            {
                GuardianDetail gDetail = new GuardianDetail();
                gDetail.StudentId = studentId;
                return View(gDetail);
            }
            else
                return View(objguardian);

        }
        [HttpPost]
        public ActionResult AddChangesGuardianDetail(GuardianDetail gDetails, int StudentId)
        {
            gDetails.StudentId = StudentId;
            int Id = guardiainrepositry.GuardianContactAddChanges(gDetails);
            TempData["getGuardianValue"] = Id;
            return RedirectToAction("AddChangesGuardianContacts", new { studentid = StudentId, guardianId = Convert.ToInt32(TempData["getGuardianValue"]) });

        }
        [HttpGet]
        public ActionResult AddChangesGuardianContacts(int studentid, int guardianId)
        {

            GuardianContacts gContact;
            gContact = gContactrepositry.GetGuardianContactInfoByGuardianId(guardianId);
            if (gContact.GuardianContactId == 0)
            {
                GuardianContacts contact = new GuardianContacts();
                contact.GuardianId = guardianId;
                contact.StudentId = studentid;
                return View(contact);

            }
            else
            {
                gContact.StudentId = studentid;
                return View(gContact);
            }

        }
        [HttpPost]
        public ActionResult AddChangesGuardianContacts(GuardianContacts gContacts, int StudentId)
        {
            int guardianId = gContactrepositry.GuardianContactAddChanges(gContacts);
            return RedirectToAction("AddChangesPreviousAcadmicRecord", new { studentid = StudentId, GuardianId = guardianId });
        }
        [HttpGet]
        public ActionResult AddChangesPreviousAcadmicRecord(int StudentId, int GuardianId)
        {
            ST_PreviousAcadmicDetail previousDetail;
            previousDetail = previousrepositry.GetPreviousAcadmicInfoByStudentId(StudentId);
            if (previousDetail.PAcadmicId == 0)
            {
                ST_PreviousAcadmicDetail Stu_PreAcadmicReco = new ST_PreviousAcadmicDetail();
                Stu_PreAcadmicReco.StudentId = StudentId;
                Stu_PreAcadmicReco.GuardianId = GuardianId;
                return View(Stu_PreAcadmicReco);
            }
            else
            {
                previousDetail.GuardianId = GuardianId;
                return View(previousDetail);
            }
        }
        [HttpPost]
        public ActionResult AddChangesPreviousAcadmicRecord(ST_PreviousAcadmicDetail previousDetail, int StudentId, int AcadmicClassId)
        {
            previousDetail.StudentId = StudentId;
            previousDetail.AcadmicClassId = AcadmicClassId;
            int ReturnStudentId = previousrepositry.PreviousAcadmicDetailAddChanges(previousDetail);
            ST_PreviousAcadmicDetail Stu_PreAcadmicReco = new ST_PreviousAcadmicDetail();
            return RedirectToAction("AddChangesAdmissionGranted", new { studentid = StudentId, GuardianId = previousDetail.GuardianId });
        }
        [HttpGet]
        public ActionResult AddChangesAdmissionGranted(int studentid, int GuardianId)
        {


            AdmissionGranted admisionGranted;
            admisionGranted = aGrantedrepositry.GetAdmissionGrantedInfoByStudentId(studentid);

            if (admisionGranted.AdmissionId == 0)
            {
                AdmissionGranted admGrant = new AdmissionGranted();
                admGrant.StudentId = studentid;
                admGrant.GuardianId = GuardianId;

                return View(admGrant);

            }
            else
            {

                admisionGranted.StudentId = studentid;
                admisionGranted.GuardianId = GuardianId;
                Student std = student.GetStudentById(studentid);
              ViewBag.RollNumber= std.RollNumber;
                return View(admisionGranted);
            }
        }
        [HttpPost]
        public ActionResult AddChangesAdmissionGranted(AdmissionGranted agranted, int StudentId, string AcadmicClassId, int RollNumber)
        {

            var userloggedId = User.Identity.GetUserId();
            // Update Student RollNumber
            Student std = student.GetStudentById(StudentId);
            std.RollNumber = RollNumber;
            std.IsActive = agranted.IsGranted;
            if (std.StudentId > 0)
            {
                int getStudent = student.StudentAddChanges(std);
            }


            agranted.StudentId = StudentId;
            agranted.CreatedById = userloggedId.ToString();
            agranted.AdmissionGrantedForClass = AcadmicClassId;
            int finalValue = aGrantedrepositry.AdmissionAddChanges(agranted);

            return RedirectToAction("StudentList", "Student");


        }



        [HttpGet]
        public ActionResult StudentProfile()
        {
            StudentProfile studProfile = new StudentProfile();

            return View(studProfile);
        }


        [HttpPost]
        public ActionResult StudentProfile(StudentProfile studentProfile, HttpPostedFileBase upLoadStudentImg)
        {
            StudentProfile studProfile = new StudentProfile();

            return View();
        }
        [HttpGet]
        public ActionResult DeleteStudent(int StudentId)
        {
          var userloggedId = User.Identity.GetUserId();
            Student std = student.GetStudentById(StudentId);
            std.IsActive = false;
            std.ModifiedById = userloggedId;
            std.ModifiedDate = DateTime.Now;
            int getStatus = student.DeleteStudent(std);
            return RedirectToAction("StudentList", "Student");

        }
    }
}