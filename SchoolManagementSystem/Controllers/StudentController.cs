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
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Web.Helpers;
using Rotativa.Core;
using Rotativa.MVC;
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
        IExport exportfiles=new ExportFileBLL();
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StudentList(string SearchBy, string search, int? page)
        {
            if (SearchBy == "FirstName" && search == "")
            {
                List<Student> stdByName = student.GetAllStudents().ToList();

                return View(stdByName.ToList().ToPagedList(page ?? 1, 10));
            }
            if (SearchBy == "FirstName")
            {
                List<Student> stdByName = student.GetAllStudents().Where(x => x.FirstName == search || search == null).ToList();

                return View(stdByName.ToList().ToPagedList(page ?? 1, 10));
            }
            if (SearchBy == "NotActiv" && search == "")
            {
                List<Student> stdByName = student.GetALLDisActiveStudents().ToList();

                return View(stdByName.ToList().ToPagedList(page ?? 1, 10));
            }

            if (SearchBy == "NotActiv")
            {
                List<Student> stdByName = student.GetALLDisActiveStudents().Where(x => x.FirstName == search || search == null).ToList();

                return View(stdByName.ToList().ToPagedList(page ?? 1, 10));
            }

            return View(student.GetAllStudents().ToList().ToPagedList(page ?? 1, 10));
        }
        [HttpGet]
        public ActionResult ExportReport()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExportReport(DateTime startDate,DateTime endDate, int AcadmicClassId,int? page)
        {
           // ExportStudentReport(startDate, endDate, AcadmicClassId);
            List<StudentDetail> studentDetail = exportfiles.GetStudentReport(startDate, endDate, AcadmicClassId);
            return new PartialViewAsPdf("../Student/_StudentListExport", studentDetail.ToList())
            {
                
                FileName = "StudentDetail.pdf"
            };
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
                std.ModifiedDate = DateTime.Now;
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
                ViewBag.RollNumber = std.RollNumber;
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
            agranted.AdmissionGrantedForClass = AcadmicClassId;
            if (agranted.AdmissionId == 0)
            {
                agranted.CreatedById = userloggedId.ToString();
                agranted.CreatedDate = DateTime.Now;
            }
            else
            {
                agranted.ModifiedById = userloggedId.ToString();
                agranted.ModifiedDate = DateTime.Now;
            }
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
        public void ExportStudentReport(DateTime startDate, DateTime endDate, int AcadmicClass)
        {
            int rowNo = 6;
            using (ExcelPackage pckg = new ExcelPackage())
            {

                ExcelWorksheet ws = pckg.Workbook.Worksheets.Add("Students");
                //set header
                using (ExcelRange rng = ws.Cells["I6:Y6"])
                {
                    ws.Cells[rowNo, 8].Value = "Student Name";
                    ws.Cells[rowNo, 9].Value = "Father Name";
                    ws.Cells[rowNo, 10].Value = "Class Name";
                    ws.Cells[rowNo, 11].Value = "Birth Date";
                    ws.Cells[rowNo, 12].Value = "Religion";
                    ws.Cells[rowNo, 13].Value = "CNIC";
                    ws.Cells[rowNo, 14].Value = "TotalSibling";
                    ws.Cells[rowNo, 15].Value = "CurrentSchoolSibling";
                    ws.Cells[rowNo, 16].Value = "CityName";
                    ws.Cells[rowNo, 17].Value = "PermanentAddress";
                    ws.Cells[rowNo, 18].Value = "PresentAddress";
                    ws.Cells[rowNo, 19].Value = "GuardianName";
                    ws.Cells[rowNo, 20].Value = "GuardianContacts";
                    ws.Cells[rowNo, 21].Value = "AdmissionGrantedDate";
                    ws.Cells[rowNo, 22].Value = "AdmissionGrantedForClass";
                    ws.Cells[rowNo, 23].Value = "AssessmentResult";
                    rng.Style.Font.Size = 12;

                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 204));
                    rng.Style.Font.Color.SetColor(Color.Black);


                }
                rowNo++;
                List<StudentDetail> studentDetail = exportfiles.GetStudentReport(startDate, endDate, AcadmicClass);
                foreach (var item in studentDetail)
                {
                    ws.Cells[rowNo, 8].Value = item.Student.StudentName;
                    ws.Cells[rowNo, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 9].Value = item.GuardianDetail.FatherName;
                    ws.Cells[rowNo, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 10].Value = item.AcadmicClass.ClassName;
                    ws.Cells[rowNo, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 11].Value = item.Student.DOB;
                    ws.Cells[rowNo, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 12].Value = item.Student.Religion;
                    ws.Cells[rowNo, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 13].Value = item.Student.CNIC;
                    ws.Cells[rowNo, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 14].Value = item.Student.NoOfSibling;
                    ws.Cells[rowNo, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 15].Value = item.Student.NoOfSiblingCurrentSchool;
                    ws.Cells[rowNo, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 16].Value = item.Cities.CityName;
                    ws.Cells[rowNo, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 17].Value = item.StudentAddress.PermanentAddress;
                    ws.Cells[rowNo, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 18].Value = item.StudentAddress.PresentAddress;
                    ws.Cells[rowNo, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 19].Value = item.GuardianDetail.GuardianName;
                    ws.Cells[rowNo, 19].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 20].Value = item.GuardianContact.FirstContact;
                    ws.Cells[rowNo, 20].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 21].Value = item.AdmissionGranted.AdmissionGrantedDate;
                    ws.Cells[rowNo, 21].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 22].Value = item.AdmissionGranted.AdmissionGrantedForClass;
                    ws.Cells[rowNo, 22].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 23].Value = item.AdmissionGranted.AssessmentResult;
                    ws.Cells[rowNo, 23].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                }
               
                //Write it back to the client
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=StudentReport.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pckg.GetAsByteArray());
                Response.End();

            }
        }
        
        public JsonResult IsStudentExist(string FName,string LName,DateTime BirthDate)
        {

            var query = student.GetAllStudents().Where(x => x.FirstName == FName && x.LastName == LName && x.DOB == BirthDate).Select(x => x).FirstOrDefault();
            if(query!=null)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            }
        }
    
    }
}