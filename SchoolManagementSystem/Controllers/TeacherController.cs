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
using System.IO;
using System.Web.UI.WebControls;
using PagedList;
using PagedList.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace SchoolManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        ITeacherRepositry repoTeacher = new TeacherRepositry();
        IExport exportfiles;

        // GET: Teacher
        public ActionResult TeacherList(string SearchBy, string search, int? page)
        {
            if (SearchBy == "CNIC" && search != "")
            {
                List<Teacher> objTeacher = repoTeacher.GetAllTeacherByCNIC(search);
                return View(objTeacher.ToList().ToPagedList(page ?? 1, 10));
            }
            else
            
            return View( repoTeacher.GetAllTeachers().ToList().ToPagedList(page??1,10));
        }

        public ActionResult AddChangesTeacher(int Id)
        {
            Teacher teacherdetail;
            if (Id == 0)
            {
                Teacher teacher = new Teacher();
                return View(teacher);
            }
            else
            {
                teacherdetail = repoTeacher.GetTeacherById(Id);
                return View(teacherdetail);
            }
        }

        [HttpPost]
        public ActionResult AddChangesTeacher(Teacher teach)
        {
            int teachId;
            var userIdentityId = User.Identity.GetUserId();
            if (teach.TeacherId > 0)
            {
                teach.ModifiedById = userIdentityId;
                teach.ModifiedDate = DateTime.Now;
                teachId = repoTeacher.InsertUpdateTeacher(teach);
            }
            else
            {
                teach.CreatedById = userIdentityId;
                teachId = repoTeacher.InsertUpdateTeacher(teach);
            }
            return RedirectToAction("AddChangesAddress", "Teacher", new { Id = teachId });
        }
        [HttpGet]
        public ActionResult AddChangesAddress(int Id)
        {
            TeacherAddress teacherAddres;
            teacherAddres = repoTeacher.GetTeacherAddressByTeacherId(Id);
            if (teacherAddres.TAddressId == 0)
            {
                TeacherAddress teacherAdd = new TeacherAddress();
                teacherAdd.TeacherId = Id;
                return View(teacherAdd);
            }
            else
            {

                return View(teacherAddres);
            }
        }

        [HttpPost]
        public ActionResult AddChangesAddress(TeacherAddress teachAdd)
        {
            int teachId;
         
            teachId = repoTeacher.InsertUpdateTAddress(teachAdd);
            return RedirectToAction("AddChangesTeacherContacts", "Teacher", new { Id = teachId });
        }
        [HttpGet]
        public ActionResult AddChangesTeacherContacts(int Id)
        {
            TeacherContact teacherContact;
            teacherContact = repoTeacher.GetTContactsById(Id);
            if (teacherContact.TeacherContactId == 0)
            {
                TeacherContact contacts = new TeacherContact();
                contacts.TeacherId = Id;
                return View(contacts);
            }
            else
            {
                return View(teacherContact);
            }

        }

        [HttpPost]
        public ActionResult AddChangesTeacherContacts(TeacherContact TContact)
        {
            int teachId;
            
            teachId = repoTeacher.InsertUpdateTContact(TContact);

            return RedirectToAction("AddChangesTeacherProfile", "Teacher", new { Id = teachId });

        }
        [HttpGet]
        public ActionResult AddChangesTeacherProfile(int Id)
        {
            TeacherProfile teacherProfile;
            teacherProfile = repoTeacher.GetTProfileById(Id);
            if(teacherProfile.TProfileId==0)
            { 
            TeacherProfile tProfile = new TeacherProfile();
            tProfile.TeacherId = Id;
               
            return View(tProfile);
            }
            else
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/FilesUpload/Images/"));
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    string fileName = Path.GetFileName(filePath);
                    if(fileName==teacherProfile.ImagePath)
                    {
                        files.Add(new ListItem(filePath));
                    }
                    
                }

                return View(teacherProfile);
            }
        }

        [HttpPost]
        public ActionResult AddChangesTeacherProfile(TeacherProfile tProfile, HttpPostedFileBase ulTeacherImage, FormCollection collection)
        {

            if (ulTeacherImage != null)
            {
                string filename = System.IO.Path.GetFileName(ulTeacherImage.FileName);
                /*Saving the file in server folder*/
                ulTeacherImage.SaveAs(Server.MapPath("~/FilesUpload/Images/" + filename));
                tProfile.ImagePath = "/FilesUpload/Images/" + filename;
            }
            int teachId;
            teachId = repoTeacher.InsertUpdateTProfile(tProfile);
            return RedirectToAction("TeacherList", "Teacher");
        }

        public ActionResult GetALLAssignClass(string SearchBy, string search, int? page)
        {
            if (SearchBy == "TeacherName" && search != "")
            {
                List<TeacherAssignClass> objTeacher = repoTeacher.GetAllTeacherAssignClassByTeacherName(search);
                return View(objTeacher.ToList().ToPagedList(page ?? 1, 10));
            
            
            }
            else
            return View(repoTeacher.GetAllTeacherAssignClass().ToList().ToPagedList(page ?? 1, 10));
        }

        [HttpGet]
        public ActionResult AddChangesTeacherAssignedClass(int id)
        {
            TeacherAssignClass teacherAssignClass=repoTeacher.GetTeacherAssignClassById(id);
            if (teacherAssignClass.TeacherAssignId== 0)
            {
                TeacherAssignClass tassignClass = new TeacherAssignClass();
                return View(tassignClass);
            }
            else
                return View(teacherAssignClass);
        }
        [HttpPost]
        public ActionResult AddChangesTeacherAssignedClass(TeacherAssignClass tassignClass)
        {
            int getStatus = repoTeacher.InsertUpdateTeacherAssignClass(tassignClass);
            return RedirectToAction("GetALLAssignClass");
        }
        public ActionResult DeleteTeacher(int Id)
        {
            Teacher teacher = repoTeacher.GetTeacherById(Id);
            teacher.Active = false;
            int getStatus = repoTeacher.DeleteTeacher(teacher);

            return RedirectToAction("TeacherList", "Teacher");
        }

       
        public ActionResult DDLTeacher(int AcadmicClassId)
        {
            try
            {
                if (AcadmicClassId > 0)
                {
                    ViewData["DDLTeacher"] = new SelectList(repoTeacher.GetTeacherByClass(AcadmicClassId).OrderBy(c => c.TeacherId).ToList(), "TeacherId", "TeacherName");
                    return View("../DropDownLists/DDLTeacher");
                }
                else
                {
                    ViewData["DDLTeacher"] = new SelectList(repoTeacher.GetALLTeacherByName().OrderBy(c => c.TeacherId).ToList(), "TeacherId", "TeacherName");
                    return View("../DropDownLists/DDLTeacher");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void GetTeacherReport()
        {
            int rowNo = 6;
            using (ExcelPackage pckg = new ExcelPackage())
            {

                ExcelWorksheet ws = pckg.Workbook.Worksheets.Add("Teachers");
                //set header
                using (ExcelRange rng = ws.Cells["I6:Y6"])
                {
                    ws.Cells[rowNo, 8].Value = "Teacher Name";
                    ws.Cells[rowNo, 9].Value = "CNIC";
                    ws.Cells[rowNo, 10].Value = "Last Qualification";
                    ws.Cells[rowNo, 11].Value = "JoinDate";
                    ws.Cells[rowNo, 12].Value = "Refrence Name";
                    ws.Cells[rowNo, 13].Value = "Refrence Contact";
                   
                    rng.Style.Font.Size = 12;

                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 204));
                    rng.Style.Font.Color.SetColor(Color.Black);


                }
                rowNo++;
                List<Teacher> teacherDetail = exportfiles.GetTeacherReport();
                foreach (var item in teacherDetail)
                {
                    ws.Cells[rowNo, 8].Value = item.TeacherName;
                    ws.Cells[rowNo, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 9].Value = item.CNIC;
                    ws.Cells[rowNo, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 10].Value = item.LastQualification;
                    ws.Cells[rowNo, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 11].Value = item.JoinDate;
                    ws.Cells[rowNo, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 12].Value = item.RefrenceName;
                    ws.Cells[rowNo, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 13].Value = item.RefrenceContact;
                    ws.Cells[rowNo, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                //Write it back to the client
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=SiteProductivity.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pckg.GetAsByteArray());
                Response.End();

            }
        }

        public void GetALLTeachersAssignedClass()
        {
            int rowNo = 6;
            using (ExcelPackage pckg = new ExcelPackage())
            {

                ExcelWorksheet ws = pckg.Workbook.Worksheets.Add("Teacher Assigned Class");
                //set header
                using (ExcelRange rng = ws.Cells["I6:Y6"])
                {
                    ws.Cells[rowNo, 8].Value = "Teacher Name";
                    ws.Cells[rowNo, 9].Value = "ClassName";
                    rng.Style.Font.Size = 12;

                    rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    rng.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 204));
                    rng.Style.Font.Color.SetColor(Color.Black);


                }
                rowNo++;
                List<TeacherAssignClass> teacherDetail = exportfiles.GetALLTeachersAssignedClass();
                foreach (var item in teacherDetail)
                {
                    ws.Cells[rowNo, 8].Value = item.TeacherName;
                    ws.Cells[rowNo, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Cells[rowNo, 9].Value = item.ClassName;
                    ws.Cells[rowNo, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                   
                }
                //Write it back to the client
                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;  filename=SiteProductivity.xlsx");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(pckg.GetAsByteArray());
                Response.End();

            }

        }
    }
}