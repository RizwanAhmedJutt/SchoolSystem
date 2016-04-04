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

namespace SchoolManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeacherController : Controller
    {
        ITeacherRepositry repoTeacher = new TeacherRepositry();


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

    }
}