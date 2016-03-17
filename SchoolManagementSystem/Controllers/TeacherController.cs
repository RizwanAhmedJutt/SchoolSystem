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
    [Authorize]
    public class TeacherController : Controller
    {
        ITeacherRepositry repoTeacher = new TeacherRepositry();

        
        // GET: Teacher
        public ActionResult TeacherList()
        {
            List<Teacher> list = new List<Teacher>();
           list =  repoTeacher.GetAllTeachers();
           return View(list);
        }

        public ActionResult Teacher(int Id)
        {
            Teacher teacher = new Teacher();
            return View(teacher);
        }

        [HttpPost]
        public ActionResult Teacher(Teacher teach, int Id)
        {
            int teachId;
            var userIdentityId = User.Identity.GetUserId();
            if (teach.TeacherId > 0)
            {
                teach.CreatedById = userIdentityId;
                teach.CreatedDate = DateTime.Now;
                teach.ModifiedById = userIdentityId;
                teach.ModifiedDate = DateTime.Now;
                teachId = repoTeacher.InsertUpdateTeacher(teach);
            }
            else
            {
                teach.CreatedById = userIdentityId;
                teach.CreatedDate = DateTime.Now;
                teach.ModifiedDate = DateTime.Now;
                teachId = repoTeacher.InsertUpdateTeacher(teach);
            }
            return RedirectToAction("TeacherAddress", "Teacher", new { Id = teachId });
        }

        public ActionResult TeacherAddress(int Id)
        {
            TeacherAddress teacherAdd = new TeacherAddress();
            teacherAdd.TeacherId = Id;
            return View(teacherAdd);
        }

        [HttpPost]
        public ActionResult TeacherAddress(TeacherAddress teachAdd)
        {
            int teachId;
            TeacherAddress teacherAdd = new TeacherAddress();
            teachId = repoTeacher.InsertUpdateTAddress(teachAdd);
            return RedirectToAction("TeacherContacts", "Teacher", new { Id = teachId });
        }


        public ActionResult TeacherAssignCourse()
        {
            return View();
        }
        public ActionResult TeacherContacts(int Id)
        {
            TeacherContact contacts = new TeacherContact();
            contacts.TeacherId = Id;
            return View(contacts);

        }

        [HttpPost]
        public ActionResult TeacherContacts(TeacherContact TContact)
        {
            int teachId;
            TeacherContact contacts = new TeacherContact();
             teachId = repoTeacher.InsertUpdateTContact(TContact);

             return RedirectToAction("TeacherProfile", "Teacher", new { Id = teachId });

        }
        public ActionResult TeacherProfile(int Id)
        {
            TeacherProfile tProfile = new TeacherProfile();
            tProfile.TeacherId = Id;
            return View(tProfile);
        }

        [HttpPost]
        public ActionResult TeacherProfile(TeacherProfile tProfile, HttpPostedFileBase AddVideo)
        {

            if (AddVideo != null)
            {
                string filename = System.IO.Path.GetFileName(AddVideo.FileName);
                /*Saving the file in server folder*/
                AddVideo.SaveAs(Server.MapPath("~/FilesUpload/Images/" + filename));
                tProfile.ImagePath = "/FilesUpload/Images/" + filename;
            }
            int teachId;
            teachId = repoTeacher.InsertUpdateTProfile(tProfile);
            return RedirectToAction("TeacherList", "Teacher");
        }

    }
}