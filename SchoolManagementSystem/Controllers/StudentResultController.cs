using SMSBusiness.Repository.Abstract;
using SMSBusiness.Repository.Concrete;
using SMSDataContract.Accounts;
using SMSDataContract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolManagementSystem.Controllers
{

    [Authorize(Roles = "Admin")]
    public class StudentResultController : Controller
    {
        //
        // GET: /StudentResult/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddChangesStudentResult()
        {
            StudentResultSheet srs = new StudentResultSheet();

            return View(srs);
        }

        public ActionResult ResultTemplate(int AcadmicClassId,int StudentId)
        {
            IStudentAssignCourse saC = new StudentAssignCourseBLL();
            var query = (from x in saC.GetStudentAssignedCourse().ToList()
                         where x.AcadmicClassId == AcadmicClassId
                         && x.StudentId == StudentId
                         select new Course() {CourseId= x.CourseId,CourseName= x.CourseName }).ToList();
            ResultSheetHelper rsh = new ResultSheetHelper();
            rsh.Courses = query.ToList();

            return View(rsh);
        }

       



    }
}